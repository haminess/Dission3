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

    // synk ������Ʈ
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
                // synk mode
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
        // �̿ϼ�
        // arrowpoint ���� ����ǰ� �ϱ�
        arrow.transform.position = _button.transform.position;
    }

    public void SynkControl()
    {
        if(Input.anyKey)
        {
            
        }
    }

    public void ShowSynkNote()
    {

    }

}
