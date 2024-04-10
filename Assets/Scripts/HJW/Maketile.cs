using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Vector2[] boxpos;
    private bool holding;
    [Space(20)]
    [Header("shortcuts")]
    public KeyCode[] keys;
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
        if (Makenote.chartmode && !Settings.popup)
        {
            curpointer.transform.position = new Vector2(mospos.x, mospos.y);
            if(makemadi.chart)
            {
                curpointer.transform.localPosition = new Vector2(makemadi.madi.transform.InverseTransformPoint( mospos.x, mospos.y, 0).x, 0);
                curtime.text = "Time: " + (curpointer.transform.localPosition.x / Makemadi.instance.madimultiplyer).ToString();
            }
        }
        else
        {
            if(Settings.popup)
            {
                return;
            }
            if (mospos.y < 0 && mospos.x > 0)
            {
                curpointer.transform.position = new Vector2((int)mospos.x + 0.5f, (int)mospos.y - 0.5f);
            }
            else if (mospos.y > 0 && mospos.x < 0)
            {
                curpointer.transform.position = new Vector2((int)mospos.x - 0.5f, (int)mospos.y + 0.5f);
            }
            else if (mospos.y < 0 && mospos.x < 0)
            {
                curpointer.transform.position = new Vector2((int)mospos.x - 0.5f, (int)mospos.y - 0.5f);
            }
            else
            {
                curpointer.transform.position = new Vector2((int)mospos.x + 0.5f, (int)mospos.y + 0.5f);
            }
        }//box move


        if (Input.GetMouseButton(0)) //box
        {
            if (makemadi.chart || Makenote.chartmode )
            {
                return;
            }
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("tile"));
            switch (mode)
            {
                case 0: //make
                    for(int i = 0; i < boxpos.Length; i++)
                    {
                        if(new Vector2(curpointer.transform.position.x, curpointer.transform.position.y) == boxpos[i])
                        {
                            return;
                        }
                    }
                    if (e || mos.hidingpointer)
                    {
                        return;
                    }
                    Instantiate(tileprefeb, new Vector2(curpointer.transform.position.x, curpointer.transform.position.y), Quaternion.identity, gameObject.transform);
                    break;
                case 1: //erase
                    if (e)
                    {
                        Destroy(e.collider.gameObject);
                    }
                    break;
            };
        } 
        if(Input.GetMouseButtonDown(0) && mode == 2)//box edit
        {
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2);
            if (holding)
            {
                if (e && e.collider.tag == "tile")  //switch
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
                }
            }
            else if (e && e.collider.tag == "tile") //hold
            {
                curpointer = e.collider.gameObject;
                e.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                tile.GetComponent<SpriteRenderer>().enabled = false;
                holding = true;
            }
        }
        repaint();
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
            Array.Resize(ref boxpos, 0);
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();
            Array.Resize(ref boxpos, gameObject.transform.childCount);
            boxpos[i] = new Vector2( MathF.Round( gameObject.transform.GetChild(i).position.x + 0.496885f), MathF.Round(gameObject.transform.GetChild(i).position.y - 0.48292f));
        }
    }

    public void Saveboxpos()
    {
        Array.Resize(ref editordata.boxpos, boxpos.Length);
        for(int i = 0; i < boxpos.Length;i++)
        {
            editordata.boxpos[i] = boxpos[i];
        }
        showtile();
    }

    public void boxposload()
    {
        for(int i=0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        for(int i=0;i < boxpos.Length;i++)
        {
            Instantiate(tileprefeb, new Vector2(boxpos[i].x - 0.496885f, boxpos[i].y + 0.48292f), Quaternion.identity ,gameObject.transform);
        }
    }

    public void hidetile()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void showtile()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void initile()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        Array.Resize(ref boxpos, 0);
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
            makenote.buttoninteraction(true);
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
            makenote.buttoninteraction(false);
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
            makenote.buttoninteraction(true);
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
