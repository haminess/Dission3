using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    private GameObject MenuUI;
    private GameObject PopupUI;

    void Start()
    {
        MenuUI = GameObject.Find("MenuUI");
        PopupUI = GameObject.Find("MenuPopupUI");

        MenuUI.SetActive(false);
        PopupUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopupUI.activeSelf)
            {
                PopupUI.SetActive(false);
            }
            else
            {
                MenuUI.SetActive(!MenuUI.activeSelf);
            }
        }
    }
}
