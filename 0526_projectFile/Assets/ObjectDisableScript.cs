using UnityEngine;
using System.Collections;

public class ObjectDisableScript : MonoBehaviour {
	public GameObject gameobject;
	// Use this for initialization
	void Start () {
		gameobject.SetActive (false);
	}

}
