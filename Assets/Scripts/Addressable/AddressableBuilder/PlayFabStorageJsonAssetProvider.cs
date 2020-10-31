using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayFabStorageJsonAssetProvider : JsonAssetProvider
{
    public override string ProviderId => typeof(JsonAssetProvider).FullName;

    public override void Provide(ProvideHandle provideHandle)
    {
        Debug.Log("Provide "+provideHandle.Location);
        Debug.Log("start with "+provideHandle.Location.InternalId.StartsWith("playfab://"));
        if (provideHandle.Location.InternalId.StartsWith("playfab://") == false)
        {
            base.Provide(provideHandle);
            return;
        }

        var addressableId = provideHandle.Location.InternalId.Replace("playfab://", "");
        PlayFabClientAPI.GetContentDownloadUrl(
            new GetContentDownloadUrlRequest() { Key = addressableId, ThruCDN = false },
            result =>
            {
                Assert.IsTrue(provideHandle.Location.ResourceType == typeof(ContentCatalogData), "Only catalogs supported");
                var resourceLocation = new ResourceLocationBase(result.URL, result.URL, typeof(JsonAssetProvider).FullName, typeof(string));
                provideHandle.ResourceManager.ProvideResource<ContentCatalogData>(resourceLocation).Completed += handle =>
                {
                    Debug.Log("provideHandle complete "+handle.Result);
                    var contents = handle.Result;
                    provideHandle.Complete(contents, true, handle.OperationException);
                };
            },
            error => Debug.LogError(error.GenerateErrorReport()));
    }
}