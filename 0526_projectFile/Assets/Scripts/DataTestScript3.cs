using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataTestScript3 : MonoBehaviour {

	public Data data;
	public Text textBox ;


	void Update()
	{
		data.LoadData ();
/*		textBox.text = "지금 커리큘럼의 누적경과시간 : " + data.dataArr [data.dataArr [0].curNum].elapsedTime + 
			"\n data.dataArr[0].curNum값 : " + data.dataArr [0].curNum 
			+ "\n data.dataArr[현재커리큘럼넘버].isSuccess : " + data.dataArr [data.dataArr [0].curNum].isSuccess 
			+ "\n 현재 경과 시간 : " + data.dataArr [0].elapsedTime; 
*/
	}
}
