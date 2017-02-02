using UnityEngine;
using System.Collections;

public class ObjectCloseBtnScript : MonoBehaviour {

	public GameObject anyObject;


	public void OnClick()
	{
		anyObject.SetActive(false);

	}


}
