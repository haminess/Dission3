using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject speechBubble;  // 말풍선 UI
    public TextMeshProUGUI dialogueText;  // 대사 텍스트
    public string[] talk;  // NPC 대사 목록
    public float maxWaitTime = 5f;  // 대화 쿨타임 (5초)
    public float fadeSpeed = 2f; // 말풍선 페이드 속도
    public Transform npcTransform; // NPC의 Transform (설정 필요)
    public Vector3 bubbleOffset = new Vector3(0, 2f, 0); // NPC 머리 위로 올리는 값

    private float waitTime = 0;
    private bool isTalking = false;
    private CanvasGroup canvasGroup;
    private Camera mainCamera;
    private Animator anim; // ❗ 애니메이터 변수 추가

    private void Start()
    {
        if (speechBubble == null)
        {
            speechBubble = GameObject.Find("SpeechBubble"); // Hierarchy에서 SpeechBubble 찾기
            if (speechBubble == null)
            {
                Debug.LogError("speechBubble을 찾을 수 없습니다! Inspector에서 연결하세요.");
            }
        }

        if (speechBubble != null)
        {
            canvasGroup = speechBubble.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = speechBubble.AddComponent<CanvasGroup>();
            }

            speechBubble.SetActive(false);
            canvasGroup.alpha = 0;
        }
    }

    private void LateUpdate()
    {
        if (speechBubble == null || npcTransform == null)
        {
            return;
        }

        // NPC 위치를 기반으로 말풍선 위치 업데이트
        speechBubble.transform.position = Camera.main.WorldToScreenPoint(npcTransform.position + bubbleOffset);

        Debug.Log($"speechBubble 위치: {speechBubble.transform.position}, NPC 위치: {npcTransform.position}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Entered NPC Area"); // ← 기존 로그

        if (collision.CompareTag("Player") && waitTime <= 0 && !isTalking)
        {
            if (talk.Length == 0)
            {
                Debug.LogWarning("talk 배열이 비어 있음.");
                return;
            }

            if (speechBubble == null)
            {
                Debug.LogError("speechBubble이 null입니다. Inspector에서 speechBubble이 올바르게 연결되었는지 확인하세요.");
                return;
            }

            if (dialogueText == null)
            {
                Debug.LogError("dialogueText가 null입니다. Inspector에서 dialogueText가 연결되었는지 확인하세요.");
                return;
            }

            Debug.Log("ShowSpeechBubble 실행 시도!"); // ← 실행 여부 확인 로그 추가
            isTalking = true; // 대화 중 상태 설정
            waitTime = maxWaitTime;
            StartCoroutine(ShowSpeechBubble(talk[Random.Range(0, talk.Length)]));
        }
    }

    private IEnumerator ShowSpeechBubble(string sentence)
    {
        Debug.Log("ShowSpeechBubble 실행됨! 문장: " + sentence);

        speechBubble.SetActive(true);
        Debug.Log("speechBubble 활성화됨!");

        canvasGroup.alpha = 0;

        if (anim)
        {
            anim.SetBool("isTalking", true);
        }

        yield return StartCoroutine(FadeIn());

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueText.rectTransform);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeOut());

        speechBubble.SetActive(false);
        isTalking = false;

        if (anim)
        {
            anim.SetBool("isTalking", false);
        }

        Debug.Log("ShowSpeechBubble 종료됨!");
    }

    private IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}
