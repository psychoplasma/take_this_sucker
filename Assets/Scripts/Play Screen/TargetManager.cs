using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour {
	/*
	public GameObject faceObj;
	public GameObject loadingPanel;

	Texture2D texture;
	AndroidJavaClass picBrowser;
	AndroidJavaObject instance;

	void Start() {
	}

	public void getPic() {
		suspendGame();
		picBrowser = new AndroidJavaClass("com.apps.mmorca.picbrowser.PicBrowser");
		picBrowser.CallStatic("start", gameObject.name, "isImageReady");
		instance = picBrowser.GetStatic<AndroidJavaObject>("instance");
		instance.Call("startBrowsing");
		resumeGame();
	}

	private void suspendGame() {
		Time.timeScale = 0;
		loadingPanel.SetActive(true);
	}

	private void resumeGame() {
		Time.timeScale = 1;
		loadingPanel.SetActive(false);
	}


	//Called by Android Plugin ==> PicBrowser 
	private void isImageReady(string ready) {
		Debug.Log ("isImageReady is executing...");

		if (ready == "ready") {

			AndroidJavaObject obj = instance.Call<AndroidJavaObject>("getSelectedImage");
			byte[] bmp = AndroidJNIHelper.ConvertFromJNIArray<byte[]>(obj.GetRawObject());
			obj.Dispose();
			picBrowser.Dispose();
			instance.Dispose();

			try {
				if(bmp == null) {
					Debug.Log("byte array returning from Android is null!!!");
					return;
				}

				texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
				texture.LoadImage(bmp);

				faceObj.GetComponent<Renderer>().material.mainTexture = texture;

				//Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), new Vector2(0.5f,0.5f));
				//btn.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
			} catch(UnityException e) {
				Debug.Log("Error occured while loading image: " + e.ToString());
			}
			resumeGame();
		}
	}
	*/
}
