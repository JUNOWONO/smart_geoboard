using UnityEngine;
using System.Collections;

public class Fixer : MonoBehaviour {
	
	private Transform Tr;
	private Vector3 OriginPos;
	
	// Use this for initialization
	void Start () {
		Tr = GetComponent<Transform>();
		OriginPos = Tr.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Tr.position = OriginPos;
	}
}
