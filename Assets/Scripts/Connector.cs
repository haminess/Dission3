using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Connector : MonoBehaviour
{
    // ���õ����Ϳ� �������ִ� ��ũ��Ʈ
    public MainGameData maingamedata => DataManager.Instance.maingamedata;
    public SoundData sounddata  => DataManager.Instance.sounddata;

    public SoundManager soundMan;
    public DataManager dataMan;

    private void Start()
    {
        // ���� ������ ����
        UpdateData();

        // �� ���� �Ŵ��� ����
        //FindManager();
    }

    public void FindManager()
    {
        // manager ����
        soundMan = GameObject.FindObjectOfType<SoundManager>();
        dataMan = GameObject.FindObjectOfType<DataManager>();

        // �� ��ü �Ŵ��� ������Ʈ ������ �����ؼ�
        GameObject smobj = GameObject.Find("SoundManager");
        GameObject dataobj = GameObject.Find("Data");

        if (dataobj)
        {
            soundMan = dataobj.GetComponentInChildren<SoundManager>();
            dataMan = dataobj.GetComponent<DataManager>();
            print("������ ������Ʈ�� ����Ŵ����� ����Ǿ����ϴ�.");
        }
        else if (smobj)
        {
            soundMan = smobj.GetComponent<SoundManager>();
            print("����Ŵ����� ����Ǿ����ϴ�.");
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
