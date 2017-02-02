using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageScreenSize : MonoBehaviour {

	public Image image;
	// Use this for initialization
	void Start () {
		Screen.SetResolution (Screen.width, Screen.height, true);

	}

}
