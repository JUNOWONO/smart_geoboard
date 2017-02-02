using UnityEngine;
using System.Collections;

public class GravityOnAfterFound : MonoBehaviour {


	public Data data;
	// Use this for initialization
	void Start () {
		data.LoadData ();
		Physics.gravity = Vector3.zero;
	
	}
	
	// Update is called once per frame
	void Update () {
		//현재 커리큘럼 넘버의 isSuccess값이 true 이면 중력을 활성화
		if(data.dataArr[data.dataArr[0].curNum].isSuccess == true) 
			Physics.gravity = new Vector3(0,0,-19.620f);
	
	}
}
