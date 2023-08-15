using UnityEngine;

public class Makemadi : MonoBehaviour
{
    public static Makemadi instance;
    public Endstamp end;
    public Transform canvas;
    public Transform charts;
    public GameObject prefab;
    [Space(20)]
    public int bpm;
    public double sec;
    public int what_four;
    [Header("Backjapyo")]
    public int up;
    public int down;
    [Space(20)]
    public double madi;
    public int page;
    public uint endcount;
    public string curmadi = "0";
    public bool chart;
    int c;
    // Start is called before the first frame update
    void Start()
    {
        endcount = 0;
        instance = this;
        madi = bpm / what_four * (sec / 60);
        for (int i = 0; i < madi; i++)
        {
            var a = Instantiate(prefab, canvas);
            a.GetComponent<RectTransform>().anchoredPosition = new Vector2((505 * i) - 575, -477);
            a.transform.SetParent(charts);
            a.name = i.ToString();
        }
        c = charts.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var a = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Chart"));
        var b = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Charts"));
        if (b)
        {
            curmadi = b.collider.name;
        }
        if (a)
        {
            chart = true;
            if (Makenote.chartmode)
            {
                Maketile.instance.curpointer.GetComponent<SpriteRenderer>().enabled = true;

            }
        }
        else
        {
            chart = false;
        }
        if (chart && !Audio.playing)
        {
            if(end.endmadi != (int)madi)
            {
                endcount = 0;
            }
            if (Input.mouseScrollDelta.y > 0) //back
            {
                if (charts.GetComponent<RectTransform>().anchoredPosition.y >= -16)
                {
                    return;
                }
                if (end.endmadi == c - 1)
                {
                    endcount--;
                }
                var pos = charts.GetComponent<RectTransform>().anchoredPosition;
                charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y + 3);
                page--;
            }
            if (Input.mouseScrollDelta.y < 0) //for
            {
                if(end.endmadi == c - 1)
                {
                    if(endcount >= 7)
                    {
                        return;
                    }
                    endcount++;
                }
                var pos = charts.GetComponent<RectTransform>().anchoredPosition;
                charts.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y - 3);
                page++;
            }

        }
    }
}
