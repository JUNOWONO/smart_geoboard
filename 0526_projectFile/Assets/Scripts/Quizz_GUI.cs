using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quizz_GUI : MonoBehaviour {

	public Sprite image01;
	public Sprite image02;
	public Sprite image03;
	public Sprite image04;
	public GameObject quizzBackground;
	public Button successButton;

	public Button q1;
	public Button q2;
	public Button q3;
	public Button q4;


	public Data data;


	private int rand = 0;
	private Text mText;

	private int width;
	private int height;

	private Sprite ansSprite;
	private bool btnflag = false;
	private int curNum;


	// 학습 커리큘럼에 맞춰서 

	void Start () {
		data.LoadData ();

		curNum = data.dataArr [0].curNum; //현재 진행 중인 커리큘럼의 넘버 (1~30)

		mText = (Text)GameObject.Find ("cText").GetComponent (typeof(Text));

		Screen.SetResolution (Screen.width, Screen.height, true);
		width = Screen.width;
		height = Screen.height;

		quizzBackground.GetComponent<RawImage>().texture = Resources.Load<Texture> ("CurImages/cur" + curNum + "/quizz");
		rand = Random.Range (1, 5);
		if (rand == 1) {
			image01 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q1");
			image02 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q2");
			image03 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q3");
			image04 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q4");
		}
		if (rand == 2) {
			image01 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q2");
			image02 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q3");
			image03 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q4");
			image04 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q1");
		}
		if (rand == 3) {
			image01 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q3");
			image02 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q4");
			image03 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q1");
			image04 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q2");
		}
		if (rand == 4) {
			image01 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q4");
			image02 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q1");
			image03 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q2");
			image04 = Resources.Load<Sprite> ("CurImages/cur" + curNum + "/q3");
		}

		ansSprite = Resources.Load<Sprite> ("Textures/a1");
		successButton.GetComponent<Image>().sprite = ansSprite;

		q1.GetComponent<Image> ().sprite = image01;
		q2.GetComponent<Image> ().sprite = image02;
		q3.GetComponent<Image> ().sprite = image03;
		q4.GetComponent<Image> ().sprite = image04;
	
		successButton.gameObject.SetActive (false);


	}

	public void q1Click(int btnNum ){
		if (rand == 1) {
			mText.text = "정답입니다~";
			btnflag = true;
			successButton.gameObject.SetActive (true);
		} else mText.text = "다시 생각해 보세요~";
	}
	public void q2Click(int btnNum ){
		if (rand == 4) {
			mText.text = "정답입니다~";
			btnflag = true;
			successButton.gameObject.SetActive (true);
		} else mText.text = "다시 생각해 보세요~";
	}
	public void q3Click(int btnNum ){
		if (rand == 3) {
			mText.text = "정답입니다~";
			btnflag = true;
			successButton.gameObject.SetActive (true);
		} else mText.text = "다시 생각해 보세요~";
	}
	public void q4Click(int btnNum ){
		if (rand == 2) {
			mText.text = "정답입니다~";
			btnflag = true;
			successButton.gameObject.SetActive (true);
		} else mText.text = "다시 생각해 보세요~";
	}
	public void onSuccessBtn()
	{
		Application.LoadLevel("Result"); //일단은 바로 결과창으로 (나중에 중간에 뭔가 추가?
	}

	void Update(){

	}




	//이미지 q1이 항상 정답이 됨. 이미지의 위치는 랜덤하게 바뀜 
	// 정담과 오답의 이벤트는 밑의 OnGUI함수에서 관리해주면 됨.
	/*
	void OnGUI () {

		if (btnflag == false) { //정답을 맞추면 버튼들 사라짐!

			GUI.DrawTexture (new Rect (0, 0, width, height), quizzBackground);



			if (GUI.Button (new Rect (width / 2 + 25, 5 + 110, width / 2 - 20 -30, height / 2 - 10 -110), image01) ) {
			
				if (rand == 1) { //각각의 경우에 대해서 정/오답 체크
					mText.text = "정답입니다~";
					rand = 0; //정답인 경우 rand를 0으로 바꾸고, 새로운 이미지가 그려지게 함
					btnflag = true;
				} else {
					mText.text = "다시 생각해 보세요~";
				}
			}
			if (GUI.Button (new Rect (5 + 25, 5+ 110, width / 2 - 20 -30, height / 2 - 10-110), image02)) {

				if (rand == 4) {
					mText.text = "정답입니다~";
					rand = 0;
					btnflag = true;
				} else {
					mText.text = "다시 생각해 보세요~";
				}
			}
			if (GUI.Button (new Rect (5 + 25, height / 2 + 30, width / 2 - 20 -30, height / 2 - 10-110), image03)) {

				if (rand == 3) {
					mText.text = "정답입니다~";
					rand = 0;
					btnflag = true;
				} else {
					mText.text = "다시 생각해 보세요~";
				}
			}
			if (GUI.Button (new Rect (width / 2  + 25, height / 2 + 30, width / 2 - 20 -30, height / 2 - 10-110), image04)) {

				if (rand == 2) {
					mText.text = "정답입니다~";
					rand = 0;
					btnflag = true;
				} else {
					mText.text = "다시 생각해 보세요~";
				}
			}
		
		} else {
			StartCoroutine (Delay ());
			if(btnflag == true && Input.GetMouseButtonDown(0)==true)
			Application.LoadLevel("Result"); //일단은 바로 결과창으로 (나중에 중간에 뭔가 추가
		}
	
		//가장 앞에 나와야 하므로 맨 아래다 씀
			if (rand == 0 && btnflag == true) {
			GUI.DrawTexture (new Rect (width / 4, height / 4, width / 2, height / 2), ansTexture);
		}
	
	}

	IEnumerator Delay() { 
		yield return new WaitForSeconds(10.0f); 
	}
*/

}
