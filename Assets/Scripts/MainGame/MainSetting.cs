using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSetting : MonoBehaviour
{
    public Slider noteRange;
    public TextMeshProUGUI tNoteRange;
    public Slider judgeRange;
    public TextMeshProUGUI tJudgeRange;
    public Slider musicVolume;
    public TextMeshProUGUI tMusicVolume;

    // Start is called before the first frame update
    void Start()
    {
        // 연결
        musicVolume = gameObject.transform.Find("NoteRangeSlider").GetComponent<Slider>();
        judgeRange = gameObject.transform.Find("JudgeRangeSlider").GetComponent<Slider>();
        musicVolume = gameObject.transform.Find("VolumeSlider").GetComponent<Slider>();
        tNoteRange = noteRange.GetComponentInChildren<TextMeshProUGUI>();
        tJudgeRange = judgeRange.GetComponentInChildren<TextMeshProUGUI>();
        tMusicVolume = musicVolume.GetComponentInChildren<TextMeshProUGUI>();

        noteRange.onValueChanged.AddListener(delegate { OnNoteRnageChanged(); });
        judgeRange.onValueChanged.AddListener(delegate { OnRangeChanged(); });
        musicVolume.onValueChanged.AddListener(delegate { OnVolumeChanged(); });


        // 로컬 데이터값 반영
        noteRange.value = DataManager.Instance.maingamedata.synk;
        judgeRange.value = DataManager.Instance.maingamedata.judge;
        musicVolume.value = DataManager.Instance.sounddata.bgm;

        tNoteRange.text = noteRange.value.ToString("0.00");
        tJudgeRange.text = judgeRange.value.ToString("0.00");
        tMusicVolume.text = (musicVolume.value * 100).ToString("0");
    }
// Update Hp Bar
    private void OnNoteRnageChanged()
    {
        MainGame.instance.notesynkRange = noteRange.value;
        tNoteRange.text = noteRange.value.ToString("0.00");

        // 로컬 저장
        DataManager.Instance.maingamedata.synk = judgeRange.value;
    }
    private void OnRangeChanged()
    {
        MainGame.instance.judgeRange = judgeRange.value;
        tJudgeRange.text = judgeRange.value.ToString("0.00");

        // 로컬 저장
        DataManager.Instance.maingamedata.judge = judgeRange.value;
    }


    private void OnVolumeChanged()
    {
        MainGame.instance.bgm.volume = musicVolume.value;
        tMusicVolume.text = (musicVolume.value * 100).ToString("0");

        // 로컬 저장
        DataManager.Instance.sounddata.bgm = musicVolume.value;
    }

}
