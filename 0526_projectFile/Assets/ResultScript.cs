using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScript : MonoBehaviour {

	public Data data;
	public GameObject resultImageObject;
	public GameObject backGround;

	private bool clickFlag;
	private int resultImageNum;
	private int curNum;
	private int miniEvent;

	// Use this for initialization
	void Start () {
		data.LoadData ();
		resultImageNum = 1;
		clickFlag = false;
		miniEvent = 1;
		curNum = data.dataArr [0].curNum;
		backGround.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + data.dataArr [0].curNum);
	}
	
	// Update is called once per frame
	void Update () {
		if (miniEvent == 1) {
			if (clickFlag == true) { //지오지오가 클릭되면
				// 해당 번호의 리소스가 null이 아닐때
				resultImageObject.GetComponent<Image> ().sprite
					= Resources.Load<Sprite> ("CurImages/cur" + curNum + "/result" + miniEvent);
				//= Resources.Load<Sprite> ("CurImages/cur" + curNum + "/result" + resultImageNum);
				miniEvent ++;
				resultImageNum ++;
			} 
		} else if (miniEvent == 2) {
			if (clickFlag == false) {//지오지오가 다시 클릭되면
				//다음 번호의 결과이미지가 리소스에 존재할 때
				if (Resources.Load<Sprite> ("CurImages/cur" + curNum + "/result" + miniEvent) != null) {
					resultImageObject.GetComponent<Image> ().sprite
						= Resources.Load<Sprite> ("CurImages/cur" + curNum + "/result" + miniEvent);

					//= Resources.Load<Sprite> ("CurImages/cur" + curNum + "/result" + resultImageNum);//힌트 이미지 띄우기
					miniEvent++;
				} else {
					if (clickFlag == true) {
						resultImageObject.SetActive (false);
						miniEvent++;
					}
				}
		} else if (miniEvent == 3) {
			if (clickFlag == true) {
				resultImageObject.SetActive (false);
			}
		}
		}

	}
	public void OnClick(){
		if(clickFlag == false) clickFlag = true;
		else if (clickFlag ==true) clickFlag = false;
	}
}
