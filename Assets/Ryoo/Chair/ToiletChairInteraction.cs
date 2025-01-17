using System.Collections;
using UnityEngine;

public class ToiletChairInteraction : MonoBehaviour
{
    private Player player; // Player ��ũ��Ʈ ����
    private bool isPlayerNear = false; // �÷��̾ ���� ��ó�� �ִ��� ����
    private bool isPlayerSitting = false; // �÷��̾ ���� ���ڿ� �ɾ� �ִ��� ����
    private Vector3 originalPlayerPosition; // �÷��̾ �ɱ� �� ��ġ
    private Rigidbody2D playerRigidbody; // �÷��̾��� Rigidbody2D ����
    private SpriteRenderer playerSprite; // �÷��̾��� SpriteRenderer

    public Transform chairTransform; // ������ Transform
    public GameObject outlineObject; // ������ �ܰ��� ������Ʈ
    public GameObject qKeyImage; // QŰ UI ������Ʈ
    public Animator qKeyAnimator; // QŰ �ִϸ��̼�
    public float moveDuration = 0.5f; // �÷��̾ �̵��ϴ� �� �ɸ��� �ð�

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
                StartCoroutine(SitOnChair());
            }
            else
            {
                StartCoroutine(StandUpFromChair());
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
        if (player == null) yield break;

        // �÷��̾� ���� ��ġ ����
        originalPlayerPosition = player.transform.position;

        // Character�� Animator ��������
        Animator animator = player.GetComponentInChildren<Animator>();

        // �ɴ� �ִϸ��̼� ��� ����
        if (animator != null)
        {
            animator.SetBool("IsSitting", true);
        }

        // �ܰ��� �� QŰ UI ��Ȱ��ȭ
        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);
        if (qKeyAnimator != null) qKeyAnimator.enabled = false;

        // �̵� �ִϸ��̼� (Lerp ���)
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = chairTransform.position;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = chairTransform.position; // ���� ��ġ ����

        // ���� ���·� ����
        isPlayerSitting = true;
        player.Movable = false;
    }

    private IEnumerator StandUpFromChair()
    {
        if (player == null) yield break;

        // �̵� �ִϸ��̼� (Lerp ���)
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = originalPlayerPosition;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = originalPlayerPosition; // ���� ��ġ ����

        // �÷��̾� �̵� ����
        player.Movable = true;

        // �ɱ� ���� ����
        isPlayerSitting = false;

        // �ɴ� �ִϸ��̼� ����
        Animator animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsSitting", false);
        }

        // �ܰ��� �� QŰ UI �ٽ� Ȱ��ȭ
        outlineObject.SetActive(true);
        qKeyImage.SetActive(true);

        // QŰ �ִϸ��̼� �ٽ� Ȱ��ȭ
        if (qKeyAnimator != null)
        {
            qKeyAnimator.enabled = true;
        }
    }
}
