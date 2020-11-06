using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using TMPro;
using UnityEngine.U2D;

public class EquipmentIconPrefab : MonoBehaviour
{
    public static Subject<EquipmentTrack> OnEquipmentChanged = new Subject<EquipmentTrack>();
    public TextMeshProUGUI price_txt;
    public Image img_icon,img_overlay,img_coin;
    public Toggle toggle;
    PartEquipmentData data;
    int equipmentIndex = -1;
    private void Start() {
        // EventTrigger trigger = GetComponentInParent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerClick;
        // entry.callback.AddListener( (eventData) => { Foo(); } );
        // trigger.triggers.Add(entry);
        
    }
    public void Setup(int _equipmentIndex,SpriteAtlas atlasSprite,PartEquipmentData _data,ToggleGroup group){
        equipmentIndex = _equipmentIndex;
        data = _data;
        toggle.group = group;
       // var atlasSprite = await AddressableManager.Instance.LoadObject<SpriteAtlas>(equipmentKey);
        // Debug.Log($"atlas {atlasSprite}");
        // Debug.Log("atlasSprite "+atlasSprite);
        // Debug.Log("data.icon_name "+data.icon_name);
        img_icon.sprite = atlasSprite.GetSprite(data.icon_name);
        if( _data.price > 0){
            price_txt.text = _data.price.ToString();
            img_overlay.enabled = true;
            img_coin.enabled = true;
        }else{
            img_coin.enabled = false;
            img_overlay.enabled = false;
        }
        toggle.OnValueChangedAsObservable().Subscribe(_ =>{
            if(!_)return;
            var track = new EquipmentTrack();
            track.id = equipmentIndex;
            Debug.Log("tack id"+track.id);
             Debug.Log("tack id"+data.texture_name);
            track.model_name = data.model_name;
            track.texture_name = data.texture_name;
            OnEquipmentChanged.OnNext(track);
        }).AddTo(this);
    }

   // public void OnSelect
}
