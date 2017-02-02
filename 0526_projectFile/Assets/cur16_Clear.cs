using UnityEngine;
using System.Collections;

public class cur16_Clear : MonoBehaviour {


	public int clearCount_ = 0;
	int clearCount = 3;
	
	public GameObject nextBtn;

	void Start () {
		nextBtn.SetActive (false);
	}

	// Use this for initialization
	void Update () {
		if (clearCount_ >= 3) {
			nextBtn.SetActive(true);
		}

	}

}
