using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Build.DataBuilders;
using UnityEditor.Build;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEditor.Build.Pipeline.Utilities;
using UnityEditor.SceneManagement;

using UnityEngine;

// using UnityEditor.AddressableAssets.Initialization;
// using UnityEditor.AddressableAssets.ResourceLocators;

/// <summary>
/// Build script that takes care of modifying the settings.xml to use our json provider for loading the remote hash
/// </summary>
[CreateAssetMenu(fileName = "PlayFabStorageBuildScript.asset", menuName = "Addressables/Content Builders/PlayFab Build")]
public class PlayFabStorageBuildScript : BuildScriptPackedMode
{
    public override string Name => "PlayFab Build";

    protected override TResult DoBuild<TResult>(AddressablesDataBuilderInput builderInput, AddressableAssetsBuildContext aaContext)
    {
        Debug.Log("DoBuild "+aaContext);
        Debug.Log("DoBuild profileSettings "+builderInput.AddressableSettings.profileSettings);
        Debug.Log("DoBuild AssetPath "+builderInput.AddressableSettings.AssetPath);
        var buildResult = base.DoBuild<TResult>(builderInput, aaContext);
        if (aaContext.settings.BuildRemoteCatalog)
        {
            PatchSettingsFile(builderInput);
        }
        else
        {
            Debug.LogWarning("[TheGamedevGuru] PlayFab: Addressables Remote Catalog is not enabled, skipping patching of the settings file");
        }
        return buildResult;
    }

    private void PatchSettingsFile(AddressablesDataBuilderInput builderInput)
    {
        // Get the path to the settings.json file
        //Debug.Log("Addressables.BuildPath "+Addressables.BuildPath);
        //Debug.Log("builderInput.RuntimeSettingsFilename "+builderInput.RuntimeSettingsFilename);
        var settingsJsonPath = UnityEngine.AddressableAssets.Addressables.BuildPath + "/" + builderInput.RuntimeSettingsFilename;
        //Debug.Log("settingsJsonPath "+settingsJsonPath);
        // Parse the JSON document
        var settingsJson = JsonUtility.FromJson<UnityEngine.AddressableAssets.Initialization.ResourceManagerRuntimeData>(File.ReadAllText(settingsJsonPath));

        // Look for the remote hash section
        var originalRemoteHashCatalogLocation = settingsJson.CatalogLocations.Find(locationData => locationData.Keys[0] == "AddressablesMainContentCatalogRemoteHash");
        var isRemoteLoadPathValid = originalRemoteHashCatalogLocation.InternalId.StartsWith("playfab://");
        if (isRemoteLoadPathValid == false)
        {
            throw new BuildFailedException("RemoteBuildPath must start with playfab://");
        }

        // Change the remote hash provider to our PlayFabStorageHashProvider
        var newRemoteHashCatalogLocation = new UnityEngine.AddressableAssets.ResourceLocators.ResourceLocationData(originalRemoteHashCatalogLocation.Keys, originalRemoteHashCatalogLocation.InternalId, typeof(PlayFabStorageHashProvider), originalRemoteHashCatalogLocation.ResourceType, originalRemoteHashCatalogLocation.Dependencies);
        settingsJson.CatalogLocations.Remove(originalRemoteHashCatalogLocation);
        settingsJson.CatalogLocations.Add(newRemoteHashCatalogLocation);

        File.WriteAllText(settingsJsonPath, JsonUtility.ToJson(settingsJson));
    }
}
