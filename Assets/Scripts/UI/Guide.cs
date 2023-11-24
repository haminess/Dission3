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

    public Player player;

    // 로컬 확인
    public string guideKey = "GuideLooked";
    public int GuideLooked;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindFirstObjectByType<Player>();
        if(player)
            player.enabled = false;
        StartCoroutine(Show());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Show()
    {
        count = 0;
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
            isPlaying = false;
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
        if (player)
            player.enabled = true;
        curCo = StartCoroutine(EndCo());
    }
    IEnumerator EndCo()
    {
        anim.Play("ExplainDown");
        yield return new WaitForSeconds(anim.GetClip("ExplainDown").length);
        count = 0;
        Destroy(gameObject);
    }

    public int GetGuide()
    {
        GuideLooked = PlayerPrefs.GetInt(guideKey, 0);
        return GuideLooked;
    }

    public void SetGuide(int _state)
    {
        GuideLooked |= _state;
        PlayerPrefs.SetInt(guideKey, GuideLooked);
    }

    public bool isLookGuide(int _state)
    {
        if ((GetGuide() & _state) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
