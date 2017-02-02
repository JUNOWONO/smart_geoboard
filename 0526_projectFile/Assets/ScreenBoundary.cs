using UnityEngine;
using System.Collections;

public class ScreenBoundary : MonoBehaviour {

	public GameObject[] gameobject;
	private Vector3 wrld;
	private float width;
	private float height;
	// Use this for initialization
	void Start () {

		float width = Screen.width ;
		float height = Screen.height;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		for (int i = 0; i < gameobject.Length; i++) {
		
			if(gameobject[i].transform.position.x > width || gameobject[i].transform.position.x  < 0 )
				gameobject[i].GetComponent<Rigidbody>().velocity = new Vector3(-10, 0 , 0);
			if(gameobject[i].transform.position.y > height || gameobject[i].transform.position.x < 0)
				gameobject[i].GetComponent<Rigidbody>().velocity = new Vector3(0, -10 , 0);

		}
	
	}
}
