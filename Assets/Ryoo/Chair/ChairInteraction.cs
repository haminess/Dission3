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
    public GameObject outlineObject; // ������ �ܰ��� ������Ʈ
    public GameObject qKeyImage; // QŰ UI ������Ʈ
    public Animator qKeyAnimator; // QŰ �ִϸ��̼�

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

        // �ʱ� ���� ����
        outlineObject.SetActive(false); // �ܰ��� ��Ȱ��ȭ
        qKeyImage.SetActive(false); // QŰ UI ��Ȱ��ȭ
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

            // �ܰ��� Ȱ��ȭ �� QŰ UI ǥ��
            outlineObject.SetActive(true);
            qKeyImage.SetActive(true);

            // QŰ �ִϸ��̼� Ȱ��ȭ
            if (qKeyAnimator != null)
            {
                qKeyAnimator.enabled = true;
                qKeyAnimator.SetTrigger("Show");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ ���� ��ó���� ����
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // �ܰ��� �� QŰ UI ��Ȱ��ȭ
            outlineObject.SetActive(false);
            qKeyImage.SetActive(false);

            // QŰ �ִϸ��̼� ��Ȱ��ȭ
            if (qKeyAnimator != null)
            {
                qKeyAnimator.enabled = false;
            }
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

        // �ܰ��� �� QŰ UI ��Ȱ��ȭ
        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);

        // QŰ �ִϸ��̼� ��Ȱ��ȭ
        if (qKeyAnimator != null)
        {
            qKeyAnimator.enabled = false;
        }
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

        // �ܰ��� �� QŰ UI �ٽ� Ȱ��ȭ
        outlineObject.SetActive(true);
        qKeyImage.SetActive(true);

        // QŰ �ִϸ��̼� �ٽ� Ȱ��ȭ
        if (qKeyAnimator != null)
        {
            qKeyAnimator.enabled = true;
            qKeyAnimator.SetTrigger("Show");
        }
    }
}
