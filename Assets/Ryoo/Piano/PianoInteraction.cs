using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoInteraction : MonoBehaviour
{
    public GameObject pianoMiniGameUI; // 피아노 미니게임 UI
    public GameObject outline; // 외곽선 오브젝트
    public GameObject qKeyImage; // Q키 변수

    private bool isPlayerNearPiano = false;  // 플레이어가 피아노 근처에 있는지 확인
    private bool isPianoMiniGameActive = false;  // 피아노 미니게임이 활성화되었는지 확인

    private Animator qKeyAnimator;

    void Start()
    {
        qKeyAnimator = qKeyImage.GetComponent<Animator>();
        outline.SetActive(false); // 시작 시 외곽선 비활성화
    }

    void Update()
    {
        // 플레이어가 피아노 근처에 있고 Q 키를 누를 때 미니게임을 토글
        if (Input.GetKeyDown(KeyCode.Q) && isPlayerNearPiano)
        {
            TogglePianoMiniGame();
        }
    }

    void TogglePianoMiniGame()
    {
        isPianoMiniGameActive = !isPianoMiniGameActive;
        pianoMiniGameUI.SetActive(isPianoMiniGameActive);

        // 피아노 미니게임이 활성화되면 게임을 일시정지, 그렇지 않으면 재개
        Time.timeScale = isPianoMiniGameActive ? 0f : 1f;
    }

    // 트리거 영역에 들어왔을 때 호출됨
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // 플레이어 태그 확인
        {
            isPlayerNearPiano = true;  // 플레이어가 트리거 안에 있을 때
            qKeyImage.SetActive(true);
            qKeyAnimator.enabled = true; // 애니메이션 활성화
            outline.SetActive(true); // 외곽선 활성화
            Debug.Log("inPiano");
        }
    }

    // 트리거 영역에서 나갔을 때 호출됨
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearPiano = false;  // 플레이어가 트리거 밖으로 나갔을 때
            qKeyImage.SetActive(false);
            qKeyAnimator.enabled = false; // 애니메이션 비활성화
            outline.SetActive(false); // 외곽선 비활성화
            Debug.Log("outPiano");

            // 플레이어가 피아노 근처에서 나가면 미니게임을 강제로 종료
            if (isPianoMiniGameActive)
            {
                TogglePianoMiniGame();
            }
        }
    }
}
