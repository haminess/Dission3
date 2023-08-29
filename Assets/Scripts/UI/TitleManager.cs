using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject arrow;
    public GameObject[] TitleButton;
    public int arrowPoint;

    public SoundManager soundmanager;

    // Start is called before the first frame update
    void Start()
    {
        arrowPoint = 0;
        arrow = GameObject.Find("Arrow");

        arrow.transform.position = TitleButton[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ArrowControl();
    }
    
    void ArrowControl()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && arrow.transform.position.y > TitleButton[TitleButton.Length - 1].transform.position.y)
        {
            arrowPoint += 1;
            arrow.transform.position = TitleButton[arrowPoint].transform.position;
            soundmanager.SetEffect(2);
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && arrow.transform.position.y < TitleButton[0].transform.position.y)
        {
            arrowPoint -= 1;
            arrow.transform.position = TitleButton[arrowPoint].transform.position;
            soundmanager.SetEffect(2);
            soundmanager.PlayEffect();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TitleButton[arrowPoint].GetComponent<Button>().onClick.Invoke();
        }
    }

}
