using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StoryPackInfo
{
    public string name;
    public string desc;
    public string imagePath;  // Resources 폴더 내의 이미지 경로
    public bool isAvailable;

    private Sprite _packImage;
    public Sprite PackImage
    {
        get
        {
            if (_packImage == null && !string.IsNullOrEmpty(imagePath))
            {
                _packImage = Resources.Load<Sprite>(imagePath);
            }
            return _packImage;
        }
    }
}

public class StoryPackSelect : MonoBehaviour
{
    private StoryPackInfo[] infos;
    private Button[] buttons;

    // ui
    private TextMeshProUGUI ui_name;
    private TextMeshProUGUI ui_info;

    // Start is called before the first frame update
    void Start()
    {
        ui_name = transform.parent.Find("name").GetComponent<TextMeshProUGUI>();
        ui_info = transform.parent.Find("info").GetComponent<TextMeshProUGUI>();

        // 스토리 팩 목록 파일 불러오기
        // 팩 이름, 설명, (팩 이미지)
        LoadStoryPacks();

        // 자식 버튼에 스토리 선택 기능 넣게 하기
        CreateStoryPackButtons();
    }


    private void LoadStoryPacks()
    {
        // 데이터매니저에서 스토리팩 목록 불러오기

        // 임시 데이터
        infos = new StoryPackInfo[5];
        for (int i = 0; i < infos.Length; i++)
        {
            infos[i] = new StoryPackInfo(); // 각 요소를 초기화
        }

        infos[0].name = "학교 이야기";
        infos[0].desc = "학교 이야기를 담고 있습니다.";

        infos[1].name = "스토리 팩1";
        infos[1].desc = "스토리 팩1 이야기를 담고 있습니다.";

        infos[2].name = "스토리 팩2";
        infos[2].desc = "스토리 팩2 이야기를 담고 있습니다.";

        infos[3].name = "스토리 팩3";
        infos[3].desc = "스토리 팩3 이야기를 담고 있습니다.";

        infos[4].name = "스토리 팩4";
        infos[4].desc = "스토리 팩4 이야기를 담고 있습니다.";
    }

    private void CreateStoryPackButtons()
    {
        buttons = new Button[infos.Length];

        for (int i = 0; i < infos.Length; i++)
        {
            int index = i; // 클로저를 위한 지역 변수
            StoryPackInfo packInfo = infos[i];

            //Button newButton = Instantiate(storyPackButtonPrefab, this);
            //buttons[i] = newButton;
            Button newButton = transform.GetChild(0).GetComponent<Button>();
            buttons[i] = transform.GetChild(i).GetComponent<Button>();

            // 버튼 텍스트 설정
            Text[] texts = newButton.GetComponentsInChildren<Text>();
            if (texts.Length >= 2)
            {
                texts[0].text = packInfo.name;
                texts[1].text = packInfo.desc;
            }

            // 팩 이미지 설정
            Image packImageComponent = newButton.transform.Find("PackImage")?.GetComponent<Image>();
            if (packImageComponent != null && packInfo.PackImage != null)
            {
                packImageComponent.sprite = packInfo.PackImage;
            }

            // 버튼 interactable 설정
            //buttons[i].interactable = packInfo.isAvailable;

            // 클릭 이벤트 설정 - 인덱스 전달
            buttons[i].onClick.AddListener(() => OnStoryPackSelected(index));
        }
    }


    private void OnStoryPackSelected(int index)
    {
        if (index >= 0 && index < infos.Length)
        {
            StoryPackInfo selectedPack = infos[index];
            Debug.Log($"선택된 스토리 팩: {selectedPack.name} (인덱스: {index})");

            StoryManager.Instance.packID = index;
            ui_name.text = selectedPack.name;
            ui_info.text = selectedPack.desc;
        }
    }

}
