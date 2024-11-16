using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemPurchaseManager : MonoBehaviour
{
    public Text balanceText; // 보유 금액을 표시하는 Text UI
    public Text messageText; // 메시지를 표시할 Text UI
    public GameObject speechBubble; // 말풍선 UI 오브젝트
    public Text speechBubbleText; // 말풍선 텍스트
    public ShopItem shopItemManager; // ShopItem 스크립트를 참조
    public GameObject itemContainer; // 아이템들을 담고 있는 부모 오브젝트
    public GameObject shopUI; // 매점 UI 오브젝트
    private int balance = 10000; // 보유 금액 (Inspector에서 설정 가능)
    private bool isShopOpen = false; // 매점 창 열림 상태

    void Start()
    {
        // 보유 금액을 UI에 업데이트
        UpdateBalanceDisplay();
        messageText.text = "";
        speechBubble.SetActive(false); // 말풍선 비활성화
    }

    // 보유 금액을 UI에 업데이트하는 메소드
    private void UpdateBalanceDisplay()
    {
        balanceText.text = "보유 금액: " + balance.ToString() + "원"; // 보유 금액을 UI에 표시
    }

    // 매점 열기/닫기 메서드
    public void ToggleShop()
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

        // "어서오세요~" 말풍선 표시
        ShowSpeechBubble("어서오세요~");
    }

    private void CloseShop()
    {
        shopUI.SetActive(false); // 매점 UI 비활성화
        isShopOpen = false;

        // "안녕히 가세요~" 말풍선 표시
        ShowSpeechBubble("안녕히 가세요~");
    }

    // 구매하기 버튼을 클릭했을 때 호출되는 메소드
    public void CalculateTotalPrice()
    {
        int totalPrice = 0;
        bool hasSelectedQuantity = false; // 수량이 선택되었는지 확인
        StringBuilder purchasedItems = new StringBuilder();

        // ShopItem의 아이템 프리팹을 순회하며 가격과 수량 계산
        for (int i = 0; i < shopItemManager.itemContainer.childCount; i++)
        {
            // 각 아이템의 프리팹 가져오기
            GameObject itemPrefab = shopItemManager.itemContainer.GetChild(i).gameObject;

            // 수량 텍스트 가져오기
            Text quantityText = itemPrefab.transform.Find("ItemQuantityText").GetComponent<Text>();
            int quantity = int.Parse(quantityText.text); // 수량을 정수로 변환

            if (quantity > 0)
            {
                hasSelectedQuantity = true; // 하나 이상의 수량이 선택된 경우

                // 아이템의 가격 가져오기
                int price = shopItemManager.itemList[i].price;

                // 총 가격 계산
                totalPrice += price * quantity;

                // 구매한 아이템 정보 추가
                purchasedItems.AppendLine($"{shopItemManager.itemList[i].itemName} x {quantity}");
            }
        }

        if (!hasSelectedQuantity)
        {
            ShowMessage("수량을 선택해주세요.", "수량을 선택해주세요~");
            return; // 구매를 진행하지 않고 함수 종료
        }

        // 보유 금액과 총 구매 가격 비교
        if (totalPrice > balance)
        {
            ShowMessage("보유 금액이 부족합니다.", "돈이 부족해요~");
        }
        else
        {
            balance -= totalPrice; // 구매 후 보유 금액 감소
            UpdateBalanceDisplay(); // 보유 금액 업데이트
            ShowMessage("구매 성공!", "구매해주셔서 감사합니다~");

            // 구매 정보 로그 출력
            Debug.Log($"총 구매 가격: {totalPrice}원");
            Debug.Log("구매한 아이템 목록:\n" + purchasedItems.ToString());

            // 모든 수량을 0으로 초기화
            ResetQuantities();
        }
    }

    // 수량을 초기화하는 메서드
    private void ResetQuantities()
    {
        for (int i = 0; i < shopItemManager.itemContainer.childCount; i++)
        {
            GameObject itemPrefab = shopItemManager.itemContainer.GetChild(i).gameObject;
            Text quantityText = itemPrefab.transform.Find("ItemQuantityText").GetComponent<Text>();
            quantityText.text = "0"; // 수량을 0으로 설정
        }
    }

    private void ShowMessage(string message, string bubbleText)
    {
        // 기존 텍스트 메시지 업데이트
        messageText.text = message;

        // 말풍선 텍스트 업데이트 및 표시
        StartCoroutine(ShowSpeechBubble(bubbleText));

        StartCoroutine(ClearMessageAfterDelay(2f)); // 2초 후에 메시지 삭제
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = ""; // 텍스트를 빈 문자열로 변경하여 메시지를 숨김
    }

    private IEnumerator ShowSpeechBubble(string text)
    {
        speechBubbleText.text = text; // 말풍선 텍스트 설정
        speechBubble.SetActive(true); // 말풍선 활성화

        yield return new WaitForSeconds(2f); // 2초 동안 대기

        speechBubble.SetActive(false); // 말풍선 비활성화
    }
}
