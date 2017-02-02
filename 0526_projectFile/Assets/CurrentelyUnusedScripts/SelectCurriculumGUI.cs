using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCurriculumGUI : MonoBehaviour {

	public Data data;

	private Texture[] ButtonImages = new Texture[30];

	private int width = Screen.width;
	private int height = Screen.height;

	// Use this for initialization
	void Start () {

		data.LoadData (); //데이터 로드 (데이터 값을 변경시킨 후에는 data.SaveData 해주기)

		for (int i = 0; i <=29; i++)
		{
			ButtonImages[i] = Resources.Load<Texture> ("Buttons/b"+(i+1));
		}
	
	}
	
	// Update is called once per frame
	void OnGUI () {

		int row = 0;

		for (int i = 0; i <=29; i++) 
		{
			row = i/5;


			if (GUI.Button (new Rect ((width / 14) + (i%5)* (width / 28 + width/7) ,(height / 5) + row* (height / 50 + height/10), width / 7, height/10 ) , ButtonImages [i]))
			{	// 인덱스에 따라 독립적으로 무언가를 실행 시키고 싶을 때에는 if문을 사용
				data.dataArr[0].curNum = i+1; // 버튼이 눌렸을 때 해당 커리큘럼 넘버를 저장
				data.dataArr [data.dataArr [0].curNum].isSuccess = false; 
				data.SaveData ();
				Application.LoadLevel("CustomUI");
				Application.LoadLevelAdditive("Main");
		//		Application.LoadLevelAdditive("TextBox_text");
			}
		}
	}




}
