using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Transform targetTr; //추적할 타깃 게임오브젝트의 Transform 변수
    public float dist = 10.0f; //카메라와의 일정 거리
    public float height = 3.0f; // 카메라의 높이 
    public float dampTrace = 20.0f; //부드러운 추적을 위한 변수

    private Transform tr; //카메라 자신의 Transform 변수

    // Use this for initialization
    void Start()
    {
        //카메라 자신의 Transform 컴포넌트를 tr에 할당
        tr = GetComponent<Transform>();
    }

    // LateUpdate는 모든 Update()함수들 호충 이후 한 번씩 호출됨
    // 추적할 타깃의 이동이 종료된 이후에 카메라가 추적하기 위해 LateUpdate를 사용
    void LateUpdate()
    {

        //카메라의 위치를 추적대상에서 dist 변수만큼 뒤쪽으로 배치하고
        // height 만큼 위로 배치함
        tr.position = Vector3.Lerp(tr.position,
                                    targetTr.position - (targetTr.forward * dist) + (Vector3.up * height),
                                    Time.deltaTime * dampTrace);
        //Vector3.Lerp(Vector3 시작위치, Vector3 종료위치, float 시간)

        // 카메라가 타깃 게임오브젝트를 바라보게 설정
        tr.LookAt(targetTr.position);
    }

}
