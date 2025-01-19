using System.Collections;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public GameObject shopUI; // 상점 UI 오브젝트
    private bool isNearShop = false; // 플레이어가 상점 근처에 있는지 여부

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
            Debug.Log("in");
            isNearShop = true; // 플레이어가 상점 범위 내에 들어옴
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("out");
            isNearShop = false; // 플레이어가 상점 범위를 벗어남
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
