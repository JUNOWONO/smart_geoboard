using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCurriculumHandler : MonoBehaviour {


	private int width;
	private int height;

	private int curChapter; //현재 챔터 번호 (1번~5번)
	private int previousChapter; //이전 쳅터 번호
	private int selectedCurriculum;//선택된 커리뮬럼 넘버 (1~30번)

	public Data data;

	public Text text;

	public GameObject[] chapButns;


	// Use this for initialization
	void Start () {
		data.LoadData (); //데이터 로드 (데이터 값을 변경시킨 후에는 data.SaveData 해주기)
		width = Screen.width;
		height = Screen.height;
	
		curChapter = data.dataArr [0].curNum / 6 + 1;
		if (curChapter > 4)
			curChapter = 5;
		previousChapter = 0;

		for (int i=1; i <= 5; i++){
			chapButns[i].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/Btn_Normal");
		}
		chapButns[curChapter].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/Btn_Active");

	}
	void Update()
	{
		

		if (previousChapter != curChapter) {
			GameObject.Find ("Canvas/Background").GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + curChapter);
			GameObject.Find ("Canvas/Title").GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_title" + curChapter);
			for(int i = 1; i <= 6; i++){
				GameObject.Find ("Canvas/b"+i).GetComponent<Image>().sprite =  Resources.Load<Sprite> ("Textures/stage" + curChapter + "button" + i);
			}
		}


		//text.text = "selectedCur : " + selectedCurriculum + "\n curChap : " + curChapter + "\n previousChap : " +  previousChapter;
		

	}
	


	public void toNextChapter() //다음 챕터 버튼 이벤트
	{
		data.LoadData ();
		previousChapter = curChapter;
		curChapter++;
		if (curChapter >= 6){
			curChapter = curChapter%5;
		}
		for (int i=1; i <= 5; i++){
			chapButns[i].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/Btn_Normal");
		}
		chapButns[curChapter].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/Btn_Active");
		data.SaveData ();	
	}
	
	public void toPreviousChapter() //이전 챕터로 버튼 이벤트 
	{
		data.LoadData ();
		previousChapter = curChapter;
		curChapter--;
		if (curChapter < 1){
			curChapter = 5;
		}
		for (int i=1; i <= 5; i++){
			chapButns[i].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/Btn_Normal");
		}
		chapButns[curChapter].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/Btn_Active");
		data.SaveData ();
	}

	public void OnClicked(Button button) // 커리큘럼 선택 버튼 처리 
	{
		data.LoadData ();
		int btnNum = 0;
		if (button.name.Equals ("b1")) {
			btnNum = 1;
		}else if (button.name.Equals("b2")){
			btnNum = 2;
		}else if (button.name.Equals("b3")){
			btnNum = 3;
		}else if (button.name.Equals("b4")){
			btnNum = 4;
		}else if (button.name.Equals("b5")){
			btnNum = 5;
		}else if (button.name.Equals("b6")){
			btnNum = 6;
		}

		selectedCurriculum = btnNum + 6 * (curChapter-1);

		data.dataArr[0].curNum = selectedCurriculum; // 버튼이 눌렸을 때 해당 커리큘럼 넘버를 저장
		data.dataArr [selectedCurriculum].isSuccess = false; 
		data.SaveData ();

		//Application.LoadLevel("CustomUI"); //다음 씬으로 이동
		Application.LoadLevel("Vuforia");

	}

}
