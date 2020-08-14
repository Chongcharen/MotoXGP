using UnityEngine;
using UnityEngine.UI;
using UniRx;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using DG.Tweening;
public class PlayerSliderPrefab : MonoBehaviourPunCallbacks
{
    [SerializeField]Transform root;
    [SerializeField]Slider slider;
    [SerializeField]Image player_avatar,img_pin,img_glow;
    bool isOvertakeAnimate = false;
    bool isCloseOvertakeAnimate = false;
    Hashtable playerData;
    PlayerDistanceData distanceData;
    Color baseColor;
    bool isBaseColor = true;
    void Start(){
        AbikeChopSystem.OnBoostTime.Subscribe(time =>{
            Debug.Log("Boost time "+time);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(img_glow.DOFade(0.7f,0.5f))
            .Join(img_glow.DOFade(0,0.5f).SetDelay(time));
            sequence.Play().SetAutoKill();
        }).AddTo(this);
    }
    public void SetUpData(Hashtable _playerData,Color rankColor){
        img_pin.color = rankColor;
        baseColor = rankColor;
    }
    public void SetValue(float _vale){
        slider.value = _vale;
    }
    public void OverTaking(){
        if(isOvertakeAnimate)return;
        isOvertakeAnimate = true;
        img_glow.DOFade(0.3f,1f);
        root.DOScale(1.5f,1);
        ChangeColor(Color.white);
    }
    public void CloseOverTake(){
        if(isCloseOvertakeAnimate)return;
        isCloseOvertakeAnimate = true;
        img_glow.DOFade(0,1).OnComplete(()=>{
            isCloseOvertakeAnimate = false;
            isOvertakeAnimate = false;
        });
        img_pin.DOKill();
        img_pin.color = baseColor;
        root.DOScale(1.2f,1);
    }
    public void ChangeColor(Color color){
        img_pin.DOColor(color,0.2f).OnComplete(()=>{
            isBaseColor = !isBaseColor;
            ChangeColor(isBaseColor ? Color.white : baseColor);
        });
    }
    public Slider GetSlider(){return slider;}

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged){

    }
}
