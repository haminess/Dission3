using System.Collections;
using UnityEngine;

public class ChairInteraction : MonoBehaviour
{
    private Player player; // Player ��ũ��Ʈ ����
    private bool isPlayerNear = false; // �÷��̾ ���� ��ó�� �ִ��� ����
    private bool isPlayerSitting = false; // �÷��̾ ���� ���ڿ� �ɾ� �ִ��� ����
    private Vector3 originalPlayerPosition; // �÷��̾ �ɱ� �� ��ġ
    private Rigidbody2D playerRigidbody; // �÷��̾��� Rigidbody2D ����
    private SpriteRenderer playerSprite; // �÷��̾��� SpriteRenderer

    public Transform chairPosition; // ���� ��ġ (Inspector���� ����)
    public float moveSpeed = 3f; // �÷��̾ ���ڱ��� �̵��ϴ� �ӵ�
    public SpriteRenderer chairSprite; // ������ SpriteRenderer (Inspector���� ����)
    public SpriteRenderer deskSprite; // å���� SpriteRenderer (Inspector���� ����)

    // ���� ���̾� ����� ����
    private int originalChairLayer;
    private int originalPlayerLayer;
    private int originalDeskLayer;

    void Start()
    {
        // Player ��ũ��Ʈ�� ������Ʈ ã��
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        }

        // ���� ���̾� ����
        if (chairSprite != null) originalChairLayer = chairSprite.sortingOrder;
        if (playerSprite != null) originalPlayerLayer = playerSprite.sortingOrder;
        if (deskSprite != null) originalDeskLayer = deskSprite.sortingOrder;
    }

    void Update()
    {
        // �÷��̾ ���� ��ó�� ���� �� Q Ű�� �ɱ�/�Ͼ��
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
        // �÷��̾ ���� ��ó�� ����
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ ���� ��ó���� ����
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

        // �÷��̾� �̵� ����
        player.Movable = false;

        // ���� ��ġ ����
        originalPlayerPosition = player.transform.position;

        // �÷��̾ ���� ��ġ�� �̵�
        while (Vector3.Distance(player.transform.position, chairPosition.position) > 0.1f)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, chairPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // ��Ȯ�� ���� ��ġ�� ����
        player.transform.position = chairPosition.position;
        isPlayerSitting = true;

        //// �ɴ� �ִϸ��̼� ����
        //player.GetComponent<Animator>().SetBool("Sit", true);

        // ���̾� ����: ���� > ĳ���� > å��
        chairSprite.sortingOrder = 1; // ���� ���̾�
        playerSprite.sortingOrder = 0; // ĳ���� ���̾�
        deskSprite.sortingOrder = -1; // å�� ���̾�
    }

    private void StandUpFromChair()
    {
        if (player == null || playerSprite == null)
        {
            Debug.LogError("Player or SpriteRenderer is not set up correctly.");
            return;
        }

        // �÷��̾� �̵� ����
        player.Movable = true;

        // ���� ��ġ�� ����
        player.transform.position = originalPlayerPosition;

        // �ɱ� ���� ����
        isPlayerSitting = false;

        //// �ɴ� �ִϸ��̼� ����
        //player.GetComponent<Animator>().SetBool("Sit", false);

        // ���̾� ����
        chairSprite.sortingOrder = originalChairLayer; // ���� ���̾� ����
        playerSprite.sortingOrder = originalPlayerLayer; // ĳ���� ���̾� ����
        deskSprite.sortingOrder = originalDeskLayer; // å�� ���̾� ����
    }
}
