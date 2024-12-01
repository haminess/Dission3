using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public GameObject shopUI; // 매점 UI 오브젝트
    public GameObject qKeyImage; // Q키 UI 이미지
    public GameObject outline; // 외곽선 오브젝트
    private bool isNearShop = false; // 플레이어가 매점 근처에 있는지 여부
    private bool isShopOpen = false; // 매점 창이 열려 있는지 여부

    void Start()
    {
        outline.SetActive(false); // 시작 시 외곽선 비활성화
        qKeyImage.SetActive(false); // 시작 시 Q키 이미지 비활성화
    }

    void Update()
    {
        // 플레이어가 매점 근처에 있고 Q키를 눌렀을 때 매점 열기/닫기
        if (isNearShop && Input.GetKeyDown(KeyCode.Q))
        {
            ToggleShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 매점 근처에 들어왔을 때
        if (other.gameObject.CompareTag("Player"))
        {
            isNearShop = true;
            outline.SetActive(true); // 외곽선 활성화
            qKeyImage.SetActive(true); // Q키 이미지 활성화
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 매점 범위를 벗어났을 때
        if (other.gameObject.CompareTag("Player"))
        {
            isNearShop = false;
            outline.SetActive(false); // 외곽선 비활성화
            qKeyImage.SetActive(false); // Q키 이미지 비활성화

            // 매점이 열려 있으면 닫기
            if (isShopOpen)
            {
                CloseShop();
            }
        }
    }

    private void ToggleShop()
    {
        if (!isShopOpen)
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }

    private void OpenShop()
    {
        shopUI.SetActive(true); // 매점 UI 활성화
        isShopOpen = true;
    }

    private void CloseShop()
    {
        shopUI.SetActive(false); // 매점 UI 비활성화
        isShopOpen = false;
    }
}
