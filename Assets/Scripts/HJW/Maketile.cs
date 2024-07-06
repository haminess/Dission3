using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class boxroute
{
    public Vector2 boxpos;
    public List<Vector3> boxroute_ = new List<Vector3>();
}

public class Maketile : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public static Maketile instance;
    public Makenote makenote;
    public Makemadi makemadi;
    public Audio audio_;
    public Mouseevent mos;
    public Image[] buttons;
    public Sprite[] buttonimg;
    public GameObject curpointer;
    public GameObject tile;
    public GameObject note;
    public GameObject tileprefeb;
    public Vector3 mospos;
    public int mode;
    public TextMeshProUGUI curtime;
    [Space(20)]
    public boxroute[] boxdata;
    Vector3 u = Vector3.up;
    Vector3 d = Vector3.down;
    Vector3 l = Vector3.left;
    Vector3 r = Vector3.right;
    Vector3 ur = new Vector3(1,1,0);
    Vector3 ul = new Vector3(-1,1,0);
    Vector3 dr = new Vector3(1,-1,0);
    Vector3 dl = new Vector3(-1,-1,0);
    Vector3 n = Vector3.zero;
    private bool holding;
    [Space(20)]
    [Header("shortcuts")]
    public KeyCode[] keys;
    GameObject temp;
    bool toofast;
    public bool makeingtrail;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        note.GetComponent<Image>().enabled = false;
        mode = 0;
        holding = false;
        rebutton();
    }

    // Update is called once per frame
    void Update()
    {
        mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        if (Audio.playing)
        {
            curtime.text = "Time: " + float.Parse(audio_.mainmusic.time.ToString("N2"));
        }
        if (Makenote.chartmode && !Settings.popup && !Filedataconvey.playmode)
        {
            curpointer.transform.position = new Vector2(mospos.x, mospos.y);
            if(makemadi.chart)
            {
                if(makenote.movinglongnote)
                {
                    var a = 0.5026326221305f * makenote.dur - 0.7692264517454f;
                    var start = new Vector2(makemadi.madi.transform.InverseTransformPoint(mospos.x, mospos.y, 0).x + makenote.dur * makemadi.madimultiplyer / 4 + a, 0);
                    curpointer.GetComponent<RectTransform>().anchoredPosition = start;
                }
                else
                {
                    curpointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(makemadi.madi.transform.InverseTransformPoint(mospos.x, mospos.y, 0).x, 0);
                }

                if(curpointer.transform.localPosition.x / Makemadi.instance.madimultiplyer > 0 && !Audio.playing)
                {
                    curtime.text = "Time: " + float.Parse( (curpointer.transform.localPosition.x / Makemadi.instance.madimultiplyer).ToString("N2"));

                }
            }
        }

        else
        {
            if(Settings.popup || Filedataconvey.playmode)
            {
                return;
            }
            if (mospos.y < 0 && mospos.x > 0)
            {
                curpointer.transform.position = new Vector2((int)(mospos.x + 0.5f), (int)(mospos.y- 0.5f));
            }
            else if (mospos.y > 0 && mospos.x < 0)
            {
                curpointer.transform.position = new Vector2((int)(mospos.x - 0.5f), (int)(mospos.y+ 0.5f) );
            }
            else if (mospos.y < 0 && mospos.x < 0)
            {
                curpointer.transform.position = new Vector2((int)(mospos.x - 0.5f), (int)(mospos.y-0.5f ));
            }
            else
            {
                curpointer.transform.position = new Vector2((int)(mospos.x + 0.5f), (int)(mospos.y + 0.5f));
            }
        }//box move

        if (Input.GetMouseButtonDown(0) && mode == 0) //box
        {
            if (makemadi.chart || Makenote.chartmode || mos.hidingpointer || Filedataconvey.playmode)
            {
                return;
            }
            toofast = false;
            var t = Instantiate(tileprefeb, new Vector2(curpointer.transform.position.x, curpointer.transform.position.y), Quaternion.identity, gameObject.transform);
            temp = t;
            repaint();
        }
        if (Input.GetMouseButton(0) && mode == 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //box
        {
            if (makemadi.chart || Makenote.chartmode || mos.hidingpointer || Filedataconvey.playmode)
            {
                return;
            }
            toofast = false;
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("tileoverlap"));
            if (e)
            {
                return;
            }
            makeingtrail = true;
            var t = Instantiate(tileprefeb, new Vector2(curpointer.transform.position.x, curpointer.transform.position.y), Quaternion.identity, gameObject.transform);
            temp = t;
            if (gameObject.transform.childCount > 1)
            {
                for (int i = 1; i < gameObject.transform.childCount - 1; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.layer = 0;
                }
            }
        }

        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) //longbox
        {
            if (makemadi.chart || Makenote.chartmode || Filedataconvey.playmode)
            {
                return;
            }
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("tileoverlap"));
            var o = Physics2D.Raycast(mospos, Vector3.forward, 2);
            switch (mode)
            {
                case 0: //make
                    if (temp && temp.transform.childCount > 1)
                    {
                        temp.layer = 0;
                        for (int i = 1; i < temp.transform.childCount - 1; i++)
                        {
                            temp.transform.GetChild(i).gameObject.layer = 0;
                        }
                    }
                    if (makemadi.chart || Makenote.chartmode || mos.hidingpointer || e || makemadi.slidermoving)
                    {
                        return;
                    }
                    if (Input.GetAxis("Mouse X") > 5)
                    {
                        toofast = true;
                    }
                    if(!toofast && !makemadi.slidermoving && !e)
                    {
                        makeingtrail = true;
                        var t = Instantiate(tileprefeb, new Vector2(curpointer.transform.position.x, curpointer.transform.position.y), Quaternion.identity, temp.transform);
                        t.name = "trail";
                        t.GetComponentInChildren<TextMeshPro>().text = "";
                    }


                    break;
                case 1: //erase
                    if (o && o.collider.tag == "tile")
                    {
                    repaint();
                        if(o.collider.gameObject.name == "trail")
                        {
                            Destroy(o.collider.gameObject.transform.parent.gameObject);
                        }
                        else
                        {
                            Destroy(o.collider.gameObject);

                        }
                    }
                    break;
            };
        }
        if (Input.GetMouseButtonUp(0) && (!makemadi.chart || !Makenote.chartmode)) 
        { 
            if(Filedataconvey.playmode) { return; }
            makeingtrail = false; repaint();
            if(gameObject.transform.childCount > 0)
            {
            gameObject.transform.GetChild(gameObject.transform.childCount- 1).gameObject.layer = 0; 

            }
            if(temp)
            {
            temp.transform.GetChild(temp.transform.childCount-1).gameObject.layer = 0; 

            }
        }
        if (Input.GetMouseButtonDown(0) && mode == 2)//box edit
        {
            if (Filedataconvey.playmode) { return; }
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2);
            if (holding)
            {
                if (e &&e.collider.name !="trail" &&e.collider.tag == "tile")  //switch
                {
                    curpointer.GetComponent<BoxCollider2D>().enabled = true;
                    curpointer = e.collider.gameObject;
                    curpointer.GetComponent<BoxCollider2D>().enabled = false;
                }
                else //place
                {
                    curpointer.GetComponent<BoxCollider2D>().enabled = true;
                    tile.transform.position = curpointer.transform.position;
                    curpointer = tile;
                    curpointer.GetComponent<SpriteRenderer>().enabled = true;
                    holding = false;
                    repaint();
                }
            }
            else if (e  && e.collider.tag == "tile") //hold
            {
                if (e.collider.name == "trail")
                {
                    curpointer = e.collider.transform.parent.gameObject;
                }
                else
                {
                    curpointer = e.collider.gameObject;

                }
                curpointer.GetComponent<BoxCollider2D>().enabled = false;
                tile.GetComponent<SpriteRenderer>().enabled = false;
                holding = true;
            }
        }
        #region shortcuts
        if (Input.GetKeyDown(keys[0]))
        {
            make();
        }
        if (Input.GetKeyDown(keys[1]))
        {
            erase();
        }
        if (Input.GetKeyDown(keys[2]))
        {
            edit();
        }
        if (Input.GetKeyDown(keys[3]))
        {
            makenote.transition();
        }
        if (Input.GetKeyDown(keys[4]))
        {
            audio_.playmus();
        }
        if (Input.GetKeyDown(keys[5]))
        {
            Saveboxpos();
            makenote.Savenotepos();
            makemadi.Saveinfo();
            DataManager.Instance.SaveEditorData();
            DataManager.Instance.listload();
        }
        #endregion
    }

    void repaint() //box
    {
        if(gameObject.transform.childCount == 0)
        {
            Array.Resize(ref boxdata, 0);
        }
        for (int i = 0; i < gameObject.transform.childCount-1; i++)
        {
            Array.Resize(ref boxdata, gameObject.transform.childCount - 1);
            if (boxdata[i] == null)
            {
                boxdata[i] = new boxroute();
            }
            gameObject.transform.GetChild(i + 1).GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();
            var child = gameObject.transform.GetChild(i + 1);
            boxdata[i].boxpos = new Vector2( child.localPosition.x, child.localPosition.y);
            boxdata[i].boxroute_.Clear();
            for (int j = 1; j < child.gameObject.transform.childCount; j++)
            {
                if(j == 1)
                {
                     var childpos = child.gameObject.transform.GetChild(j).transform.localPosition;

                    if (childpos.x > 0 && childpos.y > 0) //r
                    {
                        boxdata[i].boxroute_.Add(ur);
                    }
                    else if (childpos.x > 0 && childpos.y < 0) //r
                    {
                        boxdata[i].boxroute_.Add(dr);
                    }
                    else if (childpos.x < 0 && childpos.y < 0) //r
                    {
                        boxdata[i].boxroute_.Add(dl);
                    }
                    else if (childpos.x < 0 && childpos.y > 0) //r
                    {
                        boxdata[i].boxroute_.Add(ul);
                    }
                    else if (childpos.x < 0) //l
                    {
                        boxdata[i].boxroute_.Add(l);
                    }
                    else if (childpos.x > 0) //r
                    {
                        boxdata[i].boxroute_.Add(r);
                    }
                    else if(childpos.y < 0)
                    {
                        boxdata[i].boxroute_.Add(d);
                    }
                    else if (childpos.y > 0)
                    {
                        boxdata[i].boxroute_.Add(u);
                    }
                }
                else
                {
                    var prechildpos = child.gameObject.transform.GetChild(j-1).transform.position;
                    var childpos = child.gameObject.transform.GetChild(j).transform.position;
                    if (prechildpos.x - childpos.x < 0 && prechildpos.y - childpos.y < 0) //ur
                    {
                        boxdata[i].boxroute_.Add(ur);
                    }
                    else if (prechildpos.x - childpos.x < 0 && prechildpos.y - childpos.y > 0) //dr
                    {
                        boxdata[i].boxroute_.Add(dr);
                    }
                    else if (prechildpos.x - childpos.x > 0 && prechildpos.y - childpos.y > 0) //dl
                    {
                        boxdata[i].boxroute_.Add(dl);
                    }
                    else if (prechildpos.x - childpos.x > 0 && prechildpos.y - childpos.y < 0) //ul
                    {
                        boxdata[i].boxroute_.Add(ul);
                    }
                    else if (prechildpos.x - childpos.x > 0) //l
                    {
                        boxdata[i].boxroute_.Add(l);
                    }
                    else if (prechildpos.x - childpos.x < 0) //r
                    {
                        boxdata[i].boxroute_.Add(r);
                    }
                    else if (prechildpos.y - childpos.y > 0)
                    {
                        boxdata[i].boxroute_.Add(d);
                    }
                    else if (prechildpos.y - childpos.y < 0)
                    {
                        boxdata[i].boxroute_.Add(u);
                    }
                }
            }
            if(child.gameObject.transform.childCount > 1)
            {
                boxdata[i].boxroute_.Add(n);

            }
        }
    }

    public void Saveboxpos()
    {
        Array.Resize(ref editordata.boxdata, boxdata.Length);
        for(int i = 0; i < boxdata.Length;i++)
        {
            editordata.boxdata[i] = boxdata[i];
        }
        showtile();
    }

    public void boxposload()
    {
        for(int i=1; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        for(int i=0;i < boxdata.Length;i++)
        {
            var par = Instantiate(tileprefeb, new Vector2(boxdata[i].boxpos.x, boxdata[i].boxpos.y), Quaternion.identity ,gameObject.transform);
            par.transform.localPosition = boxdata[i].boxpos;
            if(boxdata[i].boxroute_.Count > 0)
            {
                var trailpos = boxdata[i].boxroute_[0];
                for (int j = 0; j < boxdata[i].boxroute_.Count - 1; j++)
                {
                    if (j == 0)
                    {
                        var trail = Instantiate(tileprefeb, new Vector2(0, 0), Quaternion.identity, par.transform);
                        trail.transform.localPosition = new Vector2(boxdata[i].boxroute_[j].x, boxdata[i].boxroute_[j].y);
                        trail.name = "trail";
                        trail.GetComponentInChildren<TextMeshPro>().text = "";
                    }
                    else
                    {
                        trailpos += boxdata[i].boxroute_[j];
                        var trail = Instantiate(tileprefeb, new Vector2(0, 0), Quaternion.identity, par.transform);
                        trail.transform.localPosition = new Vector2(trailpos.x, trailpos.y);
                        trail.name = "trail";
                        trail.GetComponentInChildren<TextMeshPro>().text = "";
                    }
                }
            }
        }
    }

    public void hidetile()
    {
        for(int i = 1; i < gameObject.transform.childCount; i++)
        {
            var obj = gameObject.transform.GetChild(i);
            if(obj.childCount > 1)
            {
                for (int j = 1; j < obj.childCount; j++)
                {
                    obj.GetChild(j).GetComponent<SpriteRenderer>().enabled = false;
                }
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
                obj.GetComponent<SpriteRenderer>().enabled = false;

        }
    }

    public void showtile()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            var obj = gameObject.transform.GetChild(i);
            if (obj.childCount > 1)
            {
                for (int j = 1; j < obj.childCount; j++)
                {
                    obj.GetChild(j).GetComponent<SpriteRenderer>().enabled = true;
                }
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
                obj.GetComponent<SpriteRenderer>().enabled = true;

        }
    }

    public void initile()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        Array.Resize(ref boxdata, 0);
    }
    #region switch
    //make 0 erase 1 edit 2
    //nor 0, sel 1
    public void nol(int a)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(i != a)
            {
                buttons[i].sprite = buttonimg[0];
            }
        }
    }
    public void erase()
    {
        if(Settings.popup)
        {
            return;
        }
        mode = 1;
        buttons[1].sprite = buttonimg[1];
        nol(1);
        if (Makenote.chartmode)
        {
            Mouseevent.nopointer = true;
            makenote.movinglongnote = false;
        }
    }
    public void make()
    {
        if (Settings.popup)
        {
            return;
        }
        mode = 0;
        buttons[0].sprite = buttonimg[1];
        nol(0);
        if (Makenote.chartmode)
        {
            Mouseevent.nopointer = false;
            makenote.movinglongnote = false;
        }
    }

    public void edit()
    {
        if (Settings.popup)
        {
            return;
        }
        mode = 2;
        buttons[2].sprite = buttonimg[1];
        nol(2);
        if (Makenote.chartmode)
        {
            Mouseevent.nopointer = true;
            makenote.movinglongnote = false;
        }
    }

    public void exitchart()
    {
        Makenote.chartmode = false;
        Mouseevent.nopointer = false;
        makenote.previewbox.GetComponent<SpriteRenderer>().enabled = false;
        rebutton();
        curpointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-14.8f, 0);
        curpointer = tile;
    }

    public void rebutton()
    {
        switch (mode)
        {
            case 0:
                buttons[0].sprite = buttonimg[1];
                nol(0);
                make();
                break;
            case 1:
                buttons[1].sprite = buttonimg[1];
                nol(1);
                erase();
                break;
            case 2:
                buttons[2].sprite = buttonimg[1];
                nol(2);
                edit();
                break;
        }
    }
    #endregion
}
