using UnityEngine;
using UnityEngine.UI;

public class Makemadi : MonoBehaviour
{
    public Makenote note;
    public static Makemadi instance;
    public Audio audio_;
    public Transform canvas;
    public Transform charts;
    public GameObject prefab;
    [Space(20)]
    [Header("For fucking small madi")]
    public GameObject total;
    public GameObject start;
    public GameObject Middle;
    public GameObject End;
    public float starttime;
    public float length;
    [Space(20)]
    public int bpm;
    public double sec; //total sec
    public double madi;
    [Header("Backjapyo")]
    public int up;
    public int down;
    [Space(20)]
    public GameObject endmadi;
    public int page;
    public string curmadi = "0";
    bool chart_;
    public bool chart //마디 범위 내에 들어와 있습니다.
    {
        get
        {
            return chart_;
        }
        set
        {
            if(chart_ = !value)
            {
                note.init();
            }
            chart_ = value;
        }
    }
    public bool is_smallmadi;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        madi = bpm / up * (sec / 60);
        if(starttime != 0)
        {
            var onemadilength = sec / madi; //how long is one madi 2.3
            length = (float)(starttime / onemadilength) * 519; //total length of madi
            Middle.GetComponent<RectTransform>().sizeDelta = new Vector2( length , Middle.GetComponent<RectTransform>().sizeDelta.y); //midddle madi
            total.GetComponent<BoxCollider2D>().size = new Vector2(length, total.GetComponent<RectTransform>().sizeDelta.y); //collider

            var startpos = start.GetComponent<RectTransform>().anchoredPosition;
            var endpos = End.GetComponent<RectTransform>().anchoredPosition;
            if(starttime < onemadilength)
            {
                start.GetComponent<RectTransform>().anchoredPosition = new Vector2(startpos.x + ((515 - length) / 2), startpos.y);
                End.GetComponent<RectTransform>().anchoredPosition = new Vector2(endpos.x - ((519 - length) / 2), endpos.y);
            }
            else
            {
                start.GetComponent<RectTransform>().anchoredPosition = new Vector2(startpos.x - ((length - 515) / 2), startpos.y);
                End.GetComponent<RectTransform>().anchoredPosition = new Vector2(endpos.x + ((length - 519) / 2), endpos.y);
            }

            total.GetComponent<RectTransform>().anchoredPosition = new Vector2(-575 + ((length - 519) / 2), -450);
            total.transform.SetParent(charts.transform);
        }
        for (int i = 0; i <= Mathf.Floor((float)madi); i++) //make madi
        {
            if(i == Mathf.Floor((float)madi))
            {
                var b = Instantiate(endmadi, canvas);
                if (starttime == 0)
                {
                    b.GetComponent<RectTransform>().anchoredPosition = new Vector2((505 * i) - 575, -450);
                }
                else
                {
                    b.GetComponent<RectTransform>().anchoredPosition = new Vector2((505 * i) - 575 + (length - 16), -450);
                }
                b.transform.SetParent(charts);
                b.name = "End";
            }
            else
            {
                var a = Instantiate(prefab, canvas);
                if(starttime == 0)
                {
                    a.GetComponent<RectTransform>().anchoredPosition = new Vector2((505 * i) - 575, -450);
                }
                else
                {
                    a.GetComponent<RectTransform>().anchoredPosition = new Vector2((505 * i) - 575 + (length - 16), -450);
                }
                a.transform.SetParent(charts);
                a.name = (i + 1).ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Chart"));
        var b = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Charts"));
        var c = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Smallmadi"));
        if (b)
        {
            curmadi = b.collider.name;
        }
        if (a)
        {
            chart = true;
            if(c)
            {
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = false;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = false;
                }
                is_smallmadi = true;
            }
            else if (Makenote.chartmode && Mouseevent.nopointer == false && !Audio.playing)
            {
                if (Maketile.instance.curpointer.GetComponent<Image>())
                {
                    Maketile.instance.curpointer.GetComponent<Image>().enabled = true;
                }
                else if (Maketile.instance.curpointer.GetComponent<SpriteRenderer>())
                {
                    Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;
                }
                is_smallmadi = false;

            }
        }
        else
        {
            chart = false;
        }
        if (chart && !Audio.playing)
        {
            if (Input.mouseScrollDelta.y > 0 && charts.GetComponent<RectTransform>().anchoredPosition.y < -16) //back
            {
                var pos = charts.GetComponent<RectTransform>().anchoredPosition;
                if(charts.GetComponent<RectTransform>().anchoredPosition.y > -19)
                {
                    charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -16);
                    page = 0;
                }
                else
                {
                    charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pos.y + 3);
                    audio_.index -= 3;
                    page--;
                }
            }
            if (Input.mouseScrollDelta.y < 0) //for
            {
                if(Endstamp.isend)
                {
                    return;
                }
                var pos = charts.GetComponent<RectTransform>().anchoredPosition;
                charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, pos.y - 3);
                audio_.index += 3;
                page++;
            }
        }
    }
}
