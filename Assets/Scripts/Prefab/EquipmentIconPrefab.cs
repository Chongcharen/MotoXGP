using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
public class EquipmentIconPrefab : MonoBehaviour
{
    public static Subject<EquipmentTrack> OnEquipmentChanged = new Subject<EquipmentTrack>();
    public Image img_icon;
    public Toggle toggle;
    PartEquipmentData data;
    private void Start() {
        // EventTrigger trigger = GetComponentInParent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerClick;
        // entry.callback.AddListener( (eventData) => { Foo(); } );
        // trigger.triggers.Add(entry);
        toggle.OnValueChangedAsObservable().Subscribe(_ =>{
            if(!_)return;
            var track = new EquipmentTrack();
            track.id = 0;
            track.model_name = data.model_name;
            track.texture_name = data.texture_name;
            OnEquipmentChanged.OnNext(track);
        }).AddTo(this);
    }
    public void Setup(PartEquipmentData _data,ToggleGroup group){
        data = _data;
        toggle.group = group;
        var atlasSprite = SpriteAtlasManager.Instance.GetAtlas("equipment_helmet");
        img_icon.sprite = atlasSprite.GetSprite(data.icon_name);

    }

   // public void OnSelect
}
