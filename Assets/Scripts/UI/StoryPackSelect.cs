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
    public string imagePath;  // Resources ���� ���� �̹��� ���
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

        // ���丮 �� ��� ���� �ҷ�����
        // �� �̸�, ����, (�� �̹���)
        LoadStoryPacks();

        // �ڽ� ��ư�� ���丮 ���� ��� �ְ� �ϱ�
        CreateStoryPackButtons();
    }


    private void LoadStoryPacks()
    {
        // �����͸Ŵ������� ���丮�� ��� �ҷ�����

        // �ӽ� ������
        infos = new StoryPackInfo[5];
        for (int i = 0; i < infos.Length; i++)
        {
            infos[i] = new StoryPackInfo(); // �� ��Ҹ� �ʱ�ȭ
        }

        infos[0].name = "�б� �̾߱�";
        infos[0].desc = "�б� �̾߱⸦ ��� �ֽ��ϴ�.";

        infos[1].name = "���丮 ��1";
        infos[1].desc = "���丮 ��1 �̾߱⸦ ��� �ֽ��ϴ�.";

        infos[2].name = "���丮 ��2";
        infos[2].desc = "���丮 ��2 �̾߱⸦ ��� �ֽ��ϴ�.";

        infos[3].name = "���丮 ��3";
        infos[3].desc = "���丮 ��3 �̾߱⸦ ��� �ֽ��ϴ�.";

        infos[4].name = "���丮 ��4";
        infos[4].desc = "���丮 ��4 �̾߱⸦ ��� �ֽ��ϴ�.";
    }

    private void CreateStoryPackButtons()
    {
        buttons = new Button[infos.Length];

        for (int i = 0; i < infos.Length; i++)
        {
            int index = i; // Ŭ������ ���� ���� ����
            StoryPackInfo packInfo = infos[i];

            //Button newButton = Instantiate(storyPackButtonPrefab, this);
            //buttons[i] = newButton;
            Button newButton = transform.GetChild(0).GetComponent<Button>();
            buttons[i] = transform.GetChild(i).GetComponent<Button>();

            // ��ư �ؽ�Ʈ ����
            Text[] texts = newButton.GetComponentsInChildren<Text>();
            if (texts.Length >= 2)
            {
                texts[0].text = packInfo.name;
                texts[1].text = packInfo.desc;
            }

            // �� �̹��� ����
            Image packImageComponent = newButton.transform.Find("PackImage")?.GetComponent<Image>();
            if (packImageComponent != null && packInfo.PackImage != null)
            {
                packImageComponent.sprite = packInfo.PackImage;
            }

            // ��ư interactable ����
            //buttons[i].interactable = packInfo.isAvailable;

            // Ŭ�� �̺�Ʈ ���� - �ε��� ����
            buttons[i].onClick.AddListener(() => OnStoryPackSelected(index));
        }
    }


    private void OnStoryPackSelected(int index)
    {
        if (index >= 0 && index < infos.Length)
        {
            StoryPackInfo selectedPack = infos[index];
            Debug.Log($"���õ� ���丮 ��: {selectedPack.name} (�ε���: {index})");

            StoryManager.Instance.packID = index;
            ui_name.text = selectedPack.name;
            ui_info.text = selectedPack.desc;
        }
    }

}
