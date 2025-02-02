using System.Collections;
using UnityEngine;

public class CafeteriaChairInteraction : MonoBehaviour
{
    private static CafeteriaChairInteraction currentChair; // ���� �÷��̾ �ɾ� �ִ� ����
    private Player player; // Player ��ũ��Ʈ ����
    private bool isPlayerNear = false; // �÷��̾ ���� ��ó�� �ִ��� ����
    private bool isPlayerSitting = false; // �÷��̾ ���� ���ڿ� �ɾ� �ִ��� ����
    private Vector3 originalPlayerPosition; // �÷��̾ �ɱ� �� ��ġ

    public Transform chairPosition; // ������ ��ġ
    public GameObject outlineObject; // ������ �ܰ��� ������Ʈ
    public GameObject qKeyImage; // QŰ UI ������Ʈ
    public Animator qKeyAnimator; // QŰ �ִϸ��̼�
    public float moveDuration = 0.5f; // �÷��̾ �̵��ϴ� �� �ɸ��� �ð�

    void Start()
    {
        // Player ��ũ��Ʈ�� ������Ʈ ã��
        player = FindObjectOfType<Player>();

        // �ʱ� ���� ����
        outlineObject.SetActive(false); // �ܰ��� ��Ȱ��ȭ
        qKeyImage.SetActive(false); // QŰ UI ��Ȱ��ȭ
    }

    void Update()
    {
        // Q Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentChair == null && isPlayerNear) // �÷��̾ ��ó�� �ְ� ���� ���ڰ� ��� �ִ� ���
            {
                currentChair = this; // ���� ���ڸ� Ȱ��ȭ
                SetAllChairsActive(false); // �ٸ� ���� ��Ȱ��ȭ
                StartCoroutine(SitOnChair());
            }
            else if (currentChair == this && isPlayerSitting) // ���� ���ڿ��� �Ͼ��
            {
                StartCoroutine(StandUpFromChair());
                currentChair = null; // ���� ���� �ʱ�ȭ
                SetAllChairsActive(true); // ��� ���� �ٽ� Ȱ��ȭ
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ ���� ��ó�� ����
        if (other.CompareTag("Player") && (currentChair == null || currentChair == this))
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
        Vector3 endPos = chairPosition.position;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = chairPosition.position; // ���� ��ġ ����

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
    }

    private void SetAllChairsActive(bool active)
    {
        var allChairs = FindObjectsOfType<CafeteriaChairInteraction>();
        foreach (var chair in allChairs)
        {
            if (chair != this) // ���� ���ڴ� ����
            {
                // ��ũ��Ʈ Ȱ��ȭ/��Ȱ��ȭ
                chair.enabled = active;

                // �ܰ��� �� QŰ UI ��Ȱ��ȭ
                if (!active) // ��Ȱ��ȭ �ÿ��� ó��
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
