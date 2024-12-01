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

    public Transform chairPosition; // 의자 위치 (Inspector에서 설정)
    public float moveSpeed = 3f; // 플레이어가 의자까지 이동하는 속도
    public SpriteRenderer chairSprite; // 의자의 SpriteRenderer (Inspector에서 설정)
    public SpriteRenderer deskSprite; // 책상의 SpriteRenderer (Inspector에서 설정)

    // 원래 레이어 저장용 변수
    private int originalChairLayer;
    private int originalPlayerLayer;
    private int originalDeskLayer;

    void Start()
    {
        // Player 스크립트와 컴포넌트 찾기
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        }

        // 원래 레이어 저장
        if (chairSprite != null) originalChairLayer = chairSprite.sortingOrder;
        if (playerSprite != null) originalPlayerLayer = playerSprite.sortingOrder;
        if (deskSprite != null) originalDeskLayer = deskSprite.sortingOrder;
    }

    void Update()
    {
        // 플레이어가 의자 근처에 있을 때 Q 키로 앉기/일어서기
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q))
        {
            if (!isPlayerSitting)
            {
                StartCoroutine(SitOnChair());
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
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 의자 근처에서 나감
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private IEnumerator SitOnChair()
    {
        if (player == null || playerRigidbody == null || playerSprite == null || chairSprite == null || deskSprite == null)
        {
            Debug.LogError("Player, SpriteRenderer, or Rigidbody2D is not set up correctly.");
            yield break;
        }

        // 플레이어 이동 중지
        player.Movable = false;

        // 원래 위치 저장
        originalPlayerPosition = player.transform.position;

        // 플레이어가 의자 위치로 이동
        while (Vector3.Distance(player.transform.position, chairPosition.position) > 0.1f)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, chairPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 정확한 의자 위치로 정렬
        player.transform.position = chairPosition.position;
        isPlayerSitting = true;

        //// 앉는 애니메이션 설정
        //player.GetComponent<Animator>().SetBool("Sit", true);

        // 레이어 설정: 의자 > 캐릭터 > 책상
        chairSprite.sortingOrder = 1; // 의자 레이어
        playerSprite.sortingOrder = 0; // 캐릭터 레이어
        deskSprite.sortingOrder = -1; // 책상 레이어
    }

    private void StandUpFromChair()
    {
        if (player == null || playerSprite == null)
        {
            Debug.LogError("Player or SpriteRenderer is not set up correctly.");
            return;
        }

        // 플레이어 이동 가능
        player.Movable = true;

        // 원래 위치로 복귀
        player.transform.position = originalPlayerPosition;

        // 앉기 상태 해제
        isPlayerSitting = false;

        //// 앉는 애니메이션 해제
        //player.GetComponent<Animator>().SetBool("Sit", false);

        // 레이어 복구
        chairSprite.sortingOrder = originalChairLayer; // 의자 레이어 복구
        playerSprite.sortingOrder = originalPlayerLayer; // 캐릭터 레이어 복구
        deskSprite.sortingOrder = originalDeskLayer; // 책상 레이어 복구
    }
}
