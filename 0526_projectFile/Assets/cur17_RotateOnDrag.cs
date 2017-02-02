using UnityEngine;
using System.Collections;
using System;

public class cur17_RotateOnDrag : MonoBehaviour {
	
	private float baseAngle = 0.0f;

	public EventManager eventManager;
	public GameObject activeOnTop;
	public GameObject activeOnLeft;
	public GameObject activeOnRight;
	public GameObject nextEventBtn;


	public int clearCount;
	private bool flag0;
	private bool flag90;
	private bool flag_90;

	void Awake() {
		activeOnLeft.SetActive (false);
		activeOnTop.SetActive (false);
		activeOnRight.SetActive (false);
		clearCount = 0;
		nextEventBtn.SetActive(false);
	
	}

	void OnMouseDown(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) *Mathf.Rad2Deg;
	}
	
	void OnMouseDrag(){
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		pos = Input.mousePosition - pos;
		float ang = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
		transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
		//transform.RotateAround (Vector3.zero, Vector3.forward, ang);
		
		Vector3 rot = transform.rotation.eulerAngles;
		if ((360f - rot.z) <= 5 || (360f - rot.z) >= 355) {
			activeOnTop.SetActive (true);
			clearCount++;
		} else if ((360f - rot.z) <= 35 && (360f - rot.z) >= 20 ) {
			activeOnRight.SetActive (true);

			clearCount++;
		} else if ((360f - rot.z) >= 325 && (360f - rot.z) <= 340 ) {
			activeOnLeft.SetActive (true);

			clearCount++;
		} else {
			activeOnLeft.SetActive (false);
			activeOnTop.SetActive (false);
			activeOnRight.SetActive (false);
		}

		if (rot.z <= 335 && rot.z >= 180)
			transform.rotation = Quaternion.AngleAxis (325, Vector3.forward);
		else if(rot.z >= 25 && rot.z <= 315) transform.rotation = Quaternion.AngleAxis (35, Vector3.forward);

	}
	void OnMouseUp(){
		Vector3 rot = transform.rotation.eulerAngles;
		if ((360f - rot.z) <= 5 || (360f - rot.z) >= 355) {
	
			clearCount++;
		} else if ((360f - rot.z) <= 35 && (360f - rot.z) >= 25) {
		
			clearCount++;
		} else if ((360f - rot.z) >= 325 && (360f - rot.z) <= 335) {
		
			clearCount++;
		}
		if (clearCount >= 70) {
			nextEventBtn.SetActive(true);
		}
	}
/*
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
*/


}
