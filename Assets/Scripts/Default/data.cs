

using UnityEngine;
using System;
using System.Numerics;
using System.Collections.Generic;

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


[Serializable]
public class ChartData
{
    public string name = "empty_name";
    public int difficulty = 0;
    public AudioClip bgm;
    public Note[] note;
}

[Serializable]
public class ChartList : ISaveData
{
    public string   music_name;
    public List<ChartData> list;

    public string GetFileName() => "chart_" + music_name + ".json";
}



    // ������� �÷��� ������
    [Serializable]
public class PlayData : ISaveData
{
    public float    score;
    public int      perfect;
    public int      good;
    public int      bad;
    public int      miss;
    public int      combo;

    public int      rank;

    public string GetFileName() => "playData.json";
}


// �÷��̾� ������ ����
[Serializable]
public class PlayerData : ISaveData
{
    public string playerName;
    public int level;
    public float health;
    public UnityEngine.Vector3 position;

    public string GetFileName() => "playerData.json";
}

// ���� ���� ������ ����
[Serializable]
public class GameSettingsData : ISaveData
{
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
    public bool isFullScreen;

    public string GetFileName() => "gameSettings.json";
}
