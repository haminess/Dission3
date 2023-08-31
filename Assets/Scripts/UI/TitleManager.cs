using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject arrow;
    public GameObject[] TitleButton;
    public GameObject[] OptionButton;
    public int arrowPoint;
    public int mode;

    public SoundManager soundmanager;

    public Animator titleLogo;
    public Animator titleUI;

    // synk 오브젝트
    public GameObject synkNote;
    float time;


    // Start is called before the first frame update
    void Start()
    {
        arrowPoint = 0;
        mode = 0;


        arrow = GameObject.Find("Arrow");
        arrow.transform.position = TitleButton[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ModeControl();
    }
    
    void ArrowControl(GameObject[] _mode)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && arrow.transform.position.y > _mode[_mode.Length - 1].transform.position.y)
        {
            arrowPoint += 1;
            arrow.transform.position = _mode[arrowPoint].transform.position;
            soundmanager.SetEffect(2);
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && arrow.transform.position.y < _mode[0].transform.position.y)
        {
            arrowPoint -= 1;
            arrow.transform.position = _mode[arrowPoint].transform.position;
            soundmanager.SetEffect(2);
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _mode[arrowPoint].GetComponent<Button>().onClick.Invoke();
        }
    }

    public void ModeControl()
    {
        switch(mode)
        {
            case 0:
                if(Input.anyKeyDown)
                {
                    mode = 1;
                    titleLogo.Play("title_title_logo");
                    titleUI.Play("title_titleui");
                }
                break;
            case 1:
                // title mode
                ArrowControl(TitleButton);
                break;
            case 2:
                // option mode
                ArrowControl(OptionButton);
                break;
            case 3:
                // note synk mode
                break;
            case 4:
                // judge synk mode
                SynkControl();
                break;
            case 99:
                // none mode
                break;
        }
    }

    public void SetMode(int _mode)
    {
        mode = _mode;
        arrowPoint = 0;
        switch(mode)
        {
            case 1:
                arrow.transform.position = TitleButton[0].transform.position;
                break;
            case 2:
                arrow.transform.position = OptionButton[0].transform.position;
                OptionButton[arrowPoint].GetComponent<Button>().onClick.Invoke();
                break;
          
        }
    }
    
    public void MoveArrow(GameObject _button)
    {
        // 미완성
        // arrowpoint 값도 변경되게 하기
        arrow.transform.position = _button.transform.position;
    }

    public void SynkControl()
    {
        if(Input.anyKey)
        {
            print(soundmanager.bgm.time - soundmanager.bgmStartTime[0] / soundmanager.GetBeatTime(0));
        }
    }

    public IEnumerator ShowNoteSynk()
    {
        float sec = soundmanager.GetBeatTime(0);
        yield return new WaitForSeconds(soundmanager.bgmStartTime[0] + GetComponent<Connector>().maingamedata.synk);
        StartCoroutine(ShowBeat(sec));
        
        yield return new WaitForSeconds(1);
        soundmanager.bgm.Stop();
        soundmanager.SetBgm(0);
        soundmanager.bgm.Play();
    }

    public IEnumerator ShowBeat(float _sec)
    {
        Instantiate(synkNote);
        Destroy(synkNote, 1.5f);
        yield return new WaitForSeconds(_sec);
        StartCoroutine(ShowBeat(_sec));
    }
}
