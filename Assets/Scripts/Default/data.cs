

using UnityEngine;
using System;
using System.Numerics;

class SettingData : ISaveData
{
    SoundData   sound;
    SynkData    synk;
    public string GetFileName() => "UserSettingData.json";
}

public class SoundData
{
    public float bgm;
    public float effect;

    public SoundData()
    {
        bgm = 1;
        effect = 1;
    }
    public string GetFileName() => "playerData.json";
}

struct SynkData
{
    public float display;  // between music and note generation
    public float audio;    // between music and note judgment
}   

struct Volume
{
    public float music;
    public float effect;
}


// 플레이어 데이터 예시
[Serializable]
public class PlayerData : ISaveData
{
    public string playerName;
    public int level;
    public float health;
    public UnityEngine.Vector3 position;

    public string GetFileName() => "playerData.json";
}

// 게임 설정 데이터 예시
[Serializable]
public class GameSettingsData : ISaveData
{
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
    public bool isFullScreen;

    public string GetFileName() => "gameSettings.json";
}
