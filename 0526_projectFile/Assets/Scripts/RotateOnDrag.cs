using UnityEngine;
using System.Collections;
using System;

public class RotateOnDrag : MonoBehaviour {

	private float baseAngle = 0.0f;

	public TextMesh HoursText;
	public TextMesh MinutesText;
	public TextMesh SecondsText;
	public char hms; // hours 에는 h, minutes 에는 m, seconds에는 s


	void OnMouseDown(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) *Mathf.Rad2Deg;
	}
	
	void OnMouseDrag(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		float ang = Mathf.Atan2(pos.y, pos.x) *Mathf.Rad2Deg - baseAngle;
		transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
		//transform.RotateAround (Vector3.zero, Vector3.forward, ang);


		Vector3 rot = transform.rotation.eulerAngles;
		if (hms == 'h') {
			HoursText.text = ((360f - rot.z)*12f/360f ).ToString ("00") + "시";
		} 
		else if (hms == 'm') {
			MinutesText.text = ((360f - rot.z)*60f/360f).ToString ("00") + "분";
		}
		else if (hms == 's'){
			SecondsText.text = ((360f - rot.z)*60f/360f).ToString ("00") + "초";
		}

	}


}
