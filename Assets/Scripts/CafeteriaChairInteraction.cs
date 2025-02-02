using System.Collections;
using UnityEngine;

public class CafeteriaChairInteraction : MonoBehaviour
{
    private static CafeteriaChairInteraction currentChair; // 현재 플레이어가 앉아 있는 의자
    private Player player; // Player 스크립트 참조
    private bool isPlayerNear = false; // 플레이어가 의자 근처에 있는지 여부
    private bool isPlayerSitting = false; // 플레이어가 현재 의자에 앉아 있는지 여부
    private Vector3 originalPlayerPosition; // 플레이어가 앉기 전 위치

    public Transform chairPosition; // 의자의 위치
    public GameObject outlineObject; // 의자의 외곽선 오브젝트
    public GameObject qKeyImage; // Q키 UI 오브젝트
    public Animator qKeyAnimator; // Q키 애니메이션
    public float moveDuration = 0.5f; // 플레이어가 이동하는 데 걸리는 시간

    void Start()
    {
        // Player 스크립트와 컴포넌트 찾기
        player = FindObjectOfType<Player>();

        // 초기 상태 설정
        outlineObject.SetActive(false); // 외곽선 비활성화
        qKeyImage.SetActive(false); // Q키 UI 비활성화
    }

    void Update()
    {
        // Q 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentChair == null && isPlayerNear) // 플레이어가 근처에 있고 현재 의자가 비어 있는 경우
            {
                currentChair = this; // 현재 의자를 활성화
                SetAllChairsActive(false); // 다른 의자 비활성화
                StartCoroutine(SitOnChair());
            }
            else if (currentChair == this && isPlayerSitting) // 현재 의자에서 일어나기
            {
                StartCoroutine(StandUpFromChair());
                currentChair = null; // 현재 의자 초기화
                SetAllChairsActive(true); // 모든 의자 다시 활성화
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 의자 근처로 들어옴
        if (other.CompareTag("Player") && (currentChair == null || currentChair == this))
        {
            isPlayerNear = true;

            // 외곽선 활성화 및 Q키 UI 표시
            outlineObject.SetActive(true);
            qKeyImage.SetActive(true);

            // Q키 애니메이션 활성화
            if (qKeyAnimator != null)
            {
                qKeyAnimator.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 의자 근처에서 나감
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // 외곽선 및 Q키 UI 비활성화
            outlineObject.SetActive(false);
            qKeyImage.SetActive(false);

            // Q키 애니메이션 비활성화
            if (qKeyAnimator != null)
            {
                qKeyAnimator.enabled = false;
            }
        }
    }

    private IEnumerator SitOnChair()
    {
        if (player == null) yield break;

        // 플레이어 원래 위치 저장
        originalPlayerPosition = player.transform.position;

        // Character의 Animator 가져오기
        Animator animator = player.GetComponentInChildren<Animator>();

        // 앉는 애니메이션 즉시 시작
        if (animator != null)
        {
            animator.SetBool("IsSitting", true);
        }

        // 외곽선 및 Q키 UI 비활성화
        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);
        if (qKeyAnimator != null) qKeyAnimator.enabled = false;

        // 이동 애니메이션 (Lerp 사용)
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = chairPosition.position;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = chairPosition.position; // 최종 위치 설정

        // 앉은 상태로 설정
        isPlayerSitting = true;
        player.Movable = false;
    }

    private IEnumerator StandUpFromChair()
    {
        if (player == null) yield break;

        // 이동 애니메이션 (Lerp 사용)
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = originalPlayerPosition;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = originalPlayerPosition; // 최종 위치 설정

        // 플레이어 이동 가능
        player.Movable = true;

        // 앉기 상태 해제
        isPlayerSitting = false;

        // 앉는 애니메이션 해제
        Animator animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsSitting", false);
        }
    }

    private void SetAllChairsActive(bool active)
    {
        var allChairs = FindObjectsOfType<CafeteriaChairInteraction>();
        foreach (var chair in allChairs)
        {
            if (chair != this) // 현재 의자는 제외
            {
                // 스크립트 활성화/비활성화
                chair.enabled = active;

                // 외곽선 및 Q키 UI 비활성화
                if (!active) // 비활성화 시에만 처리
                {
                    if (chair.outlineObject != null)
                        chair.outlineObject.SetActive(false);

                    if (chair.qKeyImage != null)
                        chair.qKeyImage.SetActive(false);

                    if (chair.qKeyAnimator != null)
                        chair.qKeyAnimator.enabled = false;
                }
            }
        }
    }
}
