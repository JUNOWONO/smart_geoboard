using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {
	
	public GameObject gameObject;
	public GameObject go_ingame_button;
	
	void Start()
	{
		gameObject.SetActive (false);
	}
	
	void Update()
	{
		if (go_ingame_button.activeSelf == false) {
			gameObject.SetActive(false);	
			go_ingame_button.SetActive(true);
		}
		
		if (gameObject.activeSelf == false) {
			if (Time.timeScale == 0.0f) {
				Time.timeScale = 1.0f;
			}
		}
	}
	
	
	public void OnClick() {
		if (gameObject.activeSelf == false) {
			gameObject.SetActive (true);
			Time.timeScale = 0.0f;
		} else if (gameObject.activeSelf == true) { 
			Time.timeScale = 1.0f;
			gameObject.SetActive (false);
		}

	}
}
