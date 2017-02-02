using UnityEngine;
using System.Collections;
using System;//나중에 지

public class ClockControler : MonoBehaviour {


	public Transform hours, minutes, seconds;
	public TextMesh h, m, s;

	private const float
		hoursToDegrees = 360f / 12f,
		minutesToDegrees = 360f / 60f,
		secondsToDegrees = 360f / 60f;
	
	public bool analog; //true면 끊어지는 움직임 (false면 부드러운 움직임)

	// Update is called once per frame
	void Update () {
		DateTime time;
		if (analog) {
			TimeSpan timespan = DateTime.Now.TimeOfDay; 
			hours.localRotation =
				Quaternion.Euler(0f,0f,(float)timespan.TotalHours * -hoursToDegrees); // 타임스판으로, 딱딱 끊어지게 움직임
			minutes.localRotation =
				Quaternion.Euler(0f,0f,(float)timespan.TotalMinutes * -minutesToDegrees);
			seconds.localRotation =
				Quaternion.Euler(0f,0f,(float)timespan.TotalSeconds * -secondsToDegrees);
		}
		else {
			time = DateTime.Now;
			hours.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -hoursToDegrees); //실제 시간대로 부드럽게 움직임
			minutes.localRotation = Quaternion.Euler(0f, 0f, time.Minute * -minutesToDegrees);
			seconds.localRotation = Quaternion.Euler(0f, 0f, time.Second * -secondsToDegrees);
		
		}

		h.text = time.Hour.ToString("00") + "시";
		m.text = time.Minute.ToString("00") + "분";
		s.text = time.Second.ToString("00") + "초";



	}
}
