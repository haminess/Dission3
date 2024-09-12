using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPurchaseManager : MonoBehaviour
{
    public Text[] priceTexts;  // 각 아이템의 가격을 표시하는 Text UI 배열
    public Text balanceText;  // 보유 금액을 표시하는 Text UI
    private int balance = 10000;  // 보유 금액 (Inspector에서 설정 가능)
    public Text messageText;  // 메시지를 표시할 Text UI


    private ItemQuantityManager quantityManager;

    void Start()
    {
        // 수량 배열을 초기화하고 UI에 표시
        quantityManager = GetComponent<ItemQuantityManager>();
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

        for (int i = 0; i < priceTexts.Length; i++)
        {
            string priceString = priceTexts[i].text.Replace("G", "").Trim();  // 'G' 문자 제거 및 공백 제거
            int price = int.Parse(priceString);  // 가격을 정수로 변환
            int quantity = quantityManager.GetQuantity(i);  // ItemQuantityManager에서 수량 가져오기

            if (quantity > 0)
            {
                hasSelectedQuantity = true;
            }

            totalPrice += price * quantity;  // 각 아이템의 가격과 수량을 곱하여 총액에 더함
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
            Debug.Log("총 구매 가격: " + totalPrice + "G");  // 로그창에 총 구매 가격 출력
            quantityManager.ResetQuantities();  // 구매 후 수량 초기화
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