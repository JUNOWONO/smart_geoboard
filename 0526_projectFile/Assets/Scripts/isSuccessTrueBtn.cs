using UnityEngine;
using System.Collections;

public class isSuccessTrueBtn : MonoBehaviour {

	public Data data;
	public EventManager eventManager;
	// Use this for initialization
	void Start () {
		data.LoadData ();
	}
	
	public void OnClick()
	{
		if (data.dataArr [data.dataArr [0].curNum].isSuccess == false) {
			data.dataArr [data.dataArr [0].curNum].isSuccess = true;
			eventManager.objects[data.dataArr [0].curNum % 3 + 8].SetActive(true);//effect
			data.SaveData();
		}
	}
}
