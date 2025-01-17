using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private bool isPlayerNear = false; // �÷��̾ �� ��ó�� �ִ��� ����
    private bool isDoorOpen = false; // ���� ���� �ִ��� ����

    public Transform doorTransform; // ���� Transform
    public Vector3 doorOpenRotation; // ���� ���� ȸ�� �� (Euler Angles)
    public Vector3 doorClosedRotation; // ���� ���� ȸ�� �� (Euler Angles)
    public GameObject outlineObject; // ���� �ܰ��� ������Ʈ
    public GameObject qKeyImage; // QŰ UI ������Ʈ
    public Animator qKeyAnimator; // QŰ �ִϸ��̼�
    public float doorRotationDuration = 0.5f; // ���� ������ ������ �� �ɸ��� �ð�

    private Coroutine doorCoroutine; // �� ���¸� �����ϴ� �ڷ�ƾ ����

    void Start()
    {
        // �ʱ� ���� ����
        outlineObject.SetActive(false); // �ܰ��� ��Ȱ��ȭ
        qKeyImage.SetActive(false); // QŰ UI ��Ȱ��ȭ
    }

    void Update()
    {
        // �÷��̾ �� ��ó�� ���� �� Q Ű�� �� ����/�ݱ�
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q))
        {
            ToggleDoor();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ �� ��ó�� ����
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
        // �÷��̾ �� ��ó���� ����
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

    private void ToggleDoor()
    {
        if (isDoorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (doorCoroutine != null) StopCoroutine(doorCoroutine);
        doorCoroutine = StartCoroutine(RotateDoor(doorTransform.rotation, Quaternion.Euler(doorOpenRotation)));
        isDoorOpen = true;
    }

    private void CloseDoor()
    {
        if (doorCoroutine != null) StopCoroutine(doorCoroutine);
        doorCoroutine = StartCoroutine(RotateDoor(doorTransform.rotation, Quaternion.Euler(doorClosedRotation)));
        isDoorOpen = false;
    }

    private IEnumerator RotateDoor(Quaternion startRotation, Quaternion endRotation)
    {
        float elapsedTime = 0f;
        while (elapsedTime < doorRotationDuration)
        {
            doorTransform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / doorRotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        doorTransform.rotation = endRotation; // ���� ȸ�� ����
    }
}
