using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filedataconvey : MonoBehaviour
{
    public DataManager dataManager;
    private void Start()
    {
        if(GameObject.Find("Data"))
            dataManager = GameObject.Find("Data").GetComponent<DataManager>();
    }
    public void noteidxconvey()
    {
        Makemadi.instance.note.showpreviewbox(Convert.ToInt32( gameObject.name));
    }
    public void dataconvey()
    {
        dataManager.LoadEditorData(gameObject.name);
    }
    public void dataconveyplaymode()
    {
        dataManager.Loadplaymodedata(gameObject.name);
    }
    public void deletewarn()
    {
        Makemadi.instance.delete_ui.SetActive(true);
        Makemadi.instance.delete_obj = gameObject;
    }

    public void OnSelected()
    {
        DataManager dm = GameObject.FindObjectOfType<DataManager>();
        dm.chartNum = this.gameObject.name;
    }
}
