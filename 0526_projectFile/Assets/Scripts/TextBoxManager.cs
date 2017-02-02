using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;
	public EventManager eventManager;
	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int curLine;
	public int endAtLine;

	public static bool isActive = false;

	public Image NPCSprite;
	private string NPCName;

	public Data data;

	// Use this for initialization
	void Awake()
	{

		//textFile = Resources.Load<TextAsset>("Text/E#" + data.dataArr[0].curNum);
		textBox.SetActive (false);
		data.LoadData ();

		if(textFile != null){ // 텍스트 파일 존재할 때
			textLines = (textFile.text.Split ('\n')); // 뉴라인까지 읽어옴
		}

		endAtLine = textLines.Length - 1; // End line 초기화
		spriteChange ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (!textBox.activeSelf)
			return;

		if(textLines[curLine].Length <= 2 || curLine >= endAtLine){
			// 빈 문장은 길이가 1이다, 그때 대화창을 닫음
			// 이벤트 매니져에서 TextBox.nextText() 콜해서 다음줄로 컨너뛰면 계속 진행 
			if (textBox.activeSelf)
				DisableTextBox();
		}

		theText.text = textLines[curLine]; // 텍스트 대사를 UI에 출력



	}

	public void OnClick()
	{
		nextText ();
	}

	public void nextText(){
		if (curLine < textLines.Length - 1) {
			curLine++;
		}

		spriteChange ();
	}

	public void EnableTextBox()
	{
		textBox.SetActive(true);

		if (textLines[curLine].Length == 1)
			curLine++;

		spriteChange();
		NPCSprite.gameObject.SetActive(true);
	}

	public void DisableTextBox()
	{
		textBox.SetActive(false);
		NPCSprite.gameObject.SetActive(false);
	}

	public void spriteChange() //대화 주체에 따라 스프라이트 변경
	{
		NPCName = textLines[curLine].Split(':')[0];

		if (NPCName == "선생님")
			NPCName = "teacher";
		else if (NPCName == "지오지오")
			NPCName = "geogeo";

		if (Resources.Load<Sprite>("NPCs/" + NPCName + "-dialogue") != null)
			NPCSprite.sprite = Resources.Load<Sprite>("NPCs/" + NPCName + "-dialogue");
		else
			NPCSprite.sprite = Resources.Load<Sprite>("NPCs/error-dialogue");
	}

	public void insertButton(){



	}
		
	void stopGame()
	{
		Time.timeScale = 0; 
	}

	void resumeGame()
	{
		Time.timeScale = 1; 
	}
		
}
