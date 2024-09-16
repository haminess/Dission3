using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ShopItem : MonoBehaviour
{
    [System.Serializable]
    public class ItemInfo
    {
        public string itemName;
        [TextArea(3, 10)]
        public string description;
        public int price;
    }

    public GameObject itemPrefab; // 아이템을 표시할 UI 프리팹
    public Transform itemContainer; // 아이템을 배치할 부모 객체 (UI)

    public float offsetX = 10f;
    public float offsetY = 0f;

    private RectTransform canvasRectTransform;
    public GameObject descriptionPopupPrefab;  // 설명 팝업 프리팹
    private List<GameObject> instantiatedPopups = new List<GameObject>();  // 생성된 팝업들 관리

    // 아이템 리스트
    public List<ItemInfo> itemList = new List<ItemInfo>();

    private void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // 아이템 리스트에 항목 추가
        itemList.Add(new ItemInfo
        {
            itemName = "Sword",
            description = "A sharp blade.",
            price = 500
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Shield",
            description = "A sturdy shield.",
            price = 300
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Potion",
            description = "Restores health.",
            price = 50
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Sword",
            description = "A sharp blade.",
            price = 500
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Shield",
            description = "A sturdy shield.",
            price = 300
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Potion",
            description = "Restores health.",
            price = 50
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Sword",
            description = "A sharp blade.",
            price = 500
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Shield",
            description = "A sturdy shield.",
            price = 300
        });

        itemList.Add(new ItemInfo
        {
            itemName = "Potion",
            description = "Restores health.",
            price = 50
        });

        // 모든 아이템 동적으로 생성 및 표시
        PopulateItemDisplay();
    }

    private void PopulateItemDisplay()
    {
        foreach (var item in itemList)
        {
            // 아이템 프리팹을 동적으로 생성
            GameObject newItem = Instantiate(itemPrefab, itemContainer);

            // 생성된 프리팹의 Text 컴포넌트들에 정보 설정
            Text nameText = newItem.transform.Find("ItemNameText").GetComponent<Text>();
            Text priceText = newItem.transform.Find("ItemPriceText").GetComponent<Text>();
            Text quantityText = newItem.transform.Find("ItemQuantityText").GetComponent<Text>(); // 수량 텍스트 가져오기

            if (nameText != null) nameText.text = item.itemName;
            if (priceText != null) priceText.text = item.price.ToString() + " G";

            // 수량 텍스트 기본값 설정 (예: 초기값 1)
            if (quantityText != null) quantityText.text = "0";  // 수량을 기본 0으로 설정

            // + 및 - 버튼 가져오기
            Button increaseButton = newItem.transform.Find("IncreaseButton").GetComponent<Button>();
            Button decreaseButton = newItem.transform.Find("DecreaseButton").GetComponent<Button>();

            // + 버튼을 클릭할 때 수량 증가

            // - 버튼을 클릭할 때 수량 감소
            decreaseButton.onClick.AddListener(() => ChangeQuantity(quantityText, -1));

            // 설명 팝업을 동적으로 생성하고 숨김 처리
            GameObject descriptionPopup = Instantiate(descriptionPopupPrefab, canvasRectTransform);  // canvasRectTransform에 배치
            descriptionPopup.SetActive(false);  // 처음에는 팝업이 숨겨져 있어야 함
            instantiatedPopups.Add(descriptionPopup);  // 생성된 팝업 리스트에 추가

            // 팝업 텍스트 설정
            Text descriptionText = descriptionPopup.transform.Find("ItemDescriptionText").GetComponent<Text>();
            if (descriptionText != null)
            {
                descriptionText.text = item.description;
            }

            // 아이템에 PopupHandler 추가하고 팝업과 연결
            PopupHandler popupHandler = newItem.AddComponent<PopupHandler>();
            popupHandler.Initialize(descriptionPopup, this, newItem);
        }
    }

    // 수량을 변경하는 메서드
    private void ChangeQuantity(Text quantityText, int amount)
    {
        int currentQuantity = int.Parse(quantityText.text);
        currentQuantity += amount;

        // 수량이 0 미만이 되지 않도록 설정
        if (currentQuantity < 0)
        {
            currentQuantity = 0;
        }

        quantityText.text = currentQuantity.ToString();
    }

    // 팝업을 관리하는 별도의 클래스 (아이템에 붙여서 개별 팝업을 관리함)
    public class PopupHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject descriptionPopup;
        private RectTransform popupRectTransform;
        private ShopItem shopItemScript;
        private GameObject linkedItem; // 팝업이 표시될 아이템

        public void Initialize(GameObject popup, ShopItem shopItemScript, GameObject linkedItem)
        {
            this.descriptionPopup = popup;
            this.popupRectTransform = popup.GetComponent<RectTransform>();
            this.shopItemScript = shopItemScript;
            this.linkedItem = linkedItem;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (descriptionPopup != null)
            {
                descriptionPopup.SetActive(true);
                UpdatePopupPosition();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
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
                shopItemScript.canvasRectTransform,
                linkedItem.GetComponent<RectTransform>().position,
                null,
                out itemPosition
            );

            // 아이템 위치를 기준으로 팝업의 위치를 설정
            Vector2 popupPosition = itemPosition + new Vector2(shopItemScript.offsetX, shopItemScript.offsetY);

            // 팝업이 화면 밖으로 나가지 않도록 경계 조정
            float rightEdge = shopItemScript.canvasRectTransform.rect.width - popupRectTransform.rect.width;
            float topEdge = shopItemScript.canvasRectTransform.rect.height - popupRectTransform.rect.height;
            popupPosition.x = Mathf.Min(popupPosition.x, rightEdge);
            popupPosition.y = Mathf.Min(popupPosition.y, topEdge);

            popupRectTransform.anchoredPosition = popupPosition;
        }
    }
}
