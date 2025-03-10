using System.Collections;
using UnityEngine;

public class CafeteriaChairBack : MonoBehaviour
{
    private static CafeteriaChairBack currentChair;
    private Player player;
    private bool isPlayerNear = false;
    private bool isPlayerSitting = false;
    private Vector3 originalPlayerPosition;

    public Transform chairPosition;
    public GameObject outlineObject;
    public GameObject qKeyImage;
    public Animator qKeyAnimator;
    public float moveDuration = 0.5f;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (outlineObject == null)
            Debug.LogError("[CafeteriaChairBack] outlineObject가 설정되지 않았습니다!");

        if (qKeyImage == null)
            Debug.LogError("[CafeteriaChairBack] qKeyImage가 설정되지 않았습니다!");

        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentChair == null && isPlayerNear)
            {
                currentChair = this;
                SetAllChairsActive(false);
                StartCoroutine(SitOnChair());
            }
            else if (currentChair == this && isPlayerSitting)
            {
                StartCoroutine(StandUpFromChair());
                currentChair = null;
                SetAllChairsActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[CafeteriaChairBack] OnTriggerEnter2D 실행됨: " + other.name);
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            outlineObject.SetActive(true);
            qKeyImage.SetActive(true);
            if (qKeyAnimator != null) qKeyAnimator.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            outlineObject.SetActive(false);
            qKeyImage.SetActive(false);
            if (qKeyAnimator != null) qKeyAnimator.enabled = false;
        }
    }

    private IEnumerator SitOnChair()
    {
        if (player == null) yield break;

        originalPlayerPosition = player.transform.position;
        Animator animator = player.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetBool("IsSitting", true);
            animator.SetTrigger("SitBack");
        }

        outlineObject.SetActive(false);
        qKeyImage.SetActive(false);
        if (qKeyAnimator != null) qKeyAnimator.enabled = false;

        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = chairPosition.position;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = chairPosition.position;

        isPlayerSitting = true;
        player.Movable = false;
    }

    private IEnumerator StandUpFromChair()
    {
        if (player == null) yield break;

        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = originalPlayerPosition;

        while (elapsedTime < moveDuration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = originalPlayerPosition;

        player.Movable = true;
        isPlayerSitting = false;

        Animator animator = player.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsSitting", false);
        }
    }

    private void SetAllChairsActive(bool active)
    {
        var allChairs = FindObjectsOfType<CafeteriaChairBack>();
        foreach (var chair in allChairs)
        {
            if (chair != this)
            {
                chair.enabled = active;
                if (!active)
                {
                    if (chair.outlineObject != null) chair.outlineObject.SetActive(false);
                    if (chair.qKeyImage != null) chair.qKeyImage.SetActive(false);
                    if (chair.qKeyAnimator != null) chair.qKeyAnimator.enabled = false;
                }
            }
        }
    }
}
