using UnityEngine;
using System.Collections;

public class GoBackToIntroBtn : MonoBehaviour {

	public void OnClick()
	{
		Application.LoadLevel ("IntroScene");

	}
}
