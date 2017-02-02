using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomBtnHandler : MonoBehaviour {

	public void OnClick() {
		if (Time.timeScale == 0.0f)
			Time.timeScale = 1.0f;
		else
			Time.timeScale = 0.0f;
		
	}

}
