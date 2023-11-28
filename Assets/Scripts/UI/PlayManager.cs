using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    // 데이터 불러오기
    private MainGameData maingamedata => DataManager.Instance.maingamedata;
    private SoundData sounddata => DataManager.Instance.sounddata;
    private EditorData editordata => DataManager.Instance.editordata;

    public GameObject SelectPrefab;

    public void ShowSelect()
    {

    }

    public void ShowInfo()
    {
        // 데이터 불러오기
        DataManager.Instance.LoadEditorData("title");
    }
}
