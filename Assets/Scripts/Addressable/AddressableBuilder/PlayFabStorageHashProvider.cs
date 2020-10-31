using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine;
public class PlayFabStorageHashProvider : ResourceProviderBase
{
    public override void Provide(ProvideHandle provideHandle)
    {
        Debug.Log("PlayFabStorageHashProvider "+provideHandle.Location);
        Debug.Log("provideHandle.Location.InternalId "+provideHandle.Location.InternalId);
         Debug.Log("provider id "+provideHandle.Location.ProviderId);
        var addressableId = provideHandle.Location.InternalId.Replace("playfab://", "");
        Debug.Log("addressableId "+addressableId);
        PlayFabClientAPI.GetContentDownloadUrl(
            new GetContentDownloadUrlRequest() { Key = addressableId, ThruCDN = false },
            result =>
            {
                var resourceLocation = new ResourceLocationBase(result.URL, result.URL, typeof(TextDataProvider).FullName, typeof(string));
                provideHandle.ResourceManager.ProvideResource<string>(resourceLocation).Completed += handle =>
                {
                    Debug.Log("provideHandle completed "+handle.Result);
                    var contents = handle.Result;
                    provideHandle.Complete(contents, true, handle.OperationException);
                };
            },
            error => Debug.LogError(error.GenerateErrorReport()));
    }
}
