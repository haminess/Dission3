using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Connector : MonoBehaviour
{
    // 로컬데이터와 연결해주는 스크립트
    public MainGameData maingamedata => DataManager.Instance.maingamedata;
    public SoundData sounddata  => DataManager.Instance.sounddata;

    public SoundManager soundMan;
    public DataManager dataMan;

    private void Start()
    {
        UpdateData();
        FindSounManager();
        FindDataManager();
    }

    public void FindSounManager()
    {
        soundMan = GameObject.FindObjectOfType<SoundManager>();

        if(GameObject.Find("SoundManager"))
        {
            soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        }
    }
    public void FindDataManager()
    {
        dataMan = GameObject.FindObjectOfType<DataManager>();

        if (GameObject.Find("Data"))
        {
            dataMan = GameObject.Find("Data").GetComponent<DataManager>();
        }
    }

    public void UpdateData()
    {
        DataManager.Instance.LoadMainGameData();
        DataManager.Instance.LoadSoundData();
        print("로드됨" + maingamedata.synk);
    }
    public void SaveData()
    {
        DataManager.Instance.SaveMainGameData();
        DataManager.Instance.SaveSoundData();
        print("저장됨" + maingamedata.synk);
    }

    public void SetSynk(float _synk)
    {
        maingamedata.synk = _synk;
        SaveData();
    }

    public void SetJudge(float _judgeTime)
    {
        maingamedata.judge = _judgeTime;
        SaveData();
    }
    public void SetBGMVol(float _bgm)
    {
        sounddata.bgm = _bgm;
        soundMan.bgm.volume = _bgm;
        SaveData();
    }
    public void SetEffectVol(float _effect)
    {
        sounddata.effect = _effect;
        soundMan.effect.volume = _effect;
        SaveData();
    }
    public void ShowValue(TextMeshProUGUI _text)
    {
        UpdateData();
        switch(_text.name)
        {
            case "bgm":
                _text.text = (sounddata.bgm * 100).ToString("0");
                break;
            case "effect":
                _text.text = (sounddata.effect * 100).ToString("0");
                break;
            case "synk":
                _text.text = (maingamedata.synk * 100).ToString("0") + " ms";
                break;
            case "judge":
                _text.text = (maingamedata.judge * 100).ToString("0") + " ms";
                break;
        }
    }
    public void ShowValue(Slider _slider)
    {
        UpdateData();
        switch (_slider.name)
        {
            case "bgmslider":
                _slider.value = sounddata.bgm;
                break;
            case "effectslider":
                _slider.value = sounddata.effect;
                break;
            case "synkslider":
                _slider.value = maingamedata.synk;
                break;
            case "judgeslider":
                _slider.value = maingamedata.judge;
                break;
        }
    }
    public void AddValue01(Slider _slider)
    {
        _slider.value += 0.01f;
    }
    public void SubValue01(Slider _slider)
    {
        _slider.value -= 0.01f;
    }
    public void AddValue1(Slider _slider)
    {
        _slider.value += 0.1f;
    }
    public void SubValue1(Slider _slider)
    {
        _slider.value -= 0.1f;
    }
    public void AddValue(Slider _slider)
    {
        _slider.value += 1f;
    }
    public void SubValue(Slider _slider)
    {
        _slider.value -= 1f;
    }

}
