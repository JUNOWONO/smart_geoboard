using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class cur1_btn_event : MonoBehaviour {
	//cur1~3 여기서 처리 
	public EventManager eventManager;
	public Data data;
	public GameObject[] bars;
	public GameObject[] btns;
	private int lastBtnNum = 0; //이전 클릭에서 버튼 번호
	private int thisBtnNum = 0;
	private int btnClickCount =0;
	private int clearCount = 0; //클리어 카운트가 6이면 클리어로 인식하면 됨.
	private int eve;
	public int ClearCount_;
	private GameObject thisBtn;


	// Use this for initialization
	void Start () {

		data.LoadData ();
		if (bars.Length >= 1) {
			for (int i =1; i < bars.Length; i++) {
				bars [i].SetActive (false);
			}
		}
		eve = data.dataArr [0].curNum;

		if (eve == 8) {
			for(int i =2; i < btns.Length; i++)
				btns[i].SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (clearCount == ClearCount_) {

			if (eve == 1 || eve == 2) { //커리 1~2일때
				for (int i =1; i <= ClearCount_; i++) {
					btns [i * 2].SetActive (false);
					btns [i * 2 - 1].SetActive (false);
				}
			}
			if (Input.GetMouseButton (0)) { 
				if(eve != 24 && eve != 18) GameObject.Find ("Canvas/events/cur" + eve + "/btns").SetActive (false);
				else {
					if(eventManager.getMiniEvent() == 3)
						eventManager.NextMiniEvent();
				}
			}
		}
		 
	}

	public void OnClick()
	{	
		thisBtn = EventSystem.current.currentSelectedGameObject;
		btnClickCount++;
		thisBtnNum = int.Parse (thisBtn.name);

		if (eve == 1 || eve == 2) { //커리 1~2일때

			thisBtn.SetActive (false); //클릭되면 버튼은 숨김
			if (btnClickCount == 2) {
				btnClickCount = 0;
				if (lastBtnNum == thisBtnNum) { //두개 연속으로 같은 번호의 버튼이 클릭된 경우
					bars [thisBtnNum].SetActive (true); //사다리의 막대기를 그림.
					clearCount++;
				}
				for (int i =1; i <= ClearCount_; i++) {
					if (bars [i].activeSelf == false) {
						btns [i * 2].SetActive (true);
						btns [i * 2 - 1].SetActive (true);
					}
				}
			}
		} else if (eve == 3) { //커리3일때 

			thisBtn.SetActive (false); //클릭되면 버튼은 숨김
			if (btnClickCount == 2) {
				btnClickCount = 0;
				if (thisBtnNum == 1 & lastBtnNum == 4 || thisBtnNum == 4 & lastBtnNum == 1) {
					bars [1].SetActive (true);
					clearCount++;
				} else if (thisBtnNum == 2 & lastBtnNum == 3 || thisBtnNum == 3 & lastBtnNum == 2) {
					bars [2].SetActive (true);
					clearCount++;
				} else if (thisBtnNum == 3 & lastBtnNum == 6 || thisBtnNum == 6 & lastBtnNum == 3) {
					bars [3].SetActive (true);
					clearCount++;
				} else if (thisBtnNum == 4 & lastBtnNum == 5 || thisBtnNum == 5 & lastBtnNum == 4) {
					bars [4].SetActive (true);
					clearCount++;
				} else if (thisBtnNum == 5 & lastBtnNum == 8 || thisBtnNum == 8 & lastBtnNum == 5) {
					bars [5].SetActive (true);
					clearCount++;
				} else if (thisBtnNum == 6 & lastBtnNum == 7 || thisBtnNum == 7 & lastBtnNum == 6) {
					bars [6].SetActive (true);
					clearCount++;
				}

				for (int i = 1; i <= ClearCount_; i++) {
					if (bars [i].activeSelf == false) {
						if (i == 1 || i == 3 || i == 5) {
							btns [i].SetActive (true);
							btns [i + 3].SetActive (true);
						} else {
							btns [i].SetActive (true);
							btns [i + 1].SetActive (true);
						}
					}
				}
			}


		} else if (eve == 8) { // cur8 (2-2)
			thisBtn.GetComponent<Image>().sprite =  Resources.Load<Sprite> ("CurImages/cur" + eve + "/active");

			if(clearCount == 0){
				if( thisBtnNum != lastBtnNum) clearCount++;
			} else {
				if( thisBtnNum != lastBtnNum && thisBtnNum != 1) clearCount++;
			}


			if(clearCount == 1) {
				for(int i =2; i < btns.Length; i++)
					btns[i].SetActive(true);
				
			}

			if(clearCount >=3) {
				bars[lastBtnNum-1].SetActive(true);
				bars[thisBtnNum-1].SetActive(true);
			}
			lastBtnNum = thisBtnNum;



		}else if(eve == 9){
			thisBtn.SetActive (false); //클릭되면 버튼은 숨김
			clearCount++;

		}else if (eve == 14){ //3-2일때  
			if (thisBtnNum != 9) {
				thisBtn.SetActive (false); //클릭되면 버튼은 숨김(가운데 점은 안숨김)
			}
			if (btnClickCount == 2) {
				btnClickCount = 0;
				if(lastBtnNum == 9){
					if(thisBtnNum != 9){
						btns[thisBtnNum].SetActive(false);
						bars[thisBtnNum].SetActive(true);
						clearCount++;
					}
				} else{ // 저번에 눌린 버튼이 9가 아니면
					if(thisBtnNum == 9){
						btns[lastBtnNum].SetActive(false);
						bars[lastBtnNum].SetActive(true);
						clearCount++;
					}
				}
				for(int i =1; i< bars.Length; i++){
					if(bars[i].activeSelf == false){
						btns[i].SetActive(true);
					}
				}
			}

		}else if(eve == 18){
			thisBtn.GetComponent<Image>().sprite =  Resources.Load<Sprite> ("CurImages/cur" + eve + "/" + thisBtnNum);
			clearCount++;
			if (clearCount >= ClearCount_)
				eventManager.NextMiniEvent();
		} else if(eve == 24){
			thisBtn.GetComponent<Image>().sprite =  Resources.Load<Sprite> ("CurImages/cur" + eve + "/" + (7+thisBtnNum));
			clearCount++;
			if (clearCount >= ClearCount_)
				eventManager.NextMiniEvent();
		}else if (eve == 27) { //cur27일때 (5-3)

			if(clearCount < 4) {  //clearCount가 4보다 작을때
				if(thisBtnNum == 1) {
					thisBtn.SetActive (false);
					bars[1].SetActive(true);
					clearCount++;
				} else if( thisBtnNum == 2) {
					thisBtn.SetActive (false);
					bars[2].SetActive(true);
					clearCount++;
				} else if( thisBtnNum == 5) {
					thisBtn.SetActive (false);
					bars[5].SetActive(true);
					clearCount++;
				} else if( thisBtnNum == 6) {
					thisBtn.SetActive (false);
					bars[6].SetActive(true);
					clearCount++;
				}
			} else if(clearCount == 4) { //4일때
				if(thisBtnNum == 4) {
					thisBtn.SetActive (false);
					bars[5].SetActive(false);
					bars[4].SetActive(true);
					clearCount++;
				}
			} else {  //5이상일때
				if(thisBtnNum == 8) {
					thisBtn.SetActive (false);
					bars[9].SetActive(true);
					clearCount++;
				} else if(thisBtnNum == 9) {
					thisBtn.SetActive (false);
					bars[8].SetActive(true);
					clearCount++;
				} else if(thisBtnNum == 10) {
					thisBtn.SetActive (false);
					bars[7].SetActive(true);
					clearCount++;
				}else if(thisBtnNum == 11) {
					thisBtn.SetActive (false);
					bars[10].SetActive(true);
					clearCount++;
					clearCount++;

				}

				if (Input.GetMouseButton (0)) {
					clearCount++;
				}
			}

		}

		lastBtnNum = thisBtnNum;
	}



}
