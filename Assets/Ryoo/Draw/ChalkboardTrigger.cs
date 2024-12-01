using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalkboardTrigger : MonoBehaviour
{
    private bool isPlayerNearChalkboard = false;
    public DrawingManager drawingManager;  // DrawingManager 스크립트를 참조

    public GameObject qKeyImage; // Q키 변수
    public GameObject outline; // 외곽선 오브젝트

    private Animator qKeyAnimator;

    void Start()
    {
        qKeyAnimator = qKeyImage.GetComponent<Animator>();
        outline.SetActive(false); // 시작 시 외곽선 비활성화
    }

    void Update()
    {
        // Q 키를 눌렀을 때, 플레이어가 칠판에 가까이 있으면 그림판 창을 켜고 끄기
        if (Input.GetKeyDown(KeyCode.Q) && isPlayerNearChalkboard)
        {
            drawingManager.ToggleDrawingPanel();  // DrawingManager의 그림판 토글 함수 호출
        }
    }

    // 트리거 영역에 들어왔을 때 호출됨
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // 플레이어 태그 확인
        {
            isPlayerNearChalkboard = true;  // 플레이어가 트리거 안에 있을 때
            qKeyImage.SetActive(true);
            qKeyAnimator.enabled = true; // 애니메이션 활성화
            outline.SetActive(true); // 외곽선 활성화
            Debug.Log("inDraw");
        }
    }

    // 트리거 영역에서 나갔을 때 호출됨
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearChalkboard = false;  // 플레이어가 트리거 밖으로 나갔을 때
            qKeyImage.SetActive(false);
            qKeyAnimator.enabled = false; // 애니메이션 비활성화
            outline.SetActive(false); // 외곽선 비활성화
            Debug.Log("outDraw");
        }
    }
}
