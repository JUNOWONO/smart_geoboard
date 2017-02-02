using UnityEngine;
using System.Collections;

public class mouseClickDisable : MonoBehaviour {
	public GameObject anyObject;
	bool plag = false;

	void Update(){
		if (plag == false) {
			if (Input.GetMouseButtonDown (0)) {
				anyObject.SetActive(false);
				plag = true;
			}
		}
	}

}
