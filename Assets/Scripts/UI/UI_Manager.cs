using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using Bolt;
public class UI_Manager : GlobalEventListener
{
    public static UI_Manager Instance; 
    public Dictionary<string,UIDisplay> ui_dictionary;
    private void Awake()
    {
        Instance = this;    
        ui_dictionary = new Dictionary<string, UIDisplay>();
    }
    public void Open_UI_Friend(){
        
    }
    public static void RegisterUI(UIDisplay displayUI){
        if(!Instance.ui_dictionary.ContainsKey(displayUI.id))
            Instance.ui_dictionary.Add(displayUI.id,displayUI);
    }
    public static void UnregisterUI(UIDisplay displayUI){
        if(Instance.ui_dictionary.ContainsKey(displayUI.id))
            Instance.ui_dictionary.Remove(displayUI.id);
    }
    public static void OpenUI(string ui_name_key){
        if(Instance.ui_dictionary.ContainsKey(ui_name_key)){
            var openList = from list in Instance.ui_dictionary
                        where list.Value.root.activeSelf
                        select list.Value;
            foreach (var item in openList)
            {
                item.Close();
            }
            Instance.ui_dictionary.Values.First(u =>u.id == ui_name_key).Open();
        }
    }
}
