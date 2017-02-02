using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System.Collections;

public class EventManager : MonoBehaviour {

	const int EVENT_SIZE = 5;
	bool[] events;
	private string curEvent;
	private bool isStart;
	public GameObject[] objects;
	bool eventChanged = false;



	public Data data;
	public ImageTargetsOnOff imageTargetssOnOff;

	int level; 

	static public int eve; // 현재 event index
	static public int miniEvent; // 이벤트 속의 상태 표현


	private bool isTextBoxOpened = false;
	private bool ifLoadText = false;

	private bool clickFlag = false;
	private int chapNum;

	//저장이 필요한 변수
	/*
	 * 현재 실행 중인 커리큘럼 번호 numCurr => 근데 텍스트 파일 인덱스랑 같음
	 * 	L 현재 커리큘럼 학습 시간 studyTime
	 *	L 현재 커리큘럼 지시 사항 이행 유무 studyPerform
	 *	L 현재 커리큘럼 퀴즈 정답 유무 quizRight
	 *	L 현재 커리큘럼 학습 횟수 studyCount
	 *	L 현재 커리큘럼 학습 시간 (종합) total_studyTime
	 * 	L 현재 커리큘럼 퀴즈 정답률 total_quizRight
	 *	L 
	 *
	 * 사용자 종합 데이터
	 * 	L 사용자 이름(아이디) userName
	 * 	L 사용자 학습시간 total_studyTime
	 * 	L 사용자 퀴즈 정답률 total_quizRight
	 * 	L 사용자 총 학습 횟수 total_studyCount
	 * 	L 
	 * 	L 
	 */

	// Use this for initialization
	void Awake () {



		//StartCoroutine( LoadScene() );

		level = Application.loadedLevel;
		data.LoadData ();
		eve = data.dataArr [0].curNum; // 현재 커리큘럼 넘버를 받아와서 eve에 넣어줌
		chapNum = eve / 6 + 1;
		if (eve % 6 == 0)
			chapNum--;
		miniEvent = 0;

		isStart = true;
		LoadObjects();


	
	}

