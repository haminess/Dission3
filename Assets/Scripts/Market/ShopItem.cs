using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable]
    public class ItemInfo
    {
        public string itemName;
        [TextArea(3, 10)]
        public string description;
        public int price;
        // 필요한 다른 아이템 정보들을 여기에 추가하세요
    }

    public ItemInfo itemInfo;
    public GameObject descriptionPopup;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public float offsetX = 10f;
    public float offsetY = 0f;

    private RectTransform popupRectTransform;
    private RectTransform canvasRectTransform;

    private void Start()
    {
        popupRectTransform = descriptionPopup.GetComponent<RectTransform>();
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        
        // 초기 설정
        UpdateItemDisplay();
        HidePopup();
    }

    private void UpdateItemDisplay()
    {
        if (nameText != null) nameText.text = itemInfo.itemName;
        if (descriptionText != null) descriptionText.text = itemInfo.description;
        if (priceText != null) priceText.text = itemInfo.price.ToString() + " G";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPopup();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HidePopup();
    }

    private void ShowPopup()
    {
        if (descriptionPopup != null)
        {
            UpdatePopupPosition();
            descriptionPopup.SetActive(true);
        }
    }

    private void HidePopup()
    {
        if (descriptionPopup != null)
        {
            descriptionPopup.SetActive(false);
        }
    }

    private void UpdatePopupPosition()
    {
        Vector2 itemPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            GetComponent<RectTransform>().position,
            null,
            out itemPosition
        );

        Vector2 popupPosition = itemPosition + new Vector2(offsetX, offsetY);

        // 팝업이 화면 밖으로 나가지 않도록 조정
        float rightEdge = canvasRectTransform.rect.width - popupRectTransform.rect.width;
        float topEdge = canvasRectTransform.rect.height - popupRectTransform.rect.height;
        popupPosition.x = Mathf.Min(popupPosition.x, rightEdge);
        popupPosition.y = Mathf.Min(popupPosition.y, topEdge);

        popupRectTransform.anchoredPosition = popupPosition;
    }
}