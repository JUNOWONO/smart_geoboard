using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomBtn2Handler : MonoBehaviour {

	public Text helpText;


	public void OnClick(){

		if (helpText.text.Equals (" "))
			helpText.text = "정육면체를 그려보세요.";
		else if(helpText.text.Equals("정육면체를 그려보세요."))
			helpText.text = " ";
	}

}
	
