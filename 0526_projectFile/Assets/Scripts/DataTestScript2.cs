using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataTestScript2 : MonoBehaviour {
//이 스크립트는 데이터 로드 예시. 
// 이 스크립트에서는 값의 변경 없이 LoadData를 통해서 기존의 데이터를 불러옴

	public Data data;
	public Text textBox ;
	// Use this for initialization
	void Start () {
		data.LoadData ();
		int time = 0;
//		time = data.dataArr [1].elapsedTime; // 1번 커리큘럼의 elapsedTime(경과시간)을 가져온다.
		textBox.text = "경과시간 : " + time.ToString() ; 



	
	}

}