	// Update is called once per frame
	void Update () {
		//reloadText (); //텍스트 파일에서 텍스트 읽어두기
		//Image drawBoard = GameObject.Find ("Canvas/events/drawBoard").GetComponent<Image> ();


		if (eve == 1 || eve == 9 || eve == 24 || eve == 18 ) { //커리큘럼 1-1이랑 2-3 , 4-6, 3-6 

			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			}else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					//Application.UnloadLevel("Main"); //카메라 인식 씬 끄고
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 3) {
				objects [10 + eve].SetActive (true); //11번은 cur1번 
				objects [6].SetActive (true); // hint image active
				objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");
				miniEvent++;
			} else if (miniEvent == 4) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/btns").activeSelf == false) {
					if(eve == 1) {
						GameObject.Find ("Canvas/events/cur1/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur1/done");
					} 
					miniEvent++;
				}

			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
				
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}

		} else if (eve == 2 || eve == 14) { //커리큘럼 2 랑 14(3-2)
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //12번은 cur2번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/btns").activeSelf == false) {
					if(eve == 2)
						GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete");
					miniEvent++;
				}
				
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done");
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
			
			} else if (miniEvent == 5) {
				Application.LoadLevel ("SelectCurriculum");
			
			}

		} else if (eve == 3) { //커리큘럼 3
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 

					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //13번은 cur3번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/btns").activeSelf == false) {
					GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete");
					miniEvent++;
				}
				
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done");
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
				
			} else if (miniEvent == 5) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		} else if (eve == 4) { //커리큘럼 4
				
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //14번은 cur4번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete");
				//여기에 나중에 스프라이트 애니메이션 추가하기  
		
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
					
			}
		} else if (eve == 5 ||eve == 6 || eve == 7) { //커리큘럼 5 6 7
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done");

				
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		}  else if (eve == 8) { //커리큘럼 8 (2-2)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //13번은 cur3번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/btns").activeSelf == false) { //조건이 만족되어 버튼들이 사라지면
					GameObject.Find ("Canvas/events/cur" + eve + "/image2").SetActive(false);
					miniEvent++;
				}
				
			} else if (miniEvent == 4) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
				
			} else if (miniEvent == 5) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		}else if (eve == 10) { //커리큘럼 10 (2-4)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done");
				
				
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		} else if (eve == 17 || eve == 15 || eve == 16) { //커리큘럼 17 (3-5) // 커리 15 (3-3) //커리 16
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					GameObject.Find ("ARCamera").SetActive (false);
					objects [10 + eve].SetActive (true); 
					if (eve != 15)
						objects [1].SetActive (false);
					else
						objects [1].SetActive (true);
					//GameObject.Find ("Canvas").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 3) {
				// add direcion images

			} else if (miniEvent == 4) {
				objects [10 + eve].SetActive (false);

				if(eve == 15 || eve == 17 || eve == 16) Application.LoadLevel ("SelectCurriculum");
				else GameObject.Find ("Canvas").SetActive (true);
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		} else if (eve == 19 || eve ==11 || eve == 12 || eve ==13 || eve == 25 || eve == 26 || eve == 29 || eve == 30) { //커리큘럼 19 (4-1) 랑 11 (2-5) 랑 12 (2-6) 랑 13(3-1) 25(5-1) 랑 26(5-2)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {

				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				if(eve == 13) {
					GameObject.Find ("Canvas/events/cur" + eve + "/Btn").GetComponent<Image>().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/1");
				} else if(eve == 25 || eve == 29 || eve == 30){

				}else {
					GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done");
				}
				//GameObject.Find ("Canvas/events/cur" + eve + "/image").AddComponent<Rotator>();
				
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		}else if (eve == 20) { //커리큘럼 20 (4-2)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image1/image_cup").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete1");
				GameObject.Find ("Canvas/events/cur" + eve + "/image2").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete2");
				
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		} else if (eve == 21) { //커리큘럼 21 (4-3)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //14번은 cur4번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");


					miniEvent++;
				}
			} else if (miniEvent == 3) {
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete");
				for(int i=1; i <= 6; i++) {
					GameObject.Find ("Canvas/events/cur" + eve + "/" + i).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/" + i);
				}
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		}else if (eve == 22) { //커리큘럼 21 (4-4)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //14번은 cur4번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");


					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete");
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		}else if (eve == 23) { //커리큘럼 23 (4-5)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);

					objects [10 + eve].SetActive (true); //14번은 cur4번 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/complete");
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		} else if (eve == 27) { //커리큘럼 27 (5-3)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);

					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); //12번은 cur2번 

					miniEvent++;
				}
			} else if (miniEvent == 3) {
				
				if (GameObject.Find ("Canvas/events/cur" + eve + "/btns").activeSelf == false) {
					miniEvent++;
				}
				
			} else if (miniEvent == 4) {
				objects[41].SetActive(true);
				objects [1].SetActive (true);
				objects [2].SetActive (false);

			
			} else if (miniEvent == 5) {
				
				Application.LoadLevel ("SelectCurriculum");

			}
			
		}else if (eve == 28) { //커리큘럼 28 (5-4)
			
			if (miniEvent == 0) {
				data.LoadData ();
				objects [10].SetActive (true);
				imageTargetssOnOff.ImageTargetOn ();
				miniEvent++;
			} else if (miniEvent == 1) {
				if (data.dataArr [eve].isSuccess == true) { //인식에 성공하면 
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					GameObject.Find ("Canvas/events/success").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/ans");
					miniEvent++;
				}
			} else if (miniEvent == 2) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
					objects [2].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stage_back" + chapNum);
					GameObject.Find ("ARCamera").SetActive (false);
					GameObject.Find ("Canvas/giveUp").SetActive (false);
					objects [10 + eve].SetActive (true); 
					objects [6].SetActive (true); // hint image active
					objects [6].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/direction");


					miniEvent++;
				}
			} else if (miniEvent == 3) {
								if (GameObject.Find ("Canvas/events/cur" + eve + "/panels").activeSelf == false) {
					miniEvent++;
				}	
			} else if (miniEvent == 4) {
				//GameObject.Find ("Canvas/events/cur" + eve + "/image").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done");
				//GameObject.Find ("Canvas/events/cur" + eve + "/image").AddComponent<Rotator>();
				GameObject.Find ("Canvas/events/cur" + eve + "/image1").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done1");
				GameObject.Find ("Canvas/events/cur" + eve + "/image2").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("CurImages/cur" + eve + "/done2");
				if (Input.GetMouseButton (0))
					miniEvent++;
			} else if (miniEvent == 5) {
				objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면					
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				Application.LoadLevel ("SelectCurriculum");
				
			}
		}
}
		
		/*
		if (eve == 1) { //커리큘럼 1
			if (miniEvent == 1) {
				data.LoadData ();


				OpenTextBox (); //텍스트 박스 안켜져 있으면 켜기

				GameObject.Find ("Canvas/events/drawBoard").SetActive (true); //드로우 보드 키고
				drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/cur1_intro");  //스프라이트를 인트로화면으로
				objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
				objects [2].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + eve);
				miniEvent++;

			} else if (miniEvent == 2) {
				if (textBoxManager.curLine == 4) { //텐스트 5번째 줄에서
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/geometry"); //스프라이트를 지오메트리로
				} else if (textBoxManager.curLine == 6) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/dot");
				} else if (textBoxManager.curLine == 7) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/dot_line");
				} else if (textBoxManager.curLine == 8) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/line_curve");
				} else if (textBoxManager.curLine == 9) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/face");
				} else if (textBoxManager.curLine == 10) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/faces");
				} else if (textBoxManager.curLine == 11) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur1/directions");
					miniEvent++;
				}
			} else if (miniEvent == 3) {
				if (!textBoxManager.textBox.activeSelf) { //텍스트 박스가 꺼지면
					objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"

					if (objects [1].activeSelf == false) { //버튼이 사라지면
						GameObject.Find ("Canvas/events/drawBoard").SetActive (false); //드로우 보드 비활성화
						miniEvent++;
					}
				}

			} else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/drawBoard").SetActive (false); //드로우 보드 비활성화
				objects [2].SetActive (false); //카메라를 덮었던 백그라운드 이미지 끄고 //UI Canvas/ScreenCover1
				GameObject.Find ("Canvas/events/Geogeo").SetActive (true); //지도지오 등장 (클릭하면 힌트를 줌)

				if (clickFlag == true) { //지오지오가 클릭되면
					objects [6].SetActive (true); //"UI Canvas/events/Geogeo/hintImage"
					objects [6].GetComponent<Image> ().sprite
						= Resources.Load<Sprite> ("CurImages/cur1/hint1");//힌트 이미지 띄우기
				} else {// 다시 클릭되면
					objects [6].SetActive (false);

				}

				if (data.dataArr [data.dataArr [0].curNum].isSuccess == true) { //인식에 성공하면 
					objects [6].SetActive (false); //힌트 이미지 비활성화
					GameObject.Find ("Canvas/events/Geogeo").SetActive (false);//지오지오 비활성화
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					miniEvent++;
				}
			} else if (miniEvent == 5) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					//Application.UnloadLevel("Main"); //카메라 인식 씬 끄고
					GameObject.Find ("ARCamera").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				objects [7].SetActive (true);//"OuterEvents/Oeve1"
				objects [1].SetActive (true); //다음으로 가기 버튼 생성//"UI Canvas/NextMiniEventButton"
				objects [2].SetActive (false);//백그라운드화면 제거
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
			} else if (miniEvent == 7) {

				Application.LoadLevel ("Quizz");
			} 
		} else if (eve == 2) { //커리큘럼 2---------------------------------------------------------------------
			if (miniEvent == 1) {
				data.LoadData ();
				
				OpenTextBox (); //텍스트 박스 안켜져 있으면 켜기
				
				GameObject.Find ("Canvas/events/drawBoard").SetActive (true); //드로우 보드 키고
				drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/point_line");  //스프라이트를 점과선 그림으로
				objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
				objects [2].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + eve);
				miniEvent++;

			} else if (miniEvent == 2) {
				if (textBoxManager.curLine == 2) { //텍스트 3번째 줄에서
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/howManyLines_point2");  // 점 하나를 지날 수 있는 선의 갯수
				} else if (textBoxManager.curLine == 4) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/line_2points1");
				} else if (textBoxManager.curLine == 5) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/straight_line");
				} else if (textBoxManager.curLine == 6) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/half_line");
				} else if (textBoxManager.curLine == 7) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/segment");
				} else if (textBoxManager.curLine == 8) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/shortest");
				} else if (textBoxManager.curLine == 9) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/parallel");
				} else if (textBoxManager.curLine == 10) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/meet_equal");
				} else if (textBoxManager.curLine == 11) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur2/directions");
					GameObject.Find ("Canvas/events/drawBoard").SetActive (true);
					miniEvent++;
				} 
			} else if (miniEvent == 3) {
				if (textBoxManager.textBox.activeSelf == false) { //텍스트 박스가 꺼지면
					objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"

					if (objects [1].activeSelf == false) { //버튼이 사라지면
						miniEvent++;
					}
				}
			}  else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/drawBoard").SetActive (false); //드로우 보드 비활성화
				GameObject.Find ("Canvas/events/Geogeo").SetActive (true); //지도지오 등장 (클릭하면 힌트를 줌)
				objects [2].SetActive (false); //카메라를 덮었던 백그라운드 이미지 끄고 //UI Canvas/ScreenCover1
				miniEvent++;
			} else if (miniEvent == 5){
				if (clickFlag == true) { //지오지오가 클릭되면
					objects [6].SetActive (true); //"UI Canvas/events/Geogeo/hintImage"
					objects [6].GetComponent<Image> ().sprite
						= Resources.Load<Sprite> ("CurImages/cur2/hint1");//힌트 이미지 띄우기
				} else {// 다시 클릭되면
					objects [6].SetActive (false);
				}

				if (data.dataArr [data.dataArr [0].curNum].isSuccess == true) { //인식에 성공하면 
					objects [6].SetActive (false); //힌트 이미지 비활성화
					GameObject.Find ("Canvas/events/Geogeo").SetActive (false);//지오지오 비활성화
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					GameObject.Find ("ARCamera").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 7) {
				objects [8].SetActive (true);//"OuterEvents/Oeve2"
				objects [1].SetActive (true); //다음으로 가기 버튼 생성//"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
			} else if (miniEvent == 8) {
				
				Application.LoadLevel ("Quizz");
			} 
		} else if (eve == 3) { //커리큘럼 3---------------------------------------------------------------------
			if (miniEvent == 1) {
				data.LoadData ();
				
				OpenTextBox (); //텍스트 박스 안켜져 있으면 켜기
				
				GameObject.Find ("Canvas/events/drawBoard").SetActive (true); //드로우 보드 키고
				objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
				objects [2].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + eve);
				miniEvent++;
				
			} else if (miniEvent == 2) {
				if (textBoxManager.curLine == 0) { //텍스트 3번째 줄에서
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/00"); 
				} else if (textBoxManager.curLine == 2) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/01");
				} else if (textBoxManager.curLine == 4) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/02");
				} else if (textBoxManager.curLine == 6) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/03");
				} else if (textBoxManager.curLine == 7) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/04");
				} else if (textBoxManager.curLine == 8) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/05");
				} else if (textBoxManager.curLine == 10) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/06");
				} else if (textBoxManager.curLine == 12) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/07");
				} else if (textBoxManager.curLine == 14) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur3/directions");
					miniEvent++;
				} 
			} else if (miniEvent == 3) {
				if (textBoxManager.textBox.activeSelf == false) { //텍스트 박스가 꺼지면
					objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
					
					if (objects [1].activeSelf == false) { //버튼이 사라지면
						miniEvent++;
					}
				}
			}  else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/drawBoard").SetActive (false); //드로우 보드 비활성화
				GameObject.Find ("Canvas/events/Geogeo").SetActive (true); //지도지오 등장 (클릭하면 힌트를 줌)
				objects [2].SetActive (false); //카메라를 덮었던 백그라운드 이미지 끄고 //UI Canvas/ScreenCover1
				miniEvent++;
			} else if (miniEvent == 5){
				if (clickFlag == true) { //지오지오가 클릭되면
					objects [6].SetActive (true); //"UI Canvas/events/Geogeo/hintImage"
					objects [6].GetComponent<Image> ().sprite
						= Resources.Load<Sprite> ("CurImages/cur3/hint1");//힌트 이미지 띄우기
				} else {// 다시 클릭되면
					objects [6].SetActive (false);
				}
				
				if (data.dataArr [data.dataArr [0].curNum].isSuccess == true) { //인식에 성공하면 
					objects [6].SetActive (false); //힌트 이미지 비활성화
					GameObject.Find ("Canvas/events/Geogeo").SetActive (false);//지오지오 비활성화
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					GameObject.Find ("ARCamera").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 7) {
				objects [8].SetActive (true);//"OuterEvents/Oeve2"
				objects [1].SetActive (true); //다음으로 가기 버튼 생성//"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
			} else if (miniEvent == 8) {
				
				Application.LoadLevel ("Quizz");
			} 

		} else if (eve == 4) { //커리큘럼 4---------------------------------------------------------------------
			if (miniEvent == 1) {
				data.LoadData ();
				
				OpenTextBox (); //텍스트 박스 안켜져 있으면 켜기
				
				GameObject.Find ("Canvas/events/drawBoard").SetActive (true); //드로우 보드 키고
				objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
				objects [2].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + eve);
				miniEvent++;
				
			} else if (miniEvent == 2) {
				if (textBoxManager.curLine == 0) { //텍스트 1번째 줄에서
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/00"); 
				} else if (textBoxManager.curLine == 2) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/01");
				} else if (textBoxManager.curLine == 4) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/02");
				} else if (textBoxManager.curLine == 6) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/03");
				} else if (textBoxManager.curLine == 7) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/04");
				} else if (textBoxManager.curLine == 8) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/05");
				} else if (textBoxManager.curLine == 10) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/06");
				} else if (textBoxManager.curLine == 12) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/07");
				} else if (textBoxManager.curLine == 14) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur4/directions");
					miniEvent++;
				} 
			} else if (miniEvent == 3) {
				if (textBoxManager.textBox.activeSelf == false) { //텍스트 박스가 꺼지면
					objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
					
					if (objects [1].activeSelf == false) { //버튼이 사라지면
						miniEvent++;
					}
				}
			}  else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/drawBoard").SetActive (false); //드로우 보드 비활성화
				GameObject.Find ("Canvas/events/Geogeo").SetActive (true); //지도지오 등장 (클릭하면 힌트를 줌)
				objects [2].SetActive (false); //카메라를 덮었던 백그라운드 이미지 끄고 //UI Canvas/ScreenCover1
				miniEvent++;
			} else if (miniEvent == 5){
				if (clickFlag == true) { //지오지오가 클릭되면
					objects [6].SetActive (true); //"UI Canvas/events/Geogeo/hintImage"
					objects [6].GetComponent<Image> ().sprite
						= Resources.Load<Sprite> ("CurImages/cur4/hint1");//힌트 이미지 띄우기
				} else {// 다시 클릭되면
					objects [6].SetActive (false);
				}
				
				if (data.dataArr [data.dataArr [0].curNum].isSuccess == true) { //인식에 성공하면 
					objects [6].SetActive (false); //힌트 이미지 비활성화
					GameObject.Find ("Canvas/events/Geogeo").SetActive (false);//지오지오 비활성화
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					GameObject.Find ("ARCamera").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 7) {
				objects [8].SetActive (true);//"OuterEvents/Oeve2"
				objects [1].SetActive (true); //다음으로 가기 버튼 생성//"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
			} else if (miniEvent == 8) {
				
				Application.LoadLevel ("Quizz");
			} 
			
		}else if (eve == 5) { //커리큘럼 5---------------------------------------------------------------------
			if (miniEvent == 1) {
				data.LoadData ();
				
				OpenTextBox (); //텍스트 박스 안켜져 있으면 켜기
				
				GameObject.Find ("Canvas/events/drawBoard").SetActive (true); //드로우 보드 키고
				objects [2].SetActive (true); //UI Canvas/ScreenCover1 스크린 커버 활성화
				objects [2].GetComponent<Image>().sprite = Resources.Load<Sprite> ("Textures/stage_back" + eve);
				miniEvent++;
				
			} else if (miniEvent == 2) {
				if (textBoxManager.curLine == 0) { //텍스트 1번째 줄에서
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/00"); 
				} else if (textBoxManager.curLine == 2) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/01");
				} else if (textBoxManager.curLine == 4) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/02");
				} else if (textBoxManager.curLine == 6) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/03");
				} else if (textBoxManager.curLine == 7) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/04");
				} else if (textBoxManager.curLine == 8) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/05");
				} else if (textBoxManager.curLine == 10) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/06");
				} else if (textBoxManager.curLine == 12) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/07");
				} else if (textBoxManager.curLine == 14) {
					drawBoard.sprite = Resources.Load<Sprite> ("CurImages/cur5/directions");
					miniEvent++;
				} 
			} else if (miniEvent == 3) {
				if (textBoxManager.textBox.activeSelf == false) { //텍스트 박스가 꺼지면
					objects [1].SetActive (true); //다음으로 가기 버튼 생성 //"UI Canvas/NextMiniEventButton"
					
					if (objects [1].activeSelf == false) { //버튼이 사라지면
						miniEvent++;
					}
				}
			}  else if (miniEvent == 4) {
				GameObject.Find ("Canvas/events/drawBoard").SetActive (false); //드로우 보드 비활성화
				GameObject.Find ("Canvas/events/Geogeo").SetActive (true); //지도지오 등장 (클릭하면 힌트를 줌)
				objects [2].SetActive (false); //카메라를 덮었던 백그라운드 이미지 끄고 //UI Canvas/ScreenCover1
				miniEvent++;
			} else if (miniEvent == 5){
				if (clickFlag == true) { //지오지오가 클릭되면
					objects [6].SetActive (true); //"UI Canvas/events/Geogeo/hintImage"
					objects [6].GetComponent<Image> ().sprite
						= Resources.Load<Sprite> ("CurImages/cur5/hint1");//힌트 이미지 띄우기
				} else {// 다시 클릭되면
					objects [6].SetActive (false);
				}
				
				if (data.dataArr [data.dataArr [0].curNum].isSuccess == true) { //인식에 성공하면 
					objects [6].SetActive (false); //힌트 이미지 비활성화
					GameObject.Find ("Canvas/events/Geogeo").SetActive (false);//지오지오 비활성화
					GameObject.Find ("Canvas/events/success").SetActive (true); //성공 이미지 띄워줌 (클릭하면 비활성화 됨)
					miniEvent++;
				}
			} else if (miniEvent == 6) {
				if (GameObject.Find ("Canvas/events/success").activeSelf == false) { //성공 이미지 사라지면
					GameObject.Find ("ARCamera").SetActive (false);
					miniEvent++;
				}
			} else if (miniEvent == 7) {
				objects [8].SetActive (true);//"OuterEvents/Oeve2"
				objects [1].SetActive (true); //다음으로 가기 버튼 생성//"UI Canvas/NextMiniEventButton"
				if (objects [1].activeSelf == false) { //버튼이 사라지면
					miniEvent++;
				}
			} else if (miniEvent == 8) {
				
				Application.LoadLevel ("Quizz");
			} 
			
		}
	*/
		//첫번째 커리
	// 1번3번 예전에 만들었던거. 
	/*	if (eve == 1) {
			//LoadObjects (); //"event"+eve 라는 이름으로 된 오브젝트 켜고 나머진 비활성화
			data.LoadData ();

			reloadText (); //텍스트 파일에서 텍스트 읽어두기
		
			if (miniEvent == 1) {
				if (isStart) {
					if(data.dataArr[eve].isSuccess == true)
					{
						GameObject.Find("UI Canvas/events/Circle").SetActive(true); //동그라미 그리기
						isStart = false;
				//		StartCoroutine ("startScene"); //0.8초 딜레이

					}

				} else if (isStart == false) {
					if(GameObject.Find("UI Canvas/events/Circle").activeSelf == false) //동그라미 사라지면
					{	
						Application.UnloadLevel("Main");

						//GameObject.Find("OuterEvents/Oeve1").SetActive(true);
						objects[3].SetActive(true);
						//GameObject.Find("UI Canvas").transform.SetAsLastSibling();
						Physics.gravity = new Vector3(0,-19.62f,0);
						miniEvent++;
						isStart = true;
					}
				}
			} else if (miniEvent == 2) {
				OpenTextBox (); //텍스트 박스 안켜져 있으면 켜기
				if(textBoxManager.curLine >= 2)
				{
					if(GameObject.Find("UI Canvas/events/Drag").activeSelf == false) //드래그 하라는 이미지 띄우기
					{
						GameObject.Find("UI Canvas/events/Drag").SetActive(true);
				
					}
					else // 드래그 하라는 이미지가 사라지면
					{

						miniEvent++;
					}
				}
			} else if (miniEvent == 3) {
				if (!textBoxManager.textBox.activeSelf) //텍스트 박스가 비활성화 되어 있으면
				{
					//GameObject.Find("UI Canvas").SetActive(false);
					miniEvent++;
				}
			} else if (miniEvent == 4) {
				GameObject.Find("UI Canvas/events/NextMiniEventButton").SetActive(true); //다음으로 가기 버튼 생성
				if(GameObject.Find("UI Canvas/events/NextMiniEventButton").activeSelf==false) //버튼이 사라지면
				{
					miniEvent++;
				}
			} else if (miniEvent == 5) {

				isTextBoxOpened = false;
				Application.LoadLevel("Quizz");
			} 
		}

		//세번째 커리
		else if (eve == 3) {

			data.LoadData ();
			reloadText (); //텍스트 파일에서 텍스트 읽어두기
			Animator[] animations; //에니메이션 담을 배열

			if( miniEvent == 1)
			{
				if (data.dataArr[eve].isSuccess == true) //인식되면
				{
					GameObject.Find("UI Canvas/events/Circle").SetActive(true); //동그라미 띄우기
					miniEvent++;
				}
			}
			else if( miniEvent == 2 )
			{
			
				if(GameObject.Find("UI Canvas/events/Circle").activeSelf == false) //동그라이 꺼지면
				{	
					Application.UnloadLevel("Main");
					OpenTextBox ();	
					//GameObject.Find("OuterEvents/Oeve3").SetActive(true);
					objects[4].SetActive(true);

					animations = objects[4].GetComponentsInChildren<Animator>();
					foreach(Animator anim in animations )
					{
						anim.speed = 0; //0은 멈춤, 1은 보통, 2는 빠름 
					}
					miniEvent++;
					
				}
			}
			else if( miniEvent == 3 )
			{
				if(textBoxManager.curLine >= 2)
				{
					animations = objects[4].GetComponentsInChildren<Animator>();
					foreach(Animator anim in animations )
					{
						anim.speed = 1; //0은 멈춤, 1은 보통, 2는 빠름 
					}
					miniEvent++;
				}
			}
			else if(miniEvent == 4)
			{
				GameObject.Find("UI Canvas/events/NextMiniEventButton").SetActive(true); //다음으로 가기 버튼 생성
				if(GameObject.Find("UI Canvas/events/NextMiniEventButton").activeSelf==false) //버튼이 사라지면
				{
					miniEvent++;
				}
			}
			else if(miniEvent == 5)
			{
				isTextBoxOpened = false;
				Application.LoadLevel("Quizz");
			}

		}

		else // 테스트용, 나중에 지우기
		{
			LoadObjects ();
			reloadText ();
			
			if (miniEvent == 1) {
				if (isStart) {
					StartCoroutine ("startScene");
				} else if (isStart == false) {
					miniEvent++;
				}
				
			} else if (miniEvent == 2) {
				OpenTextBox ();
				
				if (!textBoxManager.textBox.activeSelf)
					miniEvent++;
				
			} else if (miniEvent == 3) {
				
				isTextBoxOpened = false;
				
			}
		}



	}
	*/
	public int getMiniEvent(){
		return miniEvent;
	}
	public void NextMiniEvent()
	{
		miniEvent++;
	}
	public void OnClick(){
		if(clickFlag == false) clickFlag = true;
		else if (clickFlag ==true) clickFlag = false;
	}



	void LoadObjects() {

		// 모든 이벤트 오브젝트를 숨긴다. (현재 커리큘럼 번호 이벤트 오브젝트 빼고)
		foreach (GameObject eventObject in objects) {
			//Debug.Log(eventObject.name);

		/*	if (eventObject.name.Equals("event" + eve))
				eventObject.SetActive(true);
			else */
				eventObject.SetActive(false);       
		}


	}

	void nextEvent()
	{
		eve++;
		ifLoadText = false;
		miniEvent = 1;
	}


	IEnumerator startScene()
	{
		yield return new WaitForSeconds (0.8f);
		isStart = false;
	}

	public IEnumerator LoadScene() 
	{ 
		AsyncOperation scene1 = Application.LoadLevelAsync( "CustomUI" ); 
		yield return scene1; 
		//mainCamera.enabled = false; 
	//	AsyncOperation scene2 = Application.LoadLevelAdditiveAsync( "TextBox_text" ); 
	//	yield return scene2; 

		//mainCamera.enabled = true; 
		Debug.Log("Scene Load Complete"); 
	}


}
