using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Rotator : MonoBehaviour {


	public Data data;
	private int eve;

	float timer;
	int waitingTime;
	public float speed = 15f;
	public bool play = false;

	public float x = 0;
	public float y = 0;
	public float z = 1;

	float sum;

	// Use this for initialization
	void Start () {
		data.LoadData ();
		eve = data.dataArr [0].curNum;
		//timer = 0.0f;
		//waitingTime = 3;
	}



	void Update () 
	{	
	//	timer += Time.deltaTime;
	//	if(timer > waitingTime)
	//	{
	//		timer = 0;
	//	}
		if (play == true) {
			this.transform.Rotate (new Vector3 (y, x, z) * speed * Time.deltaTime);
			sum += speed * Time.deltaTime;
		}
		if (eve == 24) {
			if(sum >= 180f) {
				play = false;
				sum = 0f;
			}
		}
	}


	public void OnClick(){
			
		if (play == true)
			play = false;
		else
			play = true;
	}

}
