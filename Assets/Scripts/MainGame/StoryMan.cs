using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public enum STORY_EFFECT_TYPE
{
    PAUSE,
    FADE,
    MUSIC,
    NARRATION,
    SHAKE,
    CHARACTER_SHOW,
    CHARACTER_MOVE,
    CHARACTER_JUMP,
    CHARACTER_TALK,
}

[System.Serializable]
public class StoryUnit
{
    public STORY_EFFECT_TYPE type;
    public List<string> line;
    public object wParam;
    public object lParam;
    public float duration;
    public bool isParallel;

    public StoryUnit()
    {
        type = STORY_EFFECT_TYPE.PAUSE;
        line = null;
        wParam = null;
        lParam = null;
        duration = 0;
        isParallel = false;
    }
}

[System.Serializable]
public class StoryInfo
{
    public StoryUnit[] units;
}

public class StoryMan : MonoBehaviour
{
    public List<StoryInfo> storyInfos;

    public GameObject cam;
    public GameObject canvas;
    public GameObject character;

    private void Start()
    {
        StoryUnit unit = new StoryUnit();
        unit.type = STORY_EFFECT_TYPE.CHARACTER_TALK;
        unit.wParam = character;

        StartCoroutine(Execute());
    }


    IEnumerator Execute()
    {
        cam.SetActive(true);

        int sID = 0;
        int size = storyInfos.Count - 1;
        for (int i = 0; i < size; ++i)
        {
            StoryUnit unit = storyInfos[sID].units[i];
            IEnumerator func = Fade((bool)unit.wParam);

            switch (unit.type)
            {
                case STORY_EFFECT_TYPE.PAUSE:
                    yield return new WaitForSeconds((float)unit.wParam / 1000f);
                    break;
                case STORY_EFFECT_TYPE.MUSIC:
                    func = Music();
                    break;
                case STORY_EFFECT_TYPE.FADE:
                    // Fade in ����
                    func = Fade((bool)unit.wParam);
                    break;
                case STORY_EFFECT_TYPE.NARRATION:
                    // Narration ����
                    foreach (string line in unit.line)
                    {
                        // �ؽ�Ʈ�� ����ϰ� ���� �ð� ��ٸ��ϴ�.
                        Debug.Log(line);
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case STORY_EFFECT_TYPE.SHAKE:
                    // Shake ����
                    // (���� �� ��鸲�� �����ϴ� �ڵ� �߰�)
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_SHOW:
                    // Character Show ����
                    GameObject cha = Instantiate((GameObject)unit.wParam);
                    cha.transform.position = (Vector3)unit.wParam;
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_MOVE:
                    // Character Move ����
                    // (ĳ���� �������� �����ϴ� �ڵ� �߰�)
                    func = Move();
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_JUMP:
                    // Character Jump ����
                    // (ĳ���� ������ �����ϴ� �ڵ� �߰�)
                    func = Jump();
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_TALK:
                    // Character Talk ����
                    // (ĳ���� ��ȭ�� �����ϴ� �ڵ� �߰�)
                    func = Talk();
                    break;
            }

            if(unit.isParallel)
            {
                yield return StartCoroutine(func);
            }
            else
            {
                StartCoroutine(func);
            }
        }

        cam.SetActive(false);
    }
    IEnumerator Pause(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
    IEnumerator Fade(bool IsShowing)
    {
        // bool(in/out)
        Animation anim = GetComponentInChildren<Animation>();

        anim.enabled = true;

        if (IsShowing)
        {
            anim.Play("FadeAnim");
            yield return new WaitForSeconds(anim.GetClip("FadeAnim").length);
        }
        else
        {
            anim.Play("FadeOutAnim");
            yield return new WaitForSeconds(anim.GetClip("FadeOutAnim").length);
        }
        yield return null;
    }
    IEnumerator Music()
    {
        // duration, bool(on/off)
        yield return null;
    }
    IEnumerator Narration()
    {
        // line
        yield return null;
    }
    IEnumerator Move()
    {
        // character, ePos, animation
        yield return null;
    }
    IEnumerator Jump()
    {
        // character, ePos, animation
        yield return null;
    }
    IEnumerator Talk()
    {
        // character, line
        yield return null;
    }


    // 1. ���丮 �Ѱ� ���������� ���� ���丮 ������ �ε�(����)
    // 2. ���丮 ����Ʈ �����ͼ� ������� ����
    // 3. ���丮 ���� �ڵ�ȭ ��ƿ ����
    // 4. ���丮 ���̵�� ���丮 ����Ʈ�� ���丮 ��ɾ� ����Ʈ�� ����
    // 5. ���丮 ��ɾ�� ���丮 Ÿ�԰� �׿� �ʿ��� ������� ����
}
