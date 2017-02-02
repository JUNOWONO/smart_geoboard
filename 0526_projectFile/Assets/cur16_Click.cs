using UnityEngine;

using System.Collections;


public class cur16_Click : MonoBehaviour {



	public int imageNum = 1;
	bool flag = true;


	public cur16_Clear clearObj;



	

	void OnMouseDown(){

		if (flag == true) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("CurImages/cur16/" + imageNum);
			clearObj.clearCount_++;
			flag = false;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Textures/null");
			clearObj.clearCount_--;
			flag = true;
		}


		//Destroy(gameObject);
	}


}
