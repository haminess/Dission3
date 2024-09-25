using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Sprite normalImage;  // 기본 상태 이미지
    public Sprite pressedImage; // 클릭 상태 이미지
    public Image buttonImage;   // 버튼의 Image 컴포넌트
    public GameObject shopUI;   // 닫을 상점 UI

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        // 버튼을 클릭했을 때 이미지 변경
        buttonImage.sprite = pressedImage;
    }

    void Update()
    {
        // 마우스를 버튼에서 떼면 이미지 원상복구 및 창 닫기
        if (Input.GetMouseButtonUp(0))
        {
            buttonImage.sprite = normalImage;
            if (shopUI.activeSelf)
            {
                shopUI.SetActive(false);
            }
        }
    }
}
