using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataTestScript : MonoBehaviour {
//이 스크립트에서는 데이터 값의 변화가 일어남.
	public Data data;
	public Text textBox ;
	public int testNum;

	// Use this for initialization
	void Start () {
		int time = 0;
//		data.dataArr [1].elapsedTime = testNum; //이 부분에서 1번 커리큘럽의 경과시간(elapsedTime)의 값이 변함
//		time = data.dataArr [1].elapsedTime;
		textBox.text = time.ToString();
		data.SaveData (); //여기서 SaveData()를 통해서 변화된 값을 저장함.(다음에 LoadData()를 통해서 불러 올 수 있음)
	
	}
	

}
