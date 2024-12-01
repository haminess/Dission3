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

    public Transform chairTransform; // ������ Transform
    public SpriteRenderer chairSprite; // ������ SpriteRenderer
    public SpriteRenderer deskSprite; // å���� SpriteRenderer
    public GameObject outlineObject; // ������ �ܰ��� ������Ʈ
    public GameObject qKeyImage; // QŰ UI ������Ʈ
    public Animator qKeyAnimator; // QŰ �ִϸ��̼�

    void Start()
    {
        // Player ��ũ��Ʈ�� ������Ʈ ã��
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        }

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

    private void SitOnChair()
    {
        if (player == null) return;

        // �÷��̾� ���� ��ġ ����
        originalPlayerPosition = player.transform.position;

        // Character�� Animator ��������
        Animator animator = player.GetComponentInChildren<Animator>();

        // �ɴ� �ִϸ��̼� ��� ����
        if (animator != null)
        {
            animator.SetBool("IsSitting", true);
        }

        // ��� ���� ��ġ�� �̵�
        player.transform.position = chairTransform.position;

        // ���̾� ����: ����(1) < ĳ����(0) < å��(-1)
        if (chairSprite != null) chairSprite.sortingOrder = 1;
        if (playerSprite != null) playerSprite.sortingOrder = 0;
        if (deskSprite != null) deskSprite.sortingOrder = -1;

        // �ܰ��� �� QŰ UI ��Ȱ��ȭ
        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);
        if (qKeyAnimator != null) qKeyAnimator.enabled = false;

        isPlayerSitting = true; // ���� ���·� ����
        player.Movable = false; // �÷��̾� �̵� �Ұ�
    }

    private void StandUpFromChair()
    {
        if (player == null) return;

        // �÷��̾� �̵� ����
        player.Movable = true;

        // ���� ��ġ�� ����
        player.transform.position = originalPlayerPosition;

        // �ɱ� ���� ����
        isPlayerSitting = false;

        // �ɴ� �ִϸ��̼� ����
        Animator animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsSitting", false);
        }

        // ���̾� ����: ����(0), ĳ����(0), å��(0)
        if (chairSprite != null) chairSprite.sortingOrder = 0;
        if (playerSprite != null) playerSprite.sortingOrder = 0;
        if (deskSprite != null) deskSprite.sortingOrder = 0;

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
