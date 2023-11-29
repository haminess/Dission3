using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Maketile : MonoBehaviour
{
    EditorData editordata => DataManager.Instance.editordata;

    public static Maketile instance;
    public Makenote makenote;
    public Makemadi makemadi;
    public Audio audio_;
    public Image[] buttons;
    public Sprite[] buttonimg;
    public GameObject curpointer;
    public GameObject fakepointer;
    public GameObject tile;
    public GameObject note;
    public GameObject emergencynote;
    public GameObject prefeb;
    public Vector3 mospos;
    public int index;
    public int mode;
    [Space(20)]
    public Vector2[] boxpos;
    private bool holding;
    [Space(20)]
    [Header("Note")]
    public GameObject curmadiobj;
    public int curmadi;
    public float curpos;
    [Space(20)]
    public float unmun = 0;
    public float ebun = 0;
    public float zabun = 0;
    public float palbun = 0;
    public float sipukbun = 0;
    public float samsipebun = 0;
    [Space(20)]
    public int divide;
    public float totalbak;
    public bool fuckkkk;
    public bool is_fucking;
    [Space(20)]
    [Header("shortcuts")]
    public KeyCode[] keys;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        curmadiobj = GameObject.Find("0");
        note.GetComponent<SpriteRenderer>().enabled = false;
        index = 1;
        mode = 0;
        bakjapyoset();
        holding = false;
        rebutton();
    }

    public void bakjapyoset()
    {
        backjapyo();
        divide = makemadi.up;
        if (divide % 2 != 0) //분자가 홀수
        {
            fuckkkk = true;
        }

        switch (makemadi.down) 
        {
            case 32:
                totalbak = 0.125f * makemadi.up;
                break;
            case 16:
                totalbak = 0.25f * makemadi.up;
                break;
            case 8:
                totalbak = 0.5f * makemadi.up;
                break;
            case 4:
                totalbak = 1 * makemadi.up;
                break;
            case 2:
                totalbak = 2 * makemadi.up;
                break;
            case 1:
                totalbak = 4 * makemadi.up;
                break;
        }//calculate total bak

    }

    // Update is called once per frame
    void Update()
    {
        mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        if (Makenote.chartmode && !Settings.popup)
        {
            if (makemadi.chart)
            {
                int c = 0;
                curmadiobj = GameObject.Find(makemadi.curmadi);
                fakepointer.transform.SetParent(curmadiobj.transform);
                curpointer.transform.SetParent(curmadiobj.transform);
                if(int.TryParse(makemadi.curmadi, out curmadi))
                {
                    c = curmadi;
                }
                if(c == 0) //small madi
                {
                    fakepointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x - 1200, 0);
                   
                }
                else
                {
                    //                                                                                              mouse     madi                 page
                    fakepointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x + 120 - (505.5f * c) + (makemadi.page * 69f) - makemadi.length, 0);
                }
                backjapyo();
                switch (Makenote.mode) 
                {
                    case 0: //4 
                        if(fuckkkk & MathF.Floor( divide * zabun) != divide * zabun)
                        {
                            fuck2(divide * zabun);
                        }
                        else
                        {
                            fuck(divide * zabun);

                        }
                        break;
                    case 1: //8
                        if (fuckkkk & MathF.Floor(divide * palbun) != divide * palbun)
                        {
                            fuck2(divide * palbun);
                        }
                        else
                        {
                            fuck(divide * palbun);

                        }
                        break;
                    case 2: //16
                        if (fuckkkk & MathF.Floor(divide * sipukbun) != divide * sipukbun)
                        {
                            fuck2(divide * sipukbun);
                        }
                        else
                        {
                            fuck(divide * sipukbun);

                        }
                        break;
                    case 3: //32
                        if (fuckkkk & MathF.Floor(divide * samsipebun) != divide * samsipebun)
                        {
                            fuck2(divide * samsipebun);
                        }
                        else
                        {
                            fuck(divide * samsipebun);

                        }
                        break;
                    case 4: //1
                        if (fuckkkk & MathF.Floor(divide * unmun) != divide * unmun)
                        {
                            fuck2(divide * unmun);
                        }
                        else
                        {
                            fuck(divide * unmun);

                        }
                        break;
                    case 5: //2
                        if (fuckkkk & MathF.Floor(divide * ebun) != divide * ebun)
                        {
                            fuck2(divide * ebun);
                        }
                        else
                        {
                            fuck(divide * ebun);

                        }
                        break;
                }//note move
            }
            else 
            {
                curpointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1069.49f, -279.01f);
                is_fucking = false;
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
            if (makemadi.chart || Makenote.chartmode)
            {
                return;
            }
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2);
            switch (mode)
            {
                case 0: //make
                    if (e && e.collider.tag == "tile" || e && e.collider.tag == "ui")
                    {
                        return;
                    }
                    Instantiate(prefeb, new Vector2(curpointer.transform.position.x, curpointer.transform.position.y), Quaternion.identity, gameObject.transform);
                    break;
                case 1: //erase
                    if (e && e.collider.tag == "tile")
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
            makenote.mode4();
        }
        if (Input.GetKeyDown(keys[5]))
        {
            makenote.mode5();
        }
        if (Input.GetKeyDown(keys[6]))
        {
            makenote.mode0();
        }
        if (Input.GetKeyDown(keys[7]))
        {
            makenote.mode1();
        }
        if (Input.GetKeyDown(keys[8]))
        {
            makenote.mode2();
        }
        if (Input.GetKeyDown(keys[9]))
        {
            makenote.mode3();
        }
        if (Input.GetKeyDown(keys[10]))
        {
            audio_.playmus();
        }
        if (Input.GetKeyDown(keys[11]))
        {
            Saveboxpos();
            makenote.Savenotepos();
            makemadi.Saveinfo();
            DataManager.Instance.SaveEditorData();
            DataManager.Instance.listload();
        }
        #endregion
    }
    public void backjapyo()
    {
        switch (makemadi.down) //only 2, 4, 8, 16, 32
        {
            case 2:
                unmun = 0.5f;
                ebun = 1;
                zabun = 2;
                palbun = 4;
                sipukbun = 8;
                samsipebun = 16;
                break;
            case 4:
                unmun = 0.25f;
                ebun = 0.5f;
                zabun = 1;
                palbun = 2;
                sipukbun = 4;
                samsipebun = 8;
                break;
            case 8: //unm x
                unmun = 0;
                ebun = 0.25f;
                zabun = 0.5f;
                palbun = 1;
                sipukbun = 2;
                samsipebun = 4;
                break;
            case 16: //unm x, ebun x
                unmun = 0;
                ebun = 0;
                zabun = 0.25f;
                palbun = 0.5f;
                sipukbun = 1;
                samsipebun = 2;
                break;
            case 32: //unm x, ebun x, zabun x
                unmun = 0;
                ebun = 0;
                zabun = 0;
                palbun = 0.25f;
                sipukbun = 0.5f;
                samsipebun = 1;
                break;
        }
    }
    public void fuck(float note)
    {
        float a = (float)492 / (note * 2);
        if (curmadi == 0 || Audio.playing)
        {
            return;
        }
        is_fucking = true;
        for (int i = 0; i < note * 2 - 1; i += 2)
        {
            if (-246 + a * i <= fakepointer.transform.localPosition.x && fakepointer.transform.localPosition.x < -246 + a * (i + 2))
            {
                curpointer.GetComponent<RectTransform>().localPosition = new Vector2(-246 + a * (i + 1), 2);
                curpos = i + 1;
            }
        }
    }
    public void fuck2(float note) //분자 홀수 4분음표 사이에 들어감
    {
        float a = (float)492 / (note * 2);
        if (curmadi == 0 || Audio.playing)
        {
            return;
        }
        is_fucking = true;
        for (int i = 0; i < note * 2 + MathF.Floor( note); i += 2)
        {
            if (-246 + (a * (i + 1)) <= fakepointer.transform.localPosition.x && fakepointer.transform.localPosition.x < -246 + (a * (i + 3)))
            {
                curpointer.GetComponent<RectTransform>().localPosition = new Vector2(-246 + (a * (i + 2)), 2);
                curpos = (((i + 1) -1) * 0.5f) + 1;
            }
        }
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
            if (gameObject.transform.GetChild(i).position.x > 0)
            {
                boxpos[i].x = MathF.Ceiling(gameObject.transform.GetChild(i).position.x);
            }
            else if (gameObject.transform.GetChild(i).position.x < 0)
            {
                boxpos[i].x = MathF.Floor(gameObject.transform.GetChild(i).position.x);
            }
            if (gameObject.transform.GetChild(i).position.y > 0)
            {
                boxpos[i].y = MathF.Ceiling(gameObject.transform.GetChild(i).position.y);
            }
            else if (gameObject.transform.GetChild(i).position.y < 0)
            {
                boxpos[i].y = MathF.Floor(gameObject.transform.GetChild(i).position.y);
            }
        }
    }

    public void Saveboxpos()
    {
        Array.Resize(ref editordata.boxpos, boxpos.Length);
        for(int i = 0; i < boxpos.Length;i++)
        {
            editordata.boxpos[i] = boxpos[i];

        }
    }

    public void boxposload()
    {
        for(int i=0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        for(int i=0; i < boxpos.Length;i++)
        {
            Instantiate(prefeb, boxpos[i], Quaternion.identity, gameObject.transform);
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
            makenote.dehold();
            Mouseevent.nopointer = true;
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
            makenote.dehold();
            makenote.makemodeanimt();
            Mouseevent.nopointer = false;
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
        }
    }

    public void exitchart()
    {
        Makenote.chartmode = false;
        Mouseevent.nopointer = false;
        makenote.previewbox.GetComponent<SpriteRenderer>().enabled = false;
        rebutton();
        note.GetComponent<SpriteRenderer>().enabled = false;
        tile.GetComponent<SpriteRenderer>().enabled = true;
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
