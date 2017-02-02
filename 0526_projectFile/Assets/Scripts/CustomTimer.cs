using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomTimer : MonoBehaviour {

	//public GUIText TimerText;
	public Text TimerText ;
	public float time ;
	public string timeText;
	public Data data;


	// Use this for initialization
	void Start () {
		//TimerText = GetComponent<GUIText> ();

        time = 0.0f;
        timeText = "";
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timeText = "" + time.ToString ("00.00");
		//timeText = timeText.Replace (".", ":");
		if(TimerText != null)
          TimerText.text = timeText;

		data.LoadData ();
		if (data.dataArr [data.dataArr [0].curNum].isSuccess == true) 
		{
//			data.dataArr [data.dataArr [0].curNum].elapsedTime = data.dataArr [data.dataArr [0].curNum].elapsedTime +  (int)time;
			data.SaveData();
		}

	}

	/*
	void OnGUI(){
		string timeText;
		timeText = "" + time.ToString ("00.00");
		timeText = timeText.Replace (".", ":");
		TimerText.text = "Time: " + timeText;
	}
	*/

}
