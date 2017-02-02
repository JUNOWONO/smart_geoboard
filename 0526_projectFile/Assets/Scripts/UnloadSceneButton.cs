using UnityEngine;
using System.Collections;

public class UnloadSceneButton : MonoBehaviour {

	public string sceneName;
	
	public void OnClick()
	{
		Application.UnloadLevel (sceneName);
	}

}
