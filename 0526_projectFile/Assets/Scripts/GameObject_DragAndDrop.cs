using UnityEngine;
using System.Collections;

public class GameObject_DragAndDrop : MonoBehaviour {

	GameObject target = null;
	Vector3 screenPosition;
	Vector3 offset;
	private bool isMouseDrag = false;

	// 원하는 물체에 스크립트 추가하면 그 물체를 드래그엔 드랍으로 옮길 수 있음. 
	// 단,* 해당 물체에 반드시 Collider가 있어야 함 ( 없으면 물체 크기에 맞게 하나 만들어)*


	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hitInfo;
			target = ReturnClickedObject(out hitInfo);
			if( target != null)
			{
				isMouseDrag = true;
			
			//월드포지션에서 스크린 포지션으로 변경
			screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);

			offset = target.transform.position - Camera.main.ScreenToWorldPoint
				( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			isMouseDrag = false;
			target = null; //마우스 드랍 이후 타겟 초기화 
		}

		if (isMouseDrag) 
		{	//마우스 포지션
			Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
			                                         screenPosition.z);
			//스크린 포지션을 월드 포지션으로
			Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

			//현재 오브젝트의 위치를 업데이트
			target.transform.position = currentPosition;
		}

	
	}

	GameObject ReturnClickedObject(out RaycastHit hit)
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction * 10, out hit)) 
		{
			target = hit.collider.gameObject;
		}
		return target;
	}
}
