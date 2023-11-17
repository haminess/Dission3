using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide : MonoBehaviour
{
    public Animation anim;
    public TextMeshProUGUI text;
    public string[] explain;
    int count = 0;
    bool isPlaying = false;
    Coroutine curCo;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        StartCoroutine(Show());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Show()
    {
        print("애니메이션 시작" + Time.time);
        anim.Play("ExplainUp");
        yield return new WaitForSeconds(anim.GetClip("ExplainUp").length);
        curCo = StartCoroutine(ShowExplain(explain[count]));
    }
    IEnumerator ShowExplain(string _explain)
    {
        isPlaying = true;

        text.text = "";
        for (int i = 0; i < _explain.Length; i++)
        {
            text.text += explain[count][i];
            yield return new WaitForSeconds(0.05f);
        }

        count++;
        isPlaying = false;
    }

    public void NextExplain()
    {
        curCo = StartCoroutine(ShowExplain(explain[count]));
    }

    public void ClickGuide()
    {
        if (isPlaying)
        {
            StopCoroutine(curCo);
            text.text = explain[count];
            count++;
        }
        else if(!isPlaying && count < explain.Length)
        {
            NextExplain();
        }
        else
        {
            End();
        }
    }

    public void End()
    {
        curCo = StartCoroutine(EndCo());
    }
    IEnumerator EndCo()
    {
        anim.Play("ExplainDown");
        yield return new WaitForSeconds(anim.GetClip("ExplainDown").length);
        count = 0;
    }
}
