using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // 미사용
    GameObject arrow;
    GameObject[] TitleButton;
    GameObject[] OptionButton;
    GameObject[] SynkButton;
    public GameObject[] UI;
    public GameObject[] ArrowList;
    public int arrowPoint;

    public enum TITLE_MODE
    {
        START,
        TITLE,
        OPTION,
        SYNK,
        TUTORIAL,
        SOURCE,
        CHARACTER,
        WAIT,
        NONE
    }

    public TITLE_MODE mode = TITLE_MODE.START;

    public SoundManager soundmanager;

    public Animator titleLogo;
    public Animator titleUI;

    // synk show
    public GameObject synkNote;
    float time;


    // Start is called before the first frame update
    void Start()
    {
        SetMode(TITLE_MODE.START);

        arrow = GameObject.Find("Arrow");
        arrow.transform.position = TitleButton[0].transform.position;

        soundmanager.bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowControl();
        //ModeControl();

        switch (mode)
        {
            case TITLE_MODE.START:
                if (Input.anyKeyDown)
                {
                    mode = TITLE_MODE.TITLE;
                    titleLogo.Play("title_title_logo");
                    titleUI.Play("title_titleui");
                }
                break;
        }
    }
    
    void ArrowControl()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && arrowPoint < ArrowList.Length - 1)
        {
            arrowPoint += 1;
            arrow.transform.position = ArrowList[arrowPoint].transform.position;

            // 효과음을 설정하는 코드를 작성하세요.
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && arrowPoint > 0)
        {
            arrowPoint -= 1;
            arrow.transform.position = ArrowList[arrowPoint].transform.position;

            // 효과음을 설정하는 코드를 작성하세요.
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ArrowList[arrowPoint].GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ArrowList[arrowPoint].GetComponent<Button>().onClick.Invoke();
        }
    }
    void ArrowControl(GameObject[] _mode)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && arrow.transform.position.y > _mode[_mode.Length - 1].transform.position.y)
        {
            arrowPoint += 1;
            arrow.transform.position = _mode[arrowPoint].transform.position;
            //soundmanager.SetEffect(2);
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && arrow.transform.position.y < _mode[0].transform.position.y)
        {
            arrowPoint -= 1;
            arrow.transform.position = _mode[arrowPoint].transform.position;
            //soundmanager.SetEffect(2);
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
            case TITLE_MODE.START:
                if(Input.anyKeyDown)
                {
                    SetMode(TITLE_MODE.TITLE);
                    //mode = TITLE_MODE.TITLE;
                    titleLogo.Play("title_title_logo");
                    titleUI.Play("title_titleui");
                }
                break;
            case TITLE_MODE.TITLE:
                // title mode
                ArrowControl(TitleButton);
                break;
            case TITLE_MODE.OPTION:
                // option mode
                ArrowControl(OptionButton);
                break;
            case TITLE_MODE.SYNK:
                ArrowControl(SynkButton);
                // synk mode
                break;
            case TITLE_MODE.CHARACTER:
                //GameObject.Find("CharacterManager").GetComponent<CharacterManager_SJY>().CharacterControl();
                break;


            case TITLE_MODE.NONE:
                // none mode
                break;
        }
    }

    public void SetMode(int _mode)
    {
        SetMode((TITLE_MODE)_mode);
    }

    public void SetMode(TITLE_MODE _mode)
    {
        // change mode
        mode = _mode;
        arrowPoint = 0;

        // move camera
        CameraEffect cam = FindObjectOfType<CameraEffect>();
        cam.Destination = UI[(int)_mode].transform.position;
        cam.Move();

        // move arrow
        GameObject parent = UI[(int)mode].transform.Find("button")?.gameObject;
        if(parent == null)
            parent = UI[(int)mode].transform.GetChild(0).Find("button")?.gameObject;

        if (parent)
        {
            ArrowList = new GameObject[parent.transform.childCount];
            for (int i = 0; i < parent.transform.childCount; ++i)
            {
                ArrowList[i] = parent.transform.GetChild(i).gameObject;
            }
        }

        switch (mode)
        {
            case TITLE_MODE.TITLE:
                arrow.transform.position = TitleButton[0].transform.position;
                break;
            case TITLE_MODE.OPTION:
                arrow.transform.position = OptionButton[0].transform.position;
                OptionButton[arrowPoint].GetComponent<Button>().onClick.Invoke();
                break;
        }
    }
    
    public void MoveArrow(GameObject _button)
    {
        arrow.transform.position = _button.transform.position;
    }

    public void SetDataMode(int _mode)
    {
        GameObject.Find("Data").GetComponent<DataManager>().mode = (DataManager.Mode)_mode;
    }
}
