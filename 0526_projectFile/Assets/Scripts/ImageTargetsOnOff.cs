using UnityEngine;
using System.Collections;

public class ImageTargetsOnOff : MonoBehaviour {

	public Data data;
	public GameObject[] gameObjects = new GameObject[31];

	public void ImageTargetOn()
	{
		data.LoadData ();
		//모든 이미지 타겟을 비활성화
		for (int i=1; i <= 30; i++) 
		{
			//gameObjects[i] = GameObject.Find("ImageTarget_" + i);
			if(gameObjects[i].activeSelf==true)
				gameObjects[i].SetActive(false);
		}

		// 현재 커리큘럼 넘버에 해당하는 이미지 타겟만 활성화
		gameObjects [data.dataArr [0].curNum].SetActive (true);

	}
}
