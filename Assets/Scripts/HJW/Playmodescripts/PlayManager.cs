using System;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    public Transform fileparent;
    public GameObject fileprefeb;
    public GameObject infopannal;
    public GameObject nofile;
    public TextMeshProUGUI projectname;
    public TextMeshProUGUI bgmname;
    public TextMeshProUGUI sec;
    public TextMeshProUGUI bpm;
    public TextMeshProUGUI notecount;
    public TextMeshProUGUI creator;
    public TextMeshProUGUI timesig;
    public AudioSource music;
    public GameObject selectedFile;
    private void Awake()
    {
        PlayManager.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.Playmodelistload();
    }
}
