using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filedataconvey : MonoBehaviour
{
    public DataManager dataManager;
    private void Start()
    {
        dataManager = GameObject.Find("Data").GetComponent<DataManager>();
    }
    public void dataconvey()
    {
        dataManager.LoadEditorData(gameObject.name);
    }

    public void deletewarn()
    {
        Makemadi.instance.delete_ui.SetActive(true);
        Makemadi.instance.delete_obj = gameObject;
    }
}
