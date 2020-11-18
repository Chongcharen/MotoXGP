using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class AddressableLocationLoader
{
    public static async Task GetAll(List<object> Labels, IList<IResourceLocation> loadedLocations){
        var unloadLocations = await Addressables.LoadResourceLocationsAsync(Labels,Addressables.MergeMode.Union).Task;
        foreach (var location in unloadLocations)
        {
           // Debug.Log("location primary key "+location.PrimaryKey);
            //Debug.Log("dependencies "+location.Dependencies.Count);
            loadedLocations.Add(location);
        }
    }
}
