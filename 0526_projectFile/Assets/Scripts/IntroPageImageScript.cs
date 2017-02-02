using UnityEngine;
using System.Collections;

public class IntroPageImageScript : MonoBehaviour {

	RectTransform rectTransform;

	// Use this for initialization
	void start () {

		rectTransform = GameObject.Find("Image").GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

	}

}
