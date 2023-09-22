using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Maketile : MonoBehaviour
{
    public static Maketile instance;
    public Makenote makenote;
    public Makemadi makemadi;
    public Image[] buttons;
    public Sprite[] buttonimg;
    public GameObject curpointer;
    public GameObject fakepointer;
    public GameObject tile;
    public GameObject note;
    public GameObject prefeb;
    public Vector3 mospos;
    public int index;
    public int mode;
    [Space(20)]
    public Vector2[] boxpos;
    private bool holding;
    [Space(20)]
    [Header("Note")]
    public Sprite defaultnoteimg;
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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        curmadiobj = GameObject.Find("0");
        note.GetComponent<SpriteRenderer>().enabled = false;
        index = 1;
        mode = 0;
        holding = false;
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
        rebutton();
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


        if (Input.GetMouseButtonDown(0)) //box
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
                case 2: //edit, curpointer is following
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
                    break;
            };
        } //box edit
        repaint();
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
                ebun = 0.25f;
                zabun = 0.5f;
                palbun = 1;
                sipukbun = 2;
                samsipebun = 4;
                break;
            case 16: //unm x, ebun x
                zabun = 0.25f;
                palbun = 0.5f;
                sipukbun = 1;
                samsipebun = 2;
                break;
            case 32: //unm x, ebun x, zabun x
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
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();
            Array.Resize(ref boxpos, gameObject.transform.childCount);
            boxpos[i] = gameObject.transform.GetChild(i).position;
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
            makenote.makemodeanimf();
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
            makenote.makemodeanimt();
            Makenote.mode = 0;
            note.GetComponent<SpriteRenderer>().sprite = defaultnoteimg;
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
            makenote.makemodeanimf();
            Mouseevent.nopointer = true;
        }
    }

    public void exitchart()
    {
        Makenote.chartmode = false;
        Mouseevent.nopointer = false;
        rebutton();
        note.GetComponent<SpriteRenderer>().enabled = false;
        tile.GetComponent<SpriteRenderer>().enabled = true;
        makenote.makemodeanimf();
        for (int i = 0; i < 6; i++)
        {
            makenote.noteanim[i].Play("Normal");
            makenote.noteanim[i].SetBool("boxmode", true);
        }
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
