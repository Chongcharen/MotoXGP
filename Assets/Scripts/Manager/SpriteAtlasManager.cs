using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasManager : MonoSingleton<SpriteAtlasManager>
{
    public Dictionary<string,SpriteAtlas> atlas;
    private void Awake() {
        atlas = new Dictionary<string, SpriteAtlas>();    
    }
    public void LoadSprite(){

    }
    // sample equipment_helmet
    // public SpriteAtlas GetAtlas(string atlasname){
    //     if(!atlas.ContainsKey(atlasname)){
    //         var atlasLoad = Resources.Load("Atlas/"+atlasname) as SpriteAtlas;
    //         if(atlasLoad != null){
    //             atlas.Add(atlasname,atlasLoad);
    //             return atlasLoad;
    //         }else
    //         {
    //             Debug.LogError("Connot find Atlas "+atlasname);
    //             return null;
    //         }
    //     }
    //     return atlas[atlasname];
    // }
    public async Task<SpriteAtlas> GetAtlas(string name){
        return await AddressableManager.Instance.LoadObject<SpriteAtlas>(name);
    }
    
}
