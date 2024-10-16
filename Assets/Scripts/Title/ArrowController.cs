//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ArrowController : MonoBehaviour
//{
//    public enum TITLE_MODE
//    {
//        START,
//        TITLE,
//        OPTION,
//        SYNK,
//        TUTORIAL,
//        SOURCE,
//        NONE
//    }

//    // arrow object
//    public GameObject Arrow;
//    public GameObject[] TargetButton;

//    public TITLE_MODE mode;
//    public int ArrowNum;

//    // Start is called before the first frame update
//    void Start()
//    {
//        ArrowNum = 0;
//        mode = 0;

//        Arrow.transform.position = TargetButton[0].transform.position;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        ModeControl();
//    }

//    void SwitchMode(TITLE_MODE _mode)
//    {

//    }

//    void Switch(int _num)
//    {

//    }

//    void ArrowControl(GameObject[] _mode)
//    {
//        if (Input.GetKeyDown(KeyCode.DownArrow) && arrow.transform.position.y > _mode[_mode.Length - 1].transform.position.y)
//        {
//            arrowPoint += 1;
//            arrow.transform.position = _mode[arrowPoint].transform.position;
//            soundmanager.SetEffect(2);
//            soundmanager.PlayEffect();
//        }
//        if (Input.GetKeyDown(KeyCode.UpArrow) && arrow.transform.position.y < _mode[0].transform.position.y)
//        {
//            arrowPoint -= 1;
//            arrow.transform.position = _mode[arrowPoint].transform.position;
//            soundmanager.SetEffect(2);
//            soundmanager.PlayEffect();
//        }
//        if (Input.GetKeyDown(KeyCode.Return))
//        {
//            _mode[arrowPoint].GetComponent<Button>().onClick.Invoke();
//        }
//    }

//    public void ModeControl()
//    {
//        switch (mode)
//        {
//            case TITLE_MODE.START:
//                if (Input.anyKeyDown)
//                {
//                    mode = TITLE_MODE.TITLE;
//                    titleLogo.Play("title_title_logo");
//                    titleUI.Play("title_titleui");
//                }
//                break;
//            case TITLE_MODE.TITLE:
//                // title mode
//                ArrowControl(TitleButton);
//                break;
//            case TITLE_MODE.OPTION:
//                // option mode
//                ArrowControl(OptionButton);
//                break;
//            case TITLE_MODE.SYNK:
//                ArrowControl(SynkButton);
//                // synk mode
//                break;
//            //case 4:
//            //    GameObject.Find("CharacterManager").GetComponent<CharacterManager_SJY>().CharacterControl();
//            //    break;


//            case TITLE_MODE.NONE:
//                // none mode
//                break;
//        }
//    }

//    public void SetMode(TITLE_MODE _mode)
//    {
//        mode = _mode;
//        arrowPoint = 0;
//        switch (mode)
//        {
//            case TITLE_MODE.TITLE:
//                arrow.transform.position = TitleButton[0].transform.position;
//                break;
//            case TITLE_MODE.OPTION:
//                arrow.transform.position = OptionButton[0].transform.position;
//                OptionButton[arrowPoint].GetComponent<Button>().onClick.Invoke();
//                break;

//        }
//    }

//    public void MoveArrow(GameObject _button)
//    {
//        arrow.transform.position = _button.transform.position;
//    }

//    public void SetDataMode(int _mode)
//    {
//        GameObject.Find("Data").GetComponent<DataManager>().mode = (DataManager.Mode)_mode;
//    }
//}
