using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroSceneCanvasScript : MonoBehaviour {


//	Image image;
	// Use this for initialization
	void Start () {
		//image = GetComponentInChildren<Image> ();
		//image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		Screen.SetResolution (Screen.width, Screen.height, true);
	}
}
