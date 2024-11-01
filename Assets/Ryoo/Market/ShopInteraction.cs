using System.Collections;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public GameObject shopUI; // 상점 UI 오브젝트
    private bool isNearShop = false; // 플레이어가 상점 근처에 있는지 여부

    public GameObject qKeyImage; // Q키 변수

    private Animator qKeyAnimator;

    void Start()
    {
        qKeyAnimator = qKeyImage.GetComponent<Animator>();
    }

    void Update()
    {
        if (isNearShop && Input.GetKeyDown(KeyCode.Q))
        {
            ToggleShop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearShop = true; // 플레이어가 상점 범위 내에 들어옴
            qKeyImage.SetActive(true);
            qKeyAnimator.enabled = true; // 애니메이션 활성화
            Debug.Log("inMarket");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearShop = false; // 플레이어가 상점 범위를 벗어남
            qKeyImage.SetActive(false);
            qKeyAnimator.enabled = false; // 애니메이션 비활성화
            Debug.Log("outMarket");
        }
    }

    void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);  // 상점 창 닫기
    }
}
