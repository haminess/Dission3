using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filedataconvey : MonoBehaviour
{
    public DataManager dataManager;
    public static bool playmode;
    private void Start()
    {
        if(GameObject.Find("Data"))
            dataManager = GameObject.Find("Data").GetComponent<DataManager>();
    }
    public void toplaymode()
    {
        if (playmode)//play -> edit
        {
            playmode = false;
            Makemadi.instance.editmodeui.SetActive(true);
            Makemadi.instance.editmodeiconimg.sprite = Makemadi.instance.editmodeicon[1];
            Maketile.instance.curpointer.SetActive(true);
            Maketile.instance.makenote.previewbox.SetActive(true);
            Maketile.instance.audio_.resetmusic();
            Maketile.instance.showtile();
        }
        else//edit -> play
        {
            playmode = true;
            Makemadi.instance.editmodeui.SetActive(false);
            Makemadi.instance.editmodeiconimg.sprite = Makemadi.instance.editmodeicon[0];
            Maketile.instance.curpointer.SetActive(false);
            Maketile.instance.makenote.previewbox.SetActive(false);
            Maketile.instance.makenote.notegen.dataconvey();
            Maketile.instance.audio_.resetmusic();
            Maketile.instance.hidetile();
        }
    }
    public void noteidxconvey()
    {
        int result;
        if(int.TryParse(gameObject.name, out result))
        {

        Makemadi.instance.note.showpreviewbox(Convert.ToInt32( gameObject.name));
        }
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
