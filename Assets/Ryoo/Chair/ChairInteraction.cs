using System.Collections;
using UnityEngine;

public class ChairInteraction : MonoBehaviour
{
    private Player player; // Player 스크립트 참조
    private bool isPlayerNear = false; // 플레이어가 의자 근처에 있는지 여부
    private bool isPlayerSitting = false; // 플레이어가 현재 의자에 앉아 있는지 여부
    private Vector3 originalPlayerPosition; // 플레이어가 앉기 전 위치
    private Rigidbody2D playerRigidbody; // 플레이어의 Rigidbody2D 참조
    private SpriteRenderer playerSprite; // 플레이어의 SpriteRenderer

    public Transform chairTransform; // 의자의 Transform
    public SpriteRenderer chairSprite; // 의자의 SpriteRenderer
    public SpriteRenderer deskSprite; // 책상의 SpriteRenderer
    public GameObject outlineObject; // 의자의 외곽선 오브젝트
    public GameObject qKeyImage; // Q키 UI 오브젝트
    public Animator qKeyAnimator; // Q키 애니메이션

    void Start()
    {
        // Player 스크립트와 컴포넌트 찾기
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        }

        // 초기 상태 설정
        outlineObject.SetActive(false); // 외곽선 비활성화
        qKeyImage.SetActive(false); // Q키 UI 비활성화
    }

    void Update()
    {
        // 플레이어가 의자 근처에 있을 때 Q 키로 앉기/일어서기
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q))
        {
            if (!isPlayerSitting)
            {
                SitOnChair();
            }
            else
            {
                StandUpFromChair();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 의자 근처로 들어옴
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            // 외곽선 활성화 및 Q키 UI 표시
            outlineObject.SetActive(true);
            qKeyImage.SetActive(true);

            // Q키 애니메이션 활성화
            if (qKeyAnimator != null)
            {
                qKeyAnimator.enabled = true;
                qKeyAnimator.SetTrigger("Show");
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

    private void SitOnChair()
    {
        if (player == null) return;

        // 플레이어 원래 위치 저장
        originalPlayerPosition = player.transform.position;

        // Character의 Animator 가져오기
        Animator animator = player.GetComponentInChildren<Animator>();

        // 앉는 애니메이션 즉시 시작
        if (animator != null)
        {
            animator.SetBool("IsSitting", true);
        }

        // 즉시 의자 위치로 이동
        player.transform.position = chairTransform.position;

        // 레이어 설정: 의자(1) < 캐릭터(0) < 책상(-1)
        if (chairSprite != null) chairSprite.sortingOrder = 1;
        if (playerSprite != null) playerSprite.sortingOrder = 0;
        if (deskSprite != null) deskSprite.sortingOrder = -1;

        // 외곽선 및 Q키 UI 비활성화
        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);
        if (qKeyAnimator != null) qKeyAnimator.enabled = false;

        isPlayerSitting = true; // 앉은 상태로 설정
        player.Movable = false; // 플레이어 이동 불가
    }

    private void StandUpFromChair()
    {
        if (player == null) return;

        // 플레이어 이동 가능
        player.Movable = true;

        // 원래 위치로 복귀
        player.transform.position = originalPlayerPosition;

        // 앉기 상태 해제
        isPlayerSitting = false;

        // 앉는 애니메이션 해제
        Animator animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsSitting", false);
        }

        // 레이어 복구: 의자(0), 캐릭터(0), 책상(0)
        if (chairSprite != null) chairSprite.sortingOrder = 0;
        if (playerSprite != null) playerSprite.sortingOrder = 0;
        if (deskSprite != null) deskSprite.sortingOrder = 0;

        // 외곽선 및 Q키 UI 다시 활성화
        outlineObject.SetActive(true);
        qKeyImage.SetActive(true);

        // Q키 애니메이션 다시 활성화
        if (qKeyAnimator != null)
        {
            qKeyAnimator.enabled = true;
            qKeyAnimator.SetTrigger("Show");
        }
    }
}
