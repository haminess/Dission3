using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        // 로컬 데이터 연결
        UpdateData();

        // 씬 내부 매니저 연결
        //FindManager();
    }

    public void FindManager()
    {
        // manager 연결
        soundMan = GameObject.FindObjectOfType<SoundManager>();
        dataMan = GameObject.FindObjectOfType<DataManager>();

        // 씬 전체 매니저 오브젝트 있으면 변경해서
        GameObject smobj = GameObject.Find("SoundManager");
        GameObject dataobj = GameObject.Find("Data");

        if (dataobj)
        {
            soundMan = dataobj.GetComponentInChildren<SoundManager>();
            dataMan = dataobj.GetComponent<DataManager>();
            print("데이터 오브젝트의 사운드매니저로 연결되었습니다.");
        }
        else if (smobj)
        {
            soundMan = smobj.GetComponent<SoundManager>();
            print("사운드매니저로 연결되었습니다.");
        }
    }

    public void UpdateData()
    {
        DataManager.Instance.LoadMainGameData();
        DataManager.Instance.LoadSoundData();
    }
    public void SaveData()
    {
        DataManager.Instance.SaveMainGameData();
        DataManager.Instance.SaveSoundData();
    }

    public float GetSynk()
    {
        return maingamedata.synk;
    }
    public float GetJudge()
    {
        return maingamedata.judge;
    }
    public float GetBGMVol()
    {
        return sounddata.bgm;
    }
    public float GetEffectVol()
    {
        return sounddata.effect;
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
        switch(_text.name)
        {
            case "bgm":
                _text.text = (sounddata.bgm * 1000).ToString("0");
                break;
            case "effect":
                _text.text = (sounddata.effect * 1000).ToString("0");
                break;
            case "synk":
                _text.text = (maingamedata.synk * 1000).ToString("0") + " ms";
                break;
            case "judge":
                _text.text = (maingamedata.judge * 1000).ToString("0") + " ms";
                break;
        }
    }
    public void ShowValue(Slider _slider)
    {
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
        _slider.value += 0.001f;
    }
    public void SubValue01(Slider _slider)
    {
        _slider.value -= 0.001f;
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
