using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPurchaseManager : MonoBehaviour
{
    public Text balanceText;  // 보유 금액을 표시하는 Text UI
    public Text messageText;  // 메시지를 표시할 Text UI
    public ShopItem shopItemManager;  // ShopItem 스크립트를 참조
    public GameObject itemContainer;  // 아이템들을 담고 있는 부모 오브젝트

    private int balance = 10000;  // 보유 금액 (Inspector에서 설정 가능)

    void Start()
    {
        // 보유 금액을 UI에 업데이트
        UpdateBalanceDisplay();
        messageText.text = "";
    }

    // 보유 금액을 UI에 업데이트하는 메소드
    private void UpdateBalanceDisplay()
    {
        balanceText.text = "보유 금액: " + balance.ToString() + "원";  // 보유 금액을 UI에 표시
    }

    // 구매하기 버튼을 클릭했을 때 호출되는 메소드
    public void CalculateTotalPrice()
    {
        int totalPrice = 0;
        bool hasSelectedQuantity = false;  // 수량이 선택되었는지 확인

        // ShopItem의 아이템 프리팹을 순회하며 가격과 수량 계산
        for (int i = 0; i < shopItemManager.itemContainer.childCount; i++)
        {
            // 각 아이템의 프리팹 가져오기
            GameObject itemPrefab = shopItemManager.itemContainer.GetChild(i).gameObject;

            // 수량 텍스트 가져오기
            Text quantityText = itemPrefab.transform.Find("ItemQuantityText").GetComponent<Text>();
            int quantity = int.Parse(quantityText.text);  // 수량을 정수로 변환

            if (quantity > 0)
            {
                hasSelectedQuantity = true;  // 하나 이상의 수량이 선택된 경우
            }

            // 아이템의 가격 가져오기
            int price = shopItemManager.itemList[i].price;

            // 총 가격 계산
            totalPrice += price * quantity;
        }

        if (!hasSelectedQuantity)
        {
            ShowMessage("수량을 선택해주세요.");
            return;  // 구매를 진행하지 않고 함수 종료
        }

        // 보유 금액과 총 구매 가격 비교
        if (totalPrice > balance)
        {
            ShowMessage("보유 금액이 부족합니다.");
        }
        else
        {
            balance -= totalPrice;  // 구매 후 보유 금액 감소
            UpdateBalanceDisplay();  // 보유 금액 업데이트
            ShowMessage("구매 성공!");
            Debug.Log("총 구매 가격: " + totalPrice + "원");  // 로그창에 총 구매 가격 출력

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
            quantityText.text = "0";  // 수량을 0으로 설정
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;  // 메시지 텍스트를 설정
        StartCoroutine(ClearMessageAfterDelay(2f));  // 2초 후에 메시지 삭제
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";  // 텍스트를 빈 문자열로 변경하여 메시지를 숨김
    }
}
