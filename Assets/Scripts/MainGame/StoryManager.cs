using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public int storyID = 0;

    public bool showButton;

    // 맵 오브젝트
    public GameObject player;
    public GameObject playerCam;
    public GameObject storyCamera;
    public GameObject gameCanvas;
    public GameObject playerPrefab;
    public GameObject ChatPrefab;
    public GameObject npcPrefab;
    public Animator black;

    public GameObject stage1;     // 일반
    public GameObject stage2;     // 축제
    public GameObject stage3;     // 물건
    public GameObject stage4;     // 졸업

    // 캐릭터 이미지 스프라이트
    public Sprite baby;
    public Sprite student;
    public Sprite friend1;


    // 스크립트 관리
    public float chatSpeed = 2;

    // 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        storyCamera.SetActive(false);
        gameCanvas.SetActive(false);

        // 스테이지 별
        // 맵 오브젝트 활성화
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton)
        {
            showButton = false;
            ShowStory(storyID);
        }
    }

    public void ShowStory(int stroyID)
    {
        this.storyID = stroyID;
        StartCoroutine(ShowStoryCo());
    }

    public IEnumerator ShowStoryCo()
    {
        // 플레이어 움직임 Off
        player.GetComponent<Player>().enabled = false;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // 게임 UI Off
        gameCanvas.SetActive(false);

        // 스토리 시작
        switch (this.storyID)
        {
            case 0000:
                yield return StartCoroutine(Story1());
                break;
            case 0001:
                yield return StartCoroutine(Story1_1());
                break;
            case 0002:
                yield return StartCoroutine(Story1_2());
                break;
            case 0003:
                yield return StartCoroutine(Story2());
                break;
            case 0004:
                yield return StartCoroutine(Story2_1());
                break;
            case 0005:
                yield return StartCoroutine(Story2_2());
                break;
            case 0006:
                yield return StartCoroutine(Story3());
                break;
            case 0007:
                yield return StartCoroutine(Story3_1());
                break;
            case 0008:
                yield return StartCoroutine(Story3_2());
                break;
            case 9999:
                yield return StartCoroutine(Tutorial());
                break;
        }



        // 플레이어 움직임 On
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;

        // 게임 UI On
        gameCanvas.SetActive(true);
    }

    // 스토리 스킵
    public void Skip()
    {
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        player.SetActive(true);
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;

    }


    IEnumerator Story1()
    {
        // 화면 전환
        black.Play("anim_fade");
        yield return new WaitForSeconds(1);
        black.Play("anim_fade_out");


        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return new WaitForSeconds(1);

        // # 1
        GameObject teacher = NPC(baby, 6, 0);
        teacher.GetComponentInChildren<Animator>().Play("anim_fade");

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 6; i++)
        {
            yield return StartCoroutine(Move(teacher, Vector3.left));
        }

        yield return new WaitForSeconds(1);
        Talk(teacher, "우리 반에 전학생이 왔어요");
        yield return new WaitForSeconds(chatSpeed);
        Talk(teacher, "모두 전학생에게 집중~");
        yield return new WaitForSeconds(chatSpeed);

        GameObject players = NPC(student, 6, 0);
        players.GetComponentInChildren<Animator>().Play("anim_fade");

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 5; i++)
        {
            yield return StartCoroutine(Move(players, Vector3.left));
        }

        yield return new WaitForSeconds(1);
        Talk(players, "안녕~ 나는 순이야");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "다들 잘 부탁해!");
        yield return new WaitForSeconds(chatSpeed);

        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.down));
        yield return StartCoroutine(Move(players, Vector3.left));

        // 화면 전환
        black.Play("anim_fade");
        Destroy(teacher);
        Destroy(players);
        yield return new WaitForSeconds(1);
        black.Play("anim_fade_out");

        // # 2
        storyCamera.transform.position = new Vector3(0, -7, -10);
        GameObject minji = NPC(friend1, 0, 0);
        players = NPC(student, -1, -8);

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.down));

        yield return new WaitForSeconds(1);
        Talk(minji, "안녕! 나는 이 반 회장 민지야");
        yield return new WaitForSeconds(chatSpeed);
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;
        Talk(minji, "지금부터 너에게 학교를 소개시켜줄게!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "그래!");
        yield return new WaitForSeconds(chatSpeed);

        // Story Off
        black.Play("anim_fade");
        yield return new WaitForSeconds(1);
        Destroy(players);
        Destroy(minji);

        // 카메라 되돌리기
        black.Play("anim_fade_out");
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }

    IEnumerator Story1_1()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // 화면 세팅
        GameObject players = NPC(student, 0, 17);
        GameObject minji = NPC(friend1, 4, 17);
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;


        yield return new WaitForSeconds(1);

        Talk(minji, "대화하다 보니 우리 꽤나 공통점이 많은 것 같아!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(minji, "앞으로도 친하게 지내자~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "응! 학교를 소개시켜 줘서 고마워~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "나도 잘 부탁해!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }

    IEnumerator Story1_2()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // 화면 세팅
        GameObject minji = NPC(friend1, 4, 17);


        yield return new WaitForSeconds(1);

        Talk(minji, "어? 00이가 어디로 간 거지?");
        yield return new WaitForSeconds(chatSpeed);


        // 화면 전환
        Destroy(minji);
        storyCamera.transform.position = new Vector3(0, 0, -10);
        GameObject players = NPC(student, 0, 0);

        Talk(players, "여기가 어디지?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "잠시 한눈 파는 사이에 민지가 사라졌어..");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }

    IEnumerator Story2()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(11, -68, -10);

        // 스토리 세팅
        GameObject hanni = NPC(friend1, 5, -70);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;

        yield return new WaitForSeconds(1);

        GameObject players = NPC(student, 7, -72);
        yield return StartCoroutine(Move(players, Vector2.up));
        yield return StartCoroutine(Move(players, Vector2.up));

        Talk(hanni, "왜 이렇게 늦게 왔어!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "오늘 축제인건 알지?!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "응 늦어서 미안, 빨리 가자!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "축제 너무 재밌겠다 ㅎㅎ");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }
    IEnumerator Story2_1()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // 스토리 세팅
        GameObject hanni = NPC(friend1, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);

        yield return new WaitForSeconds(1);

        Talk(hanni, "우리 오늘 완전 멋졌어!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "다들 수고했어~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "다들 수고 많았어! 내일보자!!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }
    IEnumerator Story2_2()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // 스토리 세팅
        GameObject hanni = NPC(friend1, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);

        yield return new WaitForSeconds(1);

        Talk(hanni, "무대 의상을 못 찾아서 결국 무대에 못 올라가다니..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "그러게.. 너무 아쉬워...");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);


    }

    IEnumerator Story3()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject players = NPC(student, 46, -2);


        yield return new WaitForSeconds(1);

        GameObject diniel = NPC(friend1, 34, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));
        yield return StartCoroutine(Move(diniel, Vector3.right));

        Talk(diniel, "큰일났어..!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "우리 반티 사려고 모았던..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "학급비가 사라졌어..!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "이동수업 하면서 어디 떨어뜨린 것 같아..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "혹시 괜찮으면 나랑 같이 찾아줄래..?");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }

    IEnumerator Story3_1()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(7, -18, -10);
        GameObject diniel = NPC(friend1, 8, -20);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 10, -20);


        yield return new WaitForSeconds(1);

        Talk(diniel, "너 아니였으면 정말 큰일날 뻔했어..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "넌 정말 최고의 친구야..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "정말 고마워!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "친구라면 당연히 도와야지~");

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }
    IEnumerator Story3_2()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject diniel = NPC(friend1, 42, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 44, -2);


        yield return new WaitForSeconds(1);


        Talk(players, "어떡하지.. 아무리 찾아도 안보여");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "그러게.. 어쩔 수 없지..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "내 사비로 채워야 할 것 같아..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "이 많은 돈을 어디서 구하지..?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "ㅠ_ㅠ");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }
    IEnumerator Story4()
    {
        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);
        GameObject players = NPC(student, 38, -55);
        

        yield return new WaitForSeconds(1);

        Talk(players, "");

        yield return new WaitForSeconds(1);

        // 화면 전환

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }
    IEnumerator Story()
    {

        // 화면 전환

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);


        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        // 화면 전환

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

    }

    public void Talk(GameObject talker, string chat, float tDestroy = 2)
    {
        GameObject chatBox = Instantiate(ChatPrefab, talker.transform);
        chatBox.transform.SetParent(talker.transform);
        TextMeshProUGUI chatUI = chatBox.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        chatUI.text = chat;
        Destroy(chatBox, tDestroy);
    }

    IEnumerator Move(GameObject npc, Vector3 head, float speed = 0.05f)
    {
        Vector3 curPos = npc.transform.localPosition;
        Vector3 headPos = curPos + head;
        // 캐릭터 좌표 이동
        while(true)
        {
            npc.transform.Translate(head * speed);
            if (Mathf.Abs(npc.transform.localPosition.x - headPos.x) <= 0.1f && 
                Mathf.Abs(npc.transform.localPosition.y - headPos.y) <= 0.1f)
            {
                npc.transform.localPosition = headPos;
                break;
            }
            yield return null;
        }
    }



    // npc 캐릭터 생성
    public GameObject NPC(Sprite _sprite, float _x, float _y)
    {
        GameObject npc = Instantiate(npcPrefab);
        npc.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
        npc.transform.position = new Vector2(_x, _y);
        npc.transform.SetParent(storyCamera.transform);

        return npc;
    }

    IEnumerator Tutorial()
    {
        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        storyCamera.SetActive(false);
        playerCam.SetActive(true);
    }

}
