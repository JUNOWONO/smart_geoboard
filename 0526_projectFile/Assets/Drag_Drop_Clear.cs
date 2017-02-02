using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Drag_Drop_Clear : MonoBehaviour {

	//public Sprite[] dragImgs; //드래그 할 이미지 (순서대로 drag image들의 배열)
	public Image[] dropImgs; //드랍될 이미지 (순서대로 drop image들의 배열)
	public int numOfPieces; //몇개의 퍼즐이 맞아야 정답인지
	public Data data;
	public EventManager eventManager;

	public int eve;
	public int clearCount; // DropMe 스크립트에서 조건을 만족하면 ++됨;
	public int curNum;
	private GameObject panels;
	int flag = 0;

	// Use this for initialization
	void Start () {
		data.LoadData ();
		clearCount = 0;
		eve = data.dataArr [0].curNum;
		if (eve != 22 && eve != 29 && eve != 30  && eve != 23) {
			for (int i = 1; i <= numOfPieces; i++) {
				dropImgs [i].sprite = Resources.Load<Sprite> ("Textures/null");
			}
		}
		curNum = eve;

		panels = GameObject.Find ("Canvas/events/cur" + eve + "/panels");
		
	}
	
	// Update is called once per frame
	void Update () {
	

		if (numOfPieces == clearCount) {
			if (eve == 25 || eve == 26 || eve == 29 || eve == 30){
				if(eventManager.getMiniEvent() == 3) eventManager.NextMiniEvent();
			} else{
				panels.SetActive (false);
			}

		}

		if (eve == 26) {
			if (clearCount <= 5) {
				if (flag == 0) {
					dropImgs [7].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [8].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [9].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [10].GetComponentInParent<Image> ().raycastTarget = false;
					flag ++;
				}
			} else if (clearCount >= 6 && clearCount <= 8) {
				if (flag == 1) {
					dropImgs [1].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [2].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [3].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [4].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [5].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [6].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [7].GetComponentInParent<Image> ().raycastTarget = true;
					dropImgs [8].GetComponentInParent<Image> ().raycastTarget = true;
					dropImgs [9].GetComponentInParent<Image> ().raycastTarget = true;
					flag ++;
				}
			} else if (clearCount == 9) {
				if (flag == 2) {
					dropImgs [7].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [8].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [9].GetComponentInParent<Image> ().raycastTarget = false;
					dropImgs [10].GetComponentInParent<Image> ().raycastTarget = true;
				}
			}
		} else if (eve == 29) {
			if(flag == clearCount){
				dropImgs[1].GetComponent<Image>().sprite =  Resources.Load<Sprite> ("CurImages/cur29/" + clearCount);
				flag++;
			}
		} else if (eve == 30) {
			if(flag == clearCount){
				dropImgs[1].GetComponent<Image>().sprite =  Resources.Load<Sprite> ("CurImages/cur30/" + clearCount);
				flag++;
			}
		}

	}
}
