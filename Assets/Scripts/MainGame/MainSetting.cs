using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSetting : MonoBehaviour
{
    public Slider judgeRange;
    public TextMeshProUGUI tJudgeRange;
    public Slider musicVolume;
    public TextMeshProUGUI tMusicVolume;

    // Start is called before the first frame update
    void Start()
    {
        // 연결
        judgeRange = gameObject.transform.Find("JudgeRangeSlider").GetComponent<Slider>();
        musicVolume = gameObject.transform.Find("VolumeSlider").GetComponent<Slider>();
        tJudgeRange = judgeRange.GetComponentInChildren<TextMeshProUGUI>();
        tMusicVolume = musicVolume.GetComponentInChildren<TextMeshProUGUI>();

        judgeRange.onValueChanged.AddListener(delegate { OnRangeChanged(); });
        musicVolume.onValueChanged.AddListener(delegate { OnVolumeChanged(); });


        // 로컬 데이터값 반영
        judgeRange.value = DataManager.Instance.maingamedata.judge;
        musicVolume.value = DataManager.Instance.sounddata.bgm;

        tJudgeRange.text = judgeRange.value.ToString("0.00");
        tMusicVolume.text = (musicVolume.value * 100).ToString("0");
    }
// Update Hp Bar
    private void OnRangeChanged()
    {
        MainGame.instance.userRange = judgeRange.value;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
