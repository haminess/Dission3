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
                    // Fade in 구현
                    func = Fade((bool)unit.wParam);
                    break;
                case STORY_EFFECT_TYPE.NARRATION:
                    // Narration 구현
                    foreach (string line in unit.line)
                    {
                        // 텍스트를 출력하고 일정 시간 기다립니다.
                        Debug.Log(line);
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case STORY_EFFECT_TYPE.SHAKE:
                    // Shake 구현
                    // (진행 중 흔들림을 구현하는 코드 추가)
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_SHOW:
                    // Character Show 구현
                    GameObject cha = Instantiate((GameObject)unit.wParam);
                    cha.transform.position = (Vector3)unit.wParam;
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_MOVE:
                    // Character Move 구현
                    // (캐릭터 움직임을 구현하는 코드 추가)
                    func = Move();
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_JUMP:
                    // Character Jump 구현
                    // (캐릭터 점프를 구현하는 코드 추가)
                    func = Jump();
                    break;
                case STORY_EFFECT_TYPE.CHARACTER_TALK:
                    // Character Talk 구현
                    // (캐릭터 대화를 구현하는 코드 추가)
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


    // 1. 스토리 팩과 스테이지에 따라 스토리 데이터 로드(보류)
    // 2. 스토리 리스트 가져와서 순서대로 실행
    // 3. 스토리 실행 자동화 유틸 구현
    // 4. 스토리 아이디와 스토리 리스트는 스토리 명령어 리스트로 구성
    // 5. 스토리 명령어는 스토리 타입과 그에 필요한 정보들로 구성
}
