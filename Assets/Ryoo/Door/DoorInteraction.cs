using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private bool isPlayerNear = false; // 플레이어가 문 근처에 있는지 여부
    private bool isDoorOpen = false; // 문이 열려 있는지 여부

    public Transform doorTransform; // 문의 Transform
    public Vector3 doorOpenRotation; // 문의 열림 회전 값 (Euler Angles)
    public Vector3 doorClosedRotation; // 문의 닫힘 회전 값 (Euler Angles)
    public GameObject outlineObject; // 문의 외곽선 오브젝트
    public GameObject qKeyImage; // Q키 UI 오브젝트
    public Animator qKeyAnimator; // Q키 애니메이션
    public float doorRotationDuration = 0.5f; // 문이 열리고 닫히는 데 걸리는 시간

    private Coroutine doorCoroutine; // 문 상태를 변경하는 코루틴 참조

    void Start()
    {
        // 초기 상태 설정
        outlineObject.SetActive(false); // 외곽선 비활성화
        qKeyImage.SetActive(false); // Q키 UI 비활성화
    }

    void Update()
    {
        // 플레이어가 문 근처에 있을 때 Q 키로 문 열기/닫기
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q))
        {
            ToggleDoor();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 문 근처로 들어옴
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
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 문 근처에서 나감
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
        doorTransform.rotation = endRotation; // 최종 회전 설정
    }
}
