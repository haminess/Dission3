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
        // ¿¬°á
        judgeRange = gameObject.transform.Find("JudgeRangeSlider").GetComponent<Slider>();
        musicVolume = gameObject.transform.Find("VolumeSlider").GetComponent<Slider>();
        tJudgeRange = judgeRange.GetComponentInChildren<TextMeshProUGUI>();
        tMusicVolume = musicVolume.GetComponentInChildren<TextMeshProUGUI>();

        judgeRange.onValueChanged.AddListener(delegate { OnRangeChanged(); });
        musicVolume.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
    }
// Update Hp Bar
    private void OnRangeChanged()
    {
        MainGame.instance.userRange = judgeRange.value;
        tJudgeRange.text = judgeRange.value.ToString("0.00");
        print(MainGame.instance.userRange);
    }


    private void OnVolumeChanged()
    {
        MainGame.instance.BGM.volume = musicVolume.value;
        tMusicVolume.text = judgeRange.value.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
