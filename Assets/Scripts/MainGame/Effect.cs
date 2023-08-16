using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{


    public IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;

        // 이미지 받아오기
        SpriteRenderer sprite = obj.GetComponentInChildren<SpriteRenderer>();

        // 컬러 받아오기
        Color color = sprite.color;

        if (IsShowing)
        {
            color.a = 0f;
            sprite.color = color;

            // 1될 때까지 상승되는 투명도 대입
            while (color.a < 1f)
            {
                time += 0.02f;
                color.a = Mathf.Lerp(0, 1, time);
                sprite.color = color;
                yield return null;
            }
        }
        else if (!IsShowing)
        {
            // 투명해질 때까지 감소되는 투명도 값 대입
            while (color.a > 0f)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(1, 0, time);
                sprite.color = color;
                yield return null;
            }
        }
    }
    public IEnumerator Fade(Image obj, bool IsShowing = true)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;

        // 이미지 받아오기
        Image sprite = obj.GetComponentInChildren<Image>();

        // 컬러 받아오기
        Color color = sprite.color;

        if (IsShowing)
        {
            color.a = 0f;
            sprite.color = color;

            // 1될 때까지 상승되는 투명도 대입
            while (color.a < 1f)
            {
                time += 0.02f;
                color.a = Mathf.Lerp(0, 1, time);
                sprite.color = color;
                yield return null;
            }
        }
        else if (!IsShowing)
        {
            // 투명해질 때까지 감소되는 투명도 값 대입
            while (color.a > 0f)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(1, 0, time);
                sprite.color = color;
                yield return null;
            }
        }
    }
}
