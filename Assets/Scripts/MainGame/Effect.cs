using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{


    public IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        // time�� ���������� �ö󰡴� ��
        float time = 0f;

        // �̹��� �޾ƿ���
        SpriteRenderer sprite = obj.GetComponentInChildren<SpriteRenderer>();

        // �÷� �޾ƿ���
        Color color = sprite.color;

        if (IsShowing)
        {
            color.a = 0f;
            sprite.color = color;

            // 1�� ������ ��µǴ� ���� ����
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
            // �������� ������ ���ҵǴ� ���� �� ����
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
        // time�� ���������� �ö󰡴� ��
        float time = 0f;

        // �̹��� �޾ƿ���
        Image sprite = obj.GetComponentInChildren<Image>();

        // �÷� �޾ƿ���
        Color color = sprite.color;

        if (IsShowing)
        {
            color.a = 0f;
            sprite.color = color;

            // 1�� ������ ��µǴ� ���� ����
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
            // �������� ������ ���ҵǴ� ���� �� ����
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
