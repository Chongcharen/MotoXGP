using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UniRx;

public class ImageManager : MonoBehaviour {

    static ImageManager instance;
	Dictionary<string,Texture2D> imageDic = new Dictionary<string, Texture2D>();
	Texture2D imageTexture;
    public static ImageManager Instance
    {
        get
        {
            if(instance == null)
            {
                var go = new GameObject();
                instance = go.AddComponent<ImageManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
	public bool IsImage(string link){
		return imageDic.ContainsKey (link);
	}
	public Texture2D GetImage(string link)
	{
		return imageDic [link];
	}
	public void RemoveImageLink(string link){
		if (IsImage (link)) {
			imageDic.Remove (link);
		}
	}
	public void AddImageLink(string link , Texture2D texture){
		if (!IsImage (link)) {
			imageDic.Add (link, texture);
		}
	}
    public void SetRawImage(RawImage rawImg,string linkImage,RectTransform rect = null)
    {
        if (IsImage(linkImage))
        {
            rawImg.texture = GetImage(linkImage);
            rawImg.SetNativeSize();
            if(rect != null)
            {
                SetRawImageAnchor(rawImg, rect);
            }
        }
        else
        {
            // StartCoroutine(StaticCoroutine.LoadImage(linkImage, texture =>
            // {
            //     rawImg.texture = texture;
            //     rawImg.SetNativeSize();
            //     AddImageLink(linkImage, texture);
            //     if (rect != null)
            //     {
            //         SetRawImageAnchor(rawImg, rect);
            //     }
            // }));
        }
    }

	public void SetRawImageByScale(RawImage rawImg,string linkImage,Rect rect,float scaleSize,bool isWidth = true)
	{
		Texture2D _texture;
		float num1;
		float num2;



		if (IsImage(linkImage))
		{
			_texture = GetImage(linkImage);
			num1 = (isWidth == true) ? rect.width : rect.height;
			num2 = (isWidth == true) ? _texture.width : _texture.height;
			SetScaleImage(rect.width,ref _texture,true);
			rawImg.texture = _texture;
			rawImg.SetNativeSize();

		}
		else
		{
			// StartCoroutine(StaticCoroutine.LoadImage(linkImage, texture =>
			// 	{
			// 		_texture = texture;
			// 		num1 = (isWidth == true) ? rect.width : rect.height;
			// 		num2 = (isWidth == true) ? _texture.width : _texture.height;
			// 		SetScaleImage(num1*scaleSize,num2,ref _texture,true);
			// 		rawImg.texture = _texture;
			// 		rawImg.SetNativeSize();
			// 		AddImageLink(linkImage, _texture);
					
			// 	}));
			
		}
	}
    void SetRawImageAnchor(RawImage rawImg,RectTransform rect)
    {
            rect = rawImg.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
    }

    public Texture2D CropImage(RectTransform rectTrans,CanvasScaler scaler){
		float scaleWidth;
		float scaleHeight;
		float scaleScreen;
		float _width;
		float _height;

		float differentHeight;

		float startHeight;
		Rect rectCrop;
		Texture2D texture;

		scaleWidth = Screen.width / rectTrans.rect.width;
		scaleHeight = Screen.height/scaler.referenceResolution.y;

		scaleScreen = scaleHeight - 1;
		_width = rectTrans.rect.width * scaleWidth;
		if (scaleScreen > 0.5) {
			_height = (scaleScreen * rectTrans.rect.width) + rectTrans.rect.width;//0.690140845070423
		} else {
			_height = (scaleScreen * Screen.height) + rectTrans.rect.width;//0.126760563380282
			differentHeight = _height * scaleScreen;
			_height -= differentHeight;
		}


		startHeight = Screen.height- _height;

		texture = new Texture2D((int)_width,(int)_height,TextureFormat.RGB24, true);
		rectCrop = new Rect (0, startHeight, _width, _height);
		texture.ReadPixels (new Rect(0,rectTrans.rect.height*0.78f,640,640),0,0,false);
		texture.ReadPixels(rectCrop,0,0,false);
		texture.SetPixel (0, 0, Color.white);
		texture.Apply();
	
		return texture;
	}

	public static void SetScaleImage(float _fixWidth , ref Texture2D _texture){
		if (_texture != null)
		{
			float scaleWidth = CalculateImageScale(_fixWidth,_texture.width);
			TextureScale.Bilinear(_texture, Mathf.FloorToInt(_texture.width * scaleWidth), Mathf.FloorToInt(_texture.height * scaleWidth));
		}
	}

    public static void SetScaleImage(float _fixWidth , ref Texture2D _texture,bool isTextureSmall){

		float scaleWidth;
		float num1;
		float num2;
		if (_texture != null) {
			if (isTextureSmall) {
				num1 = (_fixWidth > _texture.width) ? _texture.width : _fixWidth;
				num2 = (_fixWidth > _texture.width) ? _fixWidth : _texture.width;
			} else {
				num1 = _fixWidth;
				num2 = _texture.width;
			}
		}
		scaleWidth = CalculateImageScale (_fixWidth, _texture.width);
		TextureScale.Bilinear (_texture, Mathf.FloorToInt (_texture.width * scaleWidth), Mathf.FloorToInt (_texture.height * scaleWidth));
	}

	public static void SetScaleImage(float _screenSize,float _textureSize , ref Texture2D _texture,bool isTextureSmall){

		float scaleWidth;
		float num1 = 0;
		float num2 = 0;
		if (_texture != null) {
			if (isTextureSmall) {
				num1 = (_screenSize > _textureSize) ? _textureSize : _screenSize;
				num2 = (_screenSize > _textureSize) ? _screenSize : _textureSize;
			} else {
				num1 = _screenSize;
				num2 = _textureSize;
			}
		}
		scaleWidth = CalculateImageScale (num1, num2);
		//TextureScale.Bilinear (_texture, Mathf.FloorToInt (_texture.width * scaleWidth), Mathf.FloorToInt (_texture.height * scaleWidth));	
	}
    public static float CalculateImageScale(float screen, float textHeight)
    {
        float scale;
        scale = screen / textHeight;
        return scale;
    }
	// public IEnumerator LoadImage(string path,Action<Texture2D> onComplete){
	// 	// using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
    //     //     {
    //     //         yield return uwr.SendWebRequest();
 
    //     //         if (uwr.isNetworkError || uwr.isHttpError)
    //     //         {
    //     //             Debug.Log(uwr.error);
    //     //         }
    //     //         else
    //     //         {
    //     //             // Get downloaded asset bundle
    //     //             //var t = DownloadHandlerTexture.GetContent(uwr);
	// 	// 			onComplete(DownloadHandlerTexture.GetContent(uwr));
    //     //         }
    //     //     }
	// }
	public IEnumerator LoadImage(string path,Action<Texture2D> onComplete)
    {
		Debug.Log("Load iamge "+path);
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                onComplete(DownloadHandlerTexture.GetContent(uwr));
            }
        }
	}
}