

class SettingData
{
    SoundData sound;
    SynkData synk;
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

