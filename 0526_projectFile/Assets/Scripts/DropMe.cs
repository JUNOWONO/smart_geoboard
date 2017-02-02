using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.yellow;
	public Drag_Drop_Clear dragDropClear;


	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null) 
			//receivingImage.overrideSprite = dropSprite;//원래위치
		/////////////밑에꺼 추가함////////////////////------------------------------------
		if (dragDropClear.eve == 26) { //컬 26에서는
			if (dragDropClear.clearCount <= 5) { //6개 이하로 쌓였을 때 
				if (gameObject.name == "1") { //drop me의 이름이 1일때
					receivingImage.overrideSprite = dropSprite;
					data.pointerDrag.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stone");
					data.pointerDrag.GetComponent<Image> ().raycastTarget = false;
					gameObject.GetComponentInParent<Image> ().raycastTarget = false;
					gameObject.GetComponent<Image> ().raycastTarget = false;
					
					dragDropClear.clearCount++;
				}
			} else if (dragDropClear.clearCount >= 6 && dragDropClear.clearCount <= 8) { //9개 이하로 쌓였을 떄는 
				if (gameObject.name == "2") { //Drop me 의 이름이 2일때 
					receivingImage.overrideSprite = dropSprite;
					data.pointerDrag.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stone");
					data.pointerDrag.GetComponent<Image> ().raycastTarget = false;
					gameObject.GetComponentInParent<Image> ().raycastTarget = false;
					gameObject.GetComponent<Image> ().raycastTarget = false;
					
					dragDropClear.clearCount++;
				}
			} else if (dragDropClear.clearCount == 9) { //마지막 3층짜리 한 조각
				if (gameObject.name == "3") { //drop me 의 이름이 3일때 
					receivingImage.overrideSprite = dropSprite;
					data.pointerDrag.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stone");
					data.pointerDrag.GetComponent<Image> ().raycastTarget = false;
					gameObject.GetComponentInParent<Image> ().raycastTarget = false;
					gameObject.GetComponent<Image> ().raycastTarget = false;
					
					dragDropClear.clearCount++;
				}
			}


		} else if(dragDropClear.eve == 29){
			data.pointerDrag.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stone");
			data.pointerDrag.GetComponent<Image> ().raycastTarget = false;
			dragDropClear.clearCount++;

		}else if(dragDropClear.eve == 30){
			dragDropClear.clearCount++;
		} else { //cur26을 제외한 나머지 에서는 
			receivingImage.overrideSprite = dropSprite;
			if (data.pointerDrag.name == gameObject.name && data.pointerDrag != gameObject) { 

				data.pointerDrag.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/stone");
				data.pointerDrag.GetComponent<Image> ().raycastTarget = false;
				gameObject.GetComponentInParent<Image> ().raycastTarget = false;
				gameObject.GetComponent<Image> ().raycastTarget = false;

				dragDropClear.clearCount++;
			}
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag; // 그래그 하고있는 오브젝트.
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}
}
