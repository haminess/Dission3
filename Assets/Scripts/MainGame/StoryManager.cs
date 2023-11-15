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
    public GameObject npcPrefab;
    public GameObject[] stageObject;     // 스테이지 전용 오브젝트

    // 캐릭터 이미지 스프라이트
    public Sprite baby;
    public Sprite student;
    public Sprite friend1;

    public GameObject ChatPrefab;
    public Image black;

    // 스크립트 관리
    public float chatSpeed = 2;
    Dictionary<int, string[]> scripts = new Dictionary<int, string[]>();
    GameObject[] storyNpc;
    int[] npcNum;

    // 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        storyCamera.SetActive(false);
        gameCanvas.SetActive(false);
        CreateScripts();

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

    void CreateScripts()
    {
        // 1스테이지
        scripts.Add(0, new string[] { "다들 좋은아침~", // 선생님
                                      "오늘은 우리 반에 전학생이 왔어요~",
                                      "전학생은 자기소개 해볼까?",
                                      "안녕 나는 ㅇㅇ이라고 해!", // 주인공
                                      "앞으로 잘 부탁해, 친하게 지내자!",
                                      // 자리에 앉는다.
                                      "안녕 ㅇㅇ아, 만나서 반가워!!", // 친구1
                                      "내가 학교 소개해줄테니까 같이 갈래?",
                                      "좋아!"}); // 주인공
        scripts.Add(1, new string[] { "이걸로 학교 소개는 끝이야!", // 친구1
                                      "앞으로 재미있게 학교생활 해보자",
                                      "아 맞다! 이 동상은 가까이 하지 마!!",
                                      "귀신이 들렀다는 소문이 있으니까~ 흐흐",
                                      "(음.. 뭔가 수상하게 생겼네..)"}); // 주인공
        scripts.Add(2, new string[] { "학교 구조가 복잡해서 길을 잃어버렸어..",
                                      "교실이 어디였더라?"});

        // 2스테이지
        scripts.Add(3, new string[] { "너 그 소문 들었어?", // 친구1
                                      "우리 학교 급식실에서 저녁마다..",
                                      "이상한 소리가 난대..!!",
                                      "에이 말도안돼! 착각이겠지~", // 친구2
                                      "진짜라니까!!", // 친구1
                                      "..그럼 오늘 한번 확인해볼래?", // 친구2
                                      "그럴까? 날도 더운데..", // 친구1
                                      "우리 세명이서 담력테스트 해볼래?!?!", // 친구1
                                      "좋아! 그럼 저녁에 급식실에서 만나자!!", // 친구2
                                      "(헉.. 무서울 것 같은데;;)"}); // 주인공
        scripts.Add(4, new string[] { "사실 내가 몰래 급식실에서 고양이를 키우고 있었어..", // 친구3
                                      "이 고양이 사람 손을 타서",
                                      "엄마 고양이한테 버려진 거 같아..",
                                      "이렇게 일이 커질 줄 몰랐는데.. 미안!",
                                      "그런 거 였구나.. 그럼 귀신은 없는건가?", // 친구1
                                      "응? ㅇㅇ이한테 가는데?",
                                      // 고양이가 인형을 물어와 주인공에게 건네준다.
                                      "(어라? 이 인형 아는 인형인데..)", // 주인공
                                      "(어디서 봤더라..)"});
        scripts.Add(5, new string[] { "으으 너무 무서워..",
                                      "그냥 포기하고 나가야겠어.."});

        // 3스테이지
        scripts.Add(6, new string[] { "오늘은 드디어 기다리고 기다리던..", // 친구1
                                      "바로 체육 대회야!!",
                                      "다들 최선을 다해서 재미있게 해보자ㅎㅎ",
                                      "ㅇㅇ아 오늘 피구 시합 힘내! 다치지 말고!!", // 친구2
                                      "응 고마워!!"}); // 주인공
        scripts.Add(7, new string[] { // 사고 때의 상황을 연상시키는 장면이 짧게 지나간다.
                                      // (괴롭힘 당하던 시절 연상도 괜찮을 지도..)
                                      // 보건실에서 주인공이 깨어난다.
                                      "괜찮아??", // 친구2
                                      "너 피구 시합 하다가 갑자기 쓰러졌어!!",
                                      "대체 왜 그런거야?",
                                      "나도 잘 모르겠어..", // 주인공
                                      "갑자기 머리가 아파서 정신을 잃은 것 같아..",
                                      "(무언가 떠오르려 했던 것 같은데..)"});
        scripts.Add(8, new string[] { // 보건실에서 주인공이 깨어난다.
                                      "너 피구 시합 하다가 갑자기 쓰러졌어!!", // 친구2
                                      "대체 왜 그런거야?",
                                      "갑자기 머리가 아파서 정신을 잃은 것 같아.."}); // 주인공

        // 4스테이지
        scripts.Add(9, new string[] { "어라..? 이 문은 왜 잠겨있는거지?", // 주인공
                                      "안에서 뭔가 소리가 들리는 것 같기도 하고..",
                                      "들어가보고 싶은데..",
                                      "학교 안에 문 여는 도구가 있지 않을까?",
                                      "한번 찾아보자!"});
        scripts.Add(10, new string[] { // 문을 열고 들어가보니 거울(유리구슬)에서 소리가 나고있다.
                                      "이제 더 이상 제가 할 수 있는게 없군요.",
                                      "전부 환자분의 마음먹기에 달렸습니다..",
                                      "ㅇㅇ아.. 엄마는 믿고 있어..",
                                      "다른 사람들이 아무리 포기하라고 해도",
                                      "엄마는 절대 ㅇㅇ이 포기 안하고 계속 기다릴꺼야..",
                                      "ㅇㅇ아 사랑해..",
                                      "엄마…",
                                      "맞아.. 여기는 내 상상이 만들어 낸 꿈 속 세계였어..",
                                      "현실이 너무 괴로워서 내가 살고 싶던 꿈을 꾸고 있었나봐",
                                      "난 이곳에 와서 이제야 행복한데.. 어떻게 해야 할까"});
        scripts.Add(11, new string[] { "문 여는 도구를 못찾겠네..",
                                      "그냥 평범한 창고겠지 뭐!"});
        // 5스테이지
        scripts.Add(12, new string[] { // 친구와 주인공이 책상 근처에서 놀고있다.
                                      "ㅇㅇ아! 내가 오늘의 운세 봐줄까??",
                                      "오늘의 운세?",
                                      "응! 그냥 책을 펴서 나온 문장으로 운세를 알아보는거야!!",
                                      "오늘 ㅇㅇ이의 운세는..",
                                      "“마음이 향하는 곳으로 가라” 라는데?",
                                      "(내 마음이 향하는 곳..?)"});
        scripts.Add(13, new string[] { "환자분 정신이 드세요!?", // 주인공
                                      "ㅇㅇ아!! 드디어 깨어났구나..!",
                                      "ㅇㅇ아 고맙다.. 너무 고마워...",
                                      "(여기가 현실이구나.. 드디어 돌아왔어..)",
                                      "(어라..? 뭔가 긴 꿈을 꿨던 것 같은데.. 뭐였더라?)"});
        scripts.Add(14, new string[] { "ㅇㅇ아! 오늘 급식 오므라이스 나온대!!", // 주인공
                                      "너가 제일 좋아하는 거잖아 ㅎㅎ",
                                      "헉 오늘 종 치자마자 뛰어가자!!!",
                                      "응 좋아!!",
                                      "(역시 이곳이 나에게 더 행복한 것 같아..)",
                                      "(이제 여기가 내 현실이야!! 다른건 아무것도 필요없어..)"});
        scripts.Add(15, new string[] { "여느 때와 다름 없는 하루였어!", // 주인공
                                      "운세의 의미가 뭔지 모르겠네..",
                                      "역시 믿거나말거나인가!",
                                      "뭔가 잊은 것 같은데.. 기분탓이겠지?"});
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
        yield return StartCoroutine(Fade(black));
        StartCoroutine(Fade(black, false));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return new WaitForSeconds(1);

        // # 1
        GameObject teacher = NPC(baby, 6, 0);
        yield return StartCoroutine(Fade(teacher));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(teacher, Vector3.left));
        yield return StartCoroutine(Move(teacher, Vector3.left));
        yield return StartCoroutine(Move(teacher, Vector3.left));
        yield return StartCoroutine(Move(teacher, Vector3.left));
        yield return StartCoroutine(Move(teacher, Vector3.left));
        yield return StartCoroutine(Move(teacher, Vector3.left));

        yield return new WaitForSeconds(1);
        Talk(teacher, "우리 반에 전학생이 왔어요");
        yield return new WaitForSeconds(chatSpeed);
        Talk(teacher, "모두 전학생에게 집중~");
        yield return new WaitForSeconds(chatSpeed);

        GameObject players = NPC(student, 6, 0); 
        yield return StartCoroutine(Fade(players));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(players, Vector3.left));
        yield return StartCoroutine(Move(players, Vector3.left));
        yield return StartCoroutine(Move(players, Vector3.left));
        yield return StartCoroutine(Move(players, Vector3.left));
        yield return StartCoroutine(Move(players, Vector3.left));

        yield return new WaitForSeconds(1);
        Talk(players, "안녕~ 나는 시운이야");
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
        yield return StartCoroutine(Fade(black));
        Destroy(teacher);
        Destroy(players);

        // # 2
        storyCamera.transform.position = new Vector3(0, -7, -10);
        GameObject minji = NPC(friend1, 0, 0);
        players = NPC(student, -1, -8);
        yield return StartCoroutine(Fade(black, false));

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
        yield return StartCoroutine(Fade(black));
        Destroy(players);
        Destroy(minji);

        // 카메라 되돌리기
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story1_1()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // 화면 세팅
        GameObject players = NPC(student, 0, 17);
        GameObject minji = NPC(friend1, 4, 17);
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;

        yield return StartCoroutine(Fade(black, false));

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
        yield return StartCoroutine(Fade(black));

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story1_2()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // 화면 세팅
        GameObject minji = NPC(friend1, 4, 17);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(minji, "어? 00이가 어디로 간 거지?");
        yield return new WaitForSeconds(chatSpeed);


        // 화면 전환
        yield return StartCoroutine(Fade(black));
        Destroy(minji);
        storyCamera.transform.position = new Vector3(0, 0, -10);
        GameObject players = NPC(student, 0, 0);
        yield return StartCoroutine(Fade(black, false));

        Talk(players, "여기가 어디지?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "잠시 한눈 파는 사이에 민지가 사라졌어..");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환
        yield return StartCoroutine(Fade(black));

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story2()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(11, -68, -10);

        // 스토리 세팅
        GameObject hanni = NPC(friend1, 5, -70);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        GameObject players = NPC(student, 7, -72);
        yield return StartCoroutine(Fade(players));
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
        yield return StartCoroutine(Fade(black));

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story2_1()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // 스토리 세팅
        GameObject hanni = NPC(friend1, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(hanni, "우리 오늘 완전 멋졌어!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "다들 수고했어~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "다들 수고 많았어! 내일보자!!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환
        yield return StartCoroutine(Fade(black));

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story2_2()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // 스토리 세팅
        GameObject hanni = NPC(friend1, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(hanni, "무대 의상을 못 찾아서 결국 무대에 못 올라가다니..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "그러게.. 너무 아쉬워...");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // 화면 전환
        yield return StartCoroutine(Fade(black));

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story3()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject players = NPC(student, 46, -2);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        GameObject diniel = NPC(friend1, 34, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Fade(diniel));
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
        yield return StartCoroutine(Fade(black));

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story3_1()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(7, -18, -10);
        GameObject diniel = NPC(friend1, 8, -20);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 10, -20);

        yield return StartCoroutine(Fade(black, false));

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
        yield return StartCoroutine(Fade(black));

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story3_2()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject diniel = NPC(friend1, 42, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 44, -2);

        yield return StartCoroutine(Fade(black, false));

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
        yield return StartCoroutine(Fade(black));

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story4()
    {
        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);
        GameObject players = NPC(student, 38, -55);
        
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(players, "");

        yield return new WaitForSeconds(1);

        // 화면 전환
        yield return StartCoroutine(Fade(black));

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story()
    {

        // 화면 전환
        yield return StartCoroutine(Fade(black));

        // 카메라 세팅
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        // 화면 전환
        yield return StartCoroutine(Fade(black));

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    // 투명한 상태에서 나타나게 하는 효과 메서드
    IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;

        // 이미지 받아오기
        SpriteRenderer sprite = obj.GetComponentInChildren<SpriteRenderer>();

        // 컬러 받아오기
        Color color = sprite.color;

        if(IsShowing)
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
        else if(!IsShowing)
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
    IEnumerator Fade(Image obj, bool IsShowing = true)
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
