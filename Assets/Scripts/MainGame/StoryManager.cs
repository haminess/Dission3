using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public enum SpriteNum
    {
        student,
        teacher,
        friend1,
        friend2,
        mom,
        doctor
    }
    public enum StoryNum
    {
        s1,
        s1_1,
        s1_2,
        s2,
        s2_1,
        s2_2,
        s3,
        s3_1,
        s3_2,
        s4,
        s4_1,
        s4_2,
        s5,
        s5_1,
        s5_2,
        s5_3
    }

    public int sID = 0;
    public StoryNum sId = StoryNum.s1;

    public bool showButton;

    // ?? ???????
    public GameObject player;
    public GameObject playerCam;
    public GameObject storyCamera;
    public GameObject gameCanvas;
    public GameObject playerPrefab;
    public GameObject[] stageObject;     // ???????? ???? ???????
    public Transform storyObject;

    // ��???? ????? ?????????
    public GameObject[] characterprefeb; //0 main, 1 girl, 2 boy, 3 teacher, 4 mom, 5 doc, 6 cat
    public Sprite baby;
    public Sprite student;
    public Sprite friend1;
    public GameObject black;

    public GameObject ChatPrefab;

    // ?????? ????
    public float chatSpeed = 2;
    Dictionary<int, string[]> scripts = new Dictionary<int, string[]>();
    int[][] npcNum;

    // ?????? ????

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        storyCamera.SetActive(false);
        gameCanvas.SetActive(false);
        CreateScripts();

        // ???????? ??
        // ?? ??????? ????
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton)
        {
            showButton = false;
            ShowStory(sId);
        }
    }

    void CreateScripts()
    {
        // 1스테이지
        scripts.Add(0, new string[] { "다들 좋은아침~",                      // 선생님
                                      "오늘은 우리 반에 전학생이 왔어요~",
                                      "전학생은 자기소개 해볼까?",
                                      "안녕 나는 오선지라고 해!",             // 주인공
                                      "앞으로 잘 부탁해, 친하게 지내자!",
                                      // 자리에 앉는다.
                                      "안녕 선지야, 만나서 반가워!!",          // 친구1
                                      "내가 학교 소개해줄테니까 같이 갈래?",
                                      "좋아!"});                             // 주인공
        scripts.Add(1, new string[] { "이걸로 학교 소개는 끝이야!",            // 친구1
                                      "앞으로 재미있게 학교생활 해보자",
                                      "아 맞다! 이 동상은 가까이 하지 마!!",
                                      "귀신이 들렀다는 소문이 있으니까~ 흐흐",
                                      "(음.. 뭔가 수상하게 생겼네..)"});       // 주인공
        scripts.Add(2, new string[] { "어? 선지가 어디로 간 거지?",
                                      "학교 구조가 복잡해서 길을 잃어버렸어..",
                                      "교실이 어디였더라?"});
        npcNum = new int[16][];
        npcNum[0] = new int[] { 1, 1, 1, 0, 0, 2, 2, 0 };
        npcNum[1] = new int[] { 2, 2, 2, 2, 0 };
        npcNum[2] = new int[] { 0, 0 };

        // 2스테이지
        scripts.Add(3, new string[] { "너 그 소문 들었어?",                    // 친구1
                                      "우리 학교 급식실에서 저녁마다..",
                                      "이상한 소리가 난대..!!",
                                      "에이 말도안돼! 착각이겠지~",            // 친구2
                                      "진짜라니까!!",                         // 친구1
                                      "..그럼 오늘 한번 확인해볼래?",          // 친구2
                                      "그럴까? 날도 더운데..",                 // 친구1
                                      "우리 세명이서 담력테스트 해볼래?!?!",    // 친구1
                                      "좋아! 그럼 저녁에 급식실에서 만나자!!",  // 친구2
                                      "(헉.. 무서울 것 같은데;;)"});           // 주인공
        scripts.Add(4, new string[] { "사실 내가 몰래 급식실에서 고양이를 키우고 있었어..", // 친구3
                                      "이 고양이 사람 손을 타서",
                                      "엄마 고양이한테 버려진 거 같아..",
                                      "이렇게 일이 커질 줄 몰랐는데.. 미안!",
                                      "그런 거 였구나.. 그럼 귀신은 없는건가?", // 친구1
                                      "응? 선지한테 가는데?",
                                      // 고양이가 인형을 물어와 주인공에게 건네준다.
                                      "(어라? 이 인형 아는 인형인데..)",         // 주인공
                                      "(어디서 봤더라..)"});
        scripts.Add(5, new string[] { "으으 너무 무서워..",
                                      "그냥 포기하고 나가야겠어.."});

        npcNum[3] = new int[] { 1, 1, 1, 2, 1, 2, 1, 1, 2, 0 };
        npcNum[4] = new int[] { 3, 3, 3, 3, 1, 1, 0, 0 };
        npcNum[5] = new int[] { 0, 0 };

        // 3스테이지
        scripts.Add(6, new string[] { "오늘은 드디어 기다리고 기다리던..",        // 친구1
                                      "바로 체육 대회야!!",
                                      "다들 최선을 다해서 재미있게 해보자ㅎㅎ",
                                      "선지야 오늘 피구 시합 힘내! 다치지 말고!!", // 친구2
                                      "응 고마워!!"});                           // 주인공
        scripts.Add(7, new string[] { // 사고 때의 상황을 연상시키는 장면이 짧게 지나간다.
                                      // (괴롭힘 당하던 시절 연상도 괜찮을 지도..)
                                      // 보건실에서 주인공이 깨어난다.
                                      "괜찮아??", // 친구2
                                      "너 피구 시합 하다가 갑자기 쓰러졌어!!",
                                      "대체 왜 그런거야?",
                                      "나도 잘 모르겠어..",                       // 주인공
                                      "갑자기 머리가 아파서 정신을 잃은 것 같아..",
                                      "(무언가 떠오르려 했던 것 같은데..)"});
        scripts.Add(8, new string[] { // 보건실에서 주인공이 깨어난다.
                                      "너 피구 시합 하다가 갑자기 쓰러졌어!!",      // 친구2
                                      "대체 왜 그런거야?",
                                      "갑자기 머리가 아파서 정신을 잃은 것 같아.."}); // 주인공

        npcNum[6] = new int[] { 1, 1, 1, 2, 0 };
        npcNum[7] = new int[] { 2, 2, 2, 0, 0, 0 };
        npcNum[8] = new int[] { 2, 2, 0 };

        // 4스테이지
        scripts.Add(9, new string[] { "어라..? 이 문은 왜 잠겨있는거지?",           // 주인공
                                      "안에서 뭔가 소리가 들리는 것 같기도 하고..",
                                      "들어가보고 싶은데..",
                                      "학교 안에 문 여는 도구가 있지 않을까?",
                                      "한번 찾아보자!"});
        scripts.Add(10, new string[] { // 문을 열고 들어가보니 거울(유리구슬)에서 소리가 나고있다.
                                      "이제 더 이상 제가 할 수 있는게 없군요.",
                                      "전부 환자분의 마음먹기에 달렸습니다..",
                                      "선지야.. 엄마는 믿고 있어..",
                                      "다른 사람들이 아무리 포기하라고 해도",
                                      "엄마는 절대 선지 포기 안하고 계속 기다릴꺼야..",
                                      "선지야 사랑해..",
                                      "엄마…",
                                      "맞아.. 여기는 내 상상이 만들어 낸 꿈 속 세계였어..",
                                      "현실이 너무 괴로워서 내가 살고 싶던 꿈을 꾸고 있었나봐",
                                      "난 이곳에 와서 이제야 행복한데.. 어떻게 해야 할까"});
        scripts.Add(11, new string[] { "문 여는 도구를 못찾겠네..",
                                      "그냥 평범한 창고겠지 뭐!"});
        npcNum[9] = new int[] { 0, 0, 0, 0, 0 };
        npcNum[10] = new int[] { 1, 1, 2, 2, 2, 2, 0, 0, 0, 0 };
        npcNum[11] = new int[] { 0, 0 };

        // 5스테이지
        scripts.Add(12, new string[] { // 친구와 주인공이 책상 근처에서 놀고있다.
                                      "선지야! 내가 오늘의 운세 봐줄까??",
                                      "오늘의 운세?",
                                      "응! 그냥 책을 펴서 나온 문장으로 운세를 알아보는거야!!",
                                      "오늘 선지의 운세는..",
                                      "“마음이 향하는 곳으로 가라” 라는데?",
                                      "(내 마음이 향하는 곳..?)"});
        scripts.Add(13, new string[] { "환자분 정신이 드세요!?",
                                      "선지야!! 드디어 깨어났구나..!",
                                      "선지야 고맙다.. 너무 고마워...",
                                      "(여기가 현실이구나.. 드디어 돌아왔어..)",
                                      "(어라..? 뭔가 긴 꿈을 꿨던 것 같은데.. 뭐였더라?)"});
        scripts.Add(14, new string[] { "선지야! 오늘 급식 오므라이스 나온대!!",    // 주인공
                                      "너가 제일 좋아하는 거잖아 ㅎㅎ",
                                      "헉 오늘 종 치자마자 뛰어가자!!!",
                                      "응 좋아!!",
                                      "(역시 이곳이 나에게 더 행복한 것 같아..)",
                                      "(이제 여기가 내 현실이야!! 다른 건 아무 것도 필요없어..)"});
        scripts.Add(15, new string[] { "여느 때와 다름 없는 하루였어!", // 주인공
                                      "운세의 의미가 뭔지 모르겠네..",
                                      "역시 믿거나 말거나인가!",
                                      "뭔가 잊은 것 같은데.. 기분 탓이겠지?"});
        npcNum[12] = new int[] { 1, 0, 1, 1, 2, 0 };
        npcNum[13] = new int[] { 3, 4, 4, 0, 0 };
        npcNum[14] = new int[] { 2, 2, 0, 2, 0, 0 };
        npcNum[15] = new int[] { 0, 0, 0, 0 };
    }

    public void ShowStory(int stroyID)
    {
        this.sID = stroyID;
        this.sId = (StoryNum)this.sID;
        StartCoroutine(ShowStoryCo());
    }
    public void ShowStory(StoryNum _stroyID)
    {
        this.sId = _stroyID;
        this.sID = (int)this.sId;
        StartCoroutine(ShowStoryCo());
    }

    public IEnumerator ShowStoryCo()
    {
        this.sId = (StoryNum)this.sID;
        // ?��???? ?????? Off
        player.GetComponent<Player>().enabled = false;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // ???? UI Off
        gameCanvas.SetActive(false);

        // ???? ????
        switch ((int)sId)
        {
            case 0000:
                yield return StartCoroutine(Story1());
                break;
            case 0001:
                yield return StartCoroutine(Story1Happy());
                break;
            case 0002:
                yield return StartCoroutine(Story1Sad());
                break;
            case 0003:
                yield return StartCoroutine(Story2());
                break;
            case 0004:
                yield return StartCoroutine(Story2Happy());
                break;
            case 0005:
                yield return StartCoroutine(Story2Sad());
                break;
            case 0006:
                yield return StartCoroutine(Story3());
                break;
            case 0007:
                yield return StartCoroutine(Story3Happy());
                break;
            case 0008:
                yield return StartCoroutine(Story3Sad());
                break;
            case 0009:
                yield return StartCoroutine(Story4());
                break;
            case 0010:
                yield return StartCoroutine(Story4Happy());
                break;
            case 0011:
                yield return StartCoroutine(Story4Sad());
                break;
            case 0012:
                yield return StartCoroutine(Story5());
                break;
            case 0013:
                yield return StartCoroutine(Ending1());
                break;
            case 0014:
                yield return StartCoroutine(Ending2());
                break;
            case 0015:
                yield return StartCoroutine(Story5Sad());
                break;
        }


        // ?��???? ?????? On
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;

        // ???? UI On
        gameCanvas.SetActive(true);
    }

    // ???? ???
    public void Skip()
    {
        // skip coroutine
        StopAllCoroutines();
        StartCoroutine(SkipCo());
    }

    public IEnumerator SkipCo()
    {
        yield return StartCoroutine(OffStory());

        if (MainGame.instance.mode == MainGame.Mode.Stage)
        {
            yield return StartCoroutine(MainGame.instance.OffStoryMusic());
            if (!MainGame.instance.isEnd)
            {
                MainGame.instance.GameStart();
            }
            else
            {
                MainGame.instance.GameEnd();
                yield return StartCoroutine(MainGame.instance.ShowCollection());

                yield return new WaitForSeconds(1);

                MainGame.instance.sceneManager.ToScoreScene();
            }
        }

    }

    IEnumerator Story()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 0, 0));


        // ��???? ????
        GameObject teacher = NPC(3, 6, 0);
        // ��???? ????
        Destroy(teacher);
        // ��???? ???
        yield return StartCoroutine(Move(teacher, Vector3.left, 6));
        // ��???? ???
        yield return StartCoroutine(Typing(teacher, scripts[sID][0]));

        // Story Off
        yield return StartCoroutine(Fade(black));

        // ???? ???????
        yield return StartCoroutine(SetCam(false));

    }
    public IEnumerator OffStory()
    {
        yield return StartCoroutine(Fade(black));

        storyCamera.SetActive(false);
        playerCam.SetActive(true);
        player.SetActive(true);
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return StartCoroutine(Fade(black, false));

        for (int i = 0; i < storyObject.childCount; i++)
        {
            Destroy(storyObject.GetChild(i).gameObject);
        }
    }

    IEnumerator SetCam(bool _isOn, float _x = 0, float _y = 0)
    {
        if (black.GetComponent<Image>().color.a < 0.1f)
        {
            yield return StartCoroutine(Fade(black));
        }

        if (_isOn)
        {
            // ???? ????
            playerCam.SetActive(false);
            storyCamera.SetActive(true);
            storyCamera.transform.position = new Vector3(_x, _y, -10);

            yield return StartCoroutine(Fade(black, false));

            yield return new WaitForSeconds(1);
        }
        else
        {
            storyCamera.SetActive(false);
            playerCam.SetActive(true);

            yield return StartCoroutine(Fade(black, false));

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Story1()
    {
        // ??? ???
        yield return StartCoroutine(SetCam(true, 8, -27));

        // # 1
        GameObject teacher = NPC(3, 14, -27);
        yield return StartCoroutine(Fade(teacher));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(teacher, Vector3.left, 6));
        yield return StartCoroutine(Jump(teacher, 2, 0.2f));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(teacher, scripts[sID][0]));
        yield return StartCoroutine(Typing(teacher, scripts[sID][1]));
        yield return StartCoroutine(Typing(teacher, scripts[sID][2]));

        GameObject player = NPC(0, 14, -27);
        yield return StartCoroutine(Fade(player));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(player, Vector3.left, 5));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(player, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        yield return StartCoroutine(Move(player, Vector3.down, 8));
        yield return StartCoroutine(Move(player, Vector3.left));

        // ??? ???
        yield return StartCoroutine(Fade(black));
        Destroy(teacher);
        Destroy(player);

        // # 2
        // ??? ???
        GameObject minji = NPC(1, 9, -27);
        player = NPC(0, 8, -35);
        yield return StartCoroutine(SetCam(true, 8, -34));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(minji, Vector3.down, 8));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(minji, scripts[sID][5]));
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Typing(minji, scripts[sID][6]));
        yield return StartCoroutine(Typing(player, scripts[sID][7]));

        yield return StartCoroutine(Move(minji, Vector3.down));
        yield return StartCoroutine(Move(minji, Vector3.right, 10));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(minji);

        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story1Happy()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 56, -8));

        // ��???? ????
        GameObject player = NPC(0, 55, -16);
        GameObject friend = NPC(1, 57, -16);
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;

        StartCoroutine(Move(player, Vector2.up, 5));
        yield return StartCoroutine(Move(friend, Vector2.up, 5));

        // ��???? ???
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend, scripts[sID][2]));
        yield return StartCoroutine(Typing(friend, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // 첨벙..!
        // 하지마...!!
        // 깔깔 얘 변기 물 젖은 꼴 좀 봐
        // 이제부터 변기라고 부르자 오변기~~
        // ...
        // 무슨 냄새가 나는 것 같은데?
        // 이거 화장실 냄새 아니야? 화장실 락스 냄새인데?
        // 헐 누구야.. 민폐

        // 이게 뭐야? 얘 아직도 인형 가지고 다니나 봐
        // 완전 유치해~~!! 우리가 예쁘게 꾸며주자~
        // 머리도 예쁘게 다듬어주고~ 얼굴도 화장해 주고~
        // ...
        // 흑흑..
        // 엄마가 준 인형인데.. 엉망이 돼버렸어..

        // 자 오늘은 운동회니까 두 명씩 짝꿍을 맺어 도장을 모아 오도록~
        // 아무도 ..이랑 짝꿍 할 사람 없어?
        // 어쩔 수 없이 ..이는 선생님이랑 짝꿍 하자
        
        // 퍽!!
        // 어머~ 미안 실수로 얼굴을 맞춰버렸네!!
        // 실수인 거 알지? 피구 하다 보면 그럴 수 있잖아~
        // 퍽!!!
        // 헉 또 얼굴을 맞춰버렸네~ 얼굴 맞았으니까 무효~

        // 덜컹..!!
        // 뭐야? 나 갇혔어 내보내줘..!!
        // 안에 사람 있어요!!
        // 안웨 솨뢈 있워요~~ 깔깔깔
        // 이 시간에는 아무도 안 올걸? 좋은 밤 보내~
        // 이거 열어줘..!! 내가 잘못했어.. 가지 마..

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(friend);
        Destroy(player);

        // ???? ???????
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story1Sad()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 56, -8));

        // ��???? ????
        GameObject friend = NPC(1, 57, -11);
        yield return StartCoroutine(Fade(friend));
        // ��???? ???
        yield return StartCoroutine(Move(friend, Vector3.left, 3));
        friend.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.3f);
        friend.GetComponentInChildren<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.3f);
        friend.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        // ��???? ???
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));

        yield return StartCoroutine(Fade(black));
        // ��???? ????
        Destroy(friend);


        // ???? ????
        yield return StartCoroutine(SetCam(true, 2, 18));

        // ��???? ????
        GameObject player = NPC(0, 3, 16);
        yield return StartCoroutine(Fade(player));
        // ��???? ???
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.3f);
        player.GetComponentInChildren<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.3f);
        // ��???? ???
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));



        // Story Off
        yield return StartCoroutine(Fade(black));
        // ��???? ????
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story2()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 8, -34));


        // ��???? ????
        GameObject friend1 = NPC(1, 9, -34);
        GameObject friend2 = NPC(2, 9, -35);
        GameObject player = NPC(0, 8, -35);
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;

        friend1.SetActive(false);
        friend2.SetActive(false);
        yield return StartCoroutine(Fade(player));
        friend1.SetActive(true);
        yield return StartCoroutine(Fade(friend1));
        friend2.SetActive(true);
        yield return StartCoroutine(Fade(friend2));

        // ��???? ???
        yield return StartCoroutine(Typing(friend1, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][2]));
        yield return StartCoroutine(Typing(friend2, scripts[sID][3]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][4]));
        yield return StartCoroutine(Typing(friend2, scripts[sID][5]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][6]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][7]));
        yield return StartCoroutine(Typing(friend2, scripts[sID][8]));
        yield return StartCoroutine(Typing(player, scripts[sID][9]));


        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(friend1);
        Destroy(friend2);
        Destroy(player);

        yield return StartCoroutine(SetCam(false));

    }
    IEnumerator Story2Happy()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 27, -1));


        // ��???? ????
        GameObject player = NPC(0, 27, -1);
        GameObject friend1 = NPC(1, 27, -2);
        GameObject friend2 = NPC(2, 28, -2);
        GameObject friend3 = NPC(1, 30, 0);
        //GameObject cat = NPC(character[(int)SpriteNum.cat], 30, -1);  // ?????
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;
        friend1.GetComponentInChildren<SpriteRenderer>().flipX = true;
        friend2.GetComponentInChildren<SpriteRenderer>().flipX = true;
        friend1.SetActive(false);
        friend2.SetActive(false);
        friend3.SetActive(false);
        yield return StartCoroutine(Fade(player));
        friend3.SetActive(true);
        yield return StartCoroutine(Fade(player));
        friend1.SetActive(true);
        yield return StartCoroutine(Fade(friend1));
        friend2.SetActive(true);
        yield return StartCoroutine(Fade(friend2));

        // ��???? ???
        yield return StartCoroutine(Move(friend3, Vector3.left, 1));
        // ��???? ???
        yield return StartCoroutine(Typing(friend3, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend3, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend3, scripts[sID][2]));
        yield return StartCoroutine(Typing(friend3, scripts[sID][3]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][4]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][5]));
        yield return StartCoroutine(Typing(player, scripts[sID][6]));
        yield return StartCoroutine(Typing(player, scripts[sID][7]));


        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(friend1);
        Destroy(friend2);
        Destroy(friend3);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story2Sad()
    {

        yield return StartCoroutine(Fade(black));
        GameObject player = NPC(0, 19, -35);
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 18, -35));

        // ��???? ???
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));

        yield return StartCoroutine(Move(player, Vector3.down, 10));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story3()
    {
        // Story On
        // #1
        // ��???? ????
        yield return StartCoroutine(Fade(black));
        GameObject friend1 = NPC(1, 8, -27);
        // ???? ????
        yield return StartCoroutine(SetCam(true, 8, -27));

        // ��???? ???
        yield return StartCoroutine(Typing(friend1, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][2]));


        yield return StartCoroutine(Fade(black));
        Destroy(friend1);

        // #2
        yield return StartCoroutine(SetCam(true, 8, -34));
        GameObject friend2 = NPC(2, 16, -36);
        GameObject player = NPC(0, 8, -35);
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;

        yield return StartCoroutine(Move(friend2, Vector2.left, 7));
        yield return StartCoroutine(Move(friend2, Vector2.up, 1));

        yield return StartCoroutine(Typing(friend2, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(friend2);
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story3Happy()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(Fade(black));
        GameObject player = NPC(0, 79, -19);
        yield return StartCoroutine(SetCam(true, 76, -21));

        // ��???? ????
        GameObject friend = NPC(2, 71, -22);
        friend.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Fade(friend));
        yield return StartCoroutine(Move(friend, Vector3.right, 7));
        yield return StartCoroutine(Move(friend, Vector3.up, 1));
        // ��???? ???
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend, scripts[sID][2]));
        yield return StartCoroutine(Typing(player, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));
        yield return StartCoroutine(Typing(player, scripts[sID][5]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(friend);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story3Sad()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(Fade(black));
        GameObject player = NPC(0, 79, -19);
        yield return StartCoroutine(SetCam(true, 76, -21));

        // ��???? ????
        GameObject friend = NPC(2, 71, -22);
        friend.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Fade(friend));
        yield return StartCoroutine(Move(friend, Vector3.right, 7));
        yield return StartCoroutine(Move(friend, Vector3.up, 1));
        // ��???? ???
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(friend);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story4()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 0, 17));

        // ��???? ????
        GameObject player = NPC(0, 9, 12);
        yield return StartCoroutine(Fade(player));
        yield return StartCoroutine(Move(player, Vector2.up, 3));
        yield return StartCoroutine(Move(player, Vector2.left, 14));
        yield return StartCoroutine(Move(player, Vector2.up, 2));
        // ��???? ???
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));
        yield return StartCoroutine(Typing(player, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story4Happy()
    {
        // Story On
        // ???? ????
        // # 1
        yield return StartCoroutine(SetCam(true, -5, 17));

        // ��???? ????
        GameObject player = NPC(0, -5, 17);
        yield return StartCoroutine(Fade(player));

        // ??????
        GameObject door = GameObject.Find("Door");
        door.transform.GetChild(0).gameObject.SetActive(false);
        yield return StartCoroutine(Fade(door, false));

        yield return StartCoroutine(Move(player, Vector2.up, 1));
        yield return StartCoroutine(Fade(player, false));

        yield return StartCoroutine(Fade(black));
        Destroy(player);


        // # 2
        yield return StartCoroutine(SetCam(true, -5, 72));

        player = NPC(0, -5, 66);
        yield return StartCoroutine(Fade(player));
        yield return StartCoroutine(Move(player, Vector2.up, 6));

        GameObject mirror = NPC(4, -5, 75);
        mirror.GetComponentInChildren<SpriteRenderer>().color = Color.clear;
        // ��???? ???
        yield return StartCoroutine(Typing(mirror, scripts[sID][0]));
        yield return StartCoroutine(Typing(mirror, scripts[sID][1]));
        yield return StartCoroutine(Typing(mirror, scripts[sID][2]));
        yield return StartCoroutine(Typing(mirror, scripts[sID][3]));
        yield return StartCoroutine(Typing(mirror, scripts[sID][4]));
        yield return StartCoroutine(Typing(mirror, scripts[sID][5]));
        yield return StartCoroutine(Typing(player, scripts[sID][6]));
        yield return StartCoroutine(Typing(player, scripts[sID][7]));
        yield return StartCoroutine(Typing(player, scripts[sID][8]));
        yield return StartCoroutine(Typing(player, scripts[sID][9]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(mirror);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story4Sad()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, -5, 17));

        // ��???? ????
        GameObject player = NPC(0, -5, 17);
        yield return StartCoroutine(Fade(player));
        // ��???? ???
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story5()
    {
        yield return StartCoroutine(SetCam(true, 9, -34));

        // ��???? ????
        GameObject friend1 = NPC(1, 9, -34);
        GameObject friend2 = NPC(2, 9, -35);
        GameObject player = NPC(0, 8, -34);
        friend1.SetActive(false);
        friend2.SetActive(false);
        yield return StartCoroutine(Fade(player));
        friend1.SetActive(true);
        yield return StartCoroutine(Fade(friend1));
        friend2.SetActive(true);
        yield return StartCoroutine(Fade(friend2));

        // ��???? ???
        yield return StartCoroutine(Typing(friend1, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][2]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][3]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][4]));
        yield return StartCoroutine(Typing(player, scripts[sID][5]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(friend1);
        Destroy(friend2);
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story5Sad()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 0, 0));

        // ��???? ????
        GameObject player = NPC(0, 0, 0);
        // ��???? ???
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));
        yield return StartCoroutine(Typing(player, scripts[sID][3]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Ending1()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 0, 0));

        // ��???? ????
        GameObject player = NPC(0, 0, 0);
        GameObject mom = NPC(4, -1, 1);
        GameObject doctor = NPC(5, 1, 1);
        // ��???? ???
        yield return StartCoroutine(Typing(doctor, scripts[sID][0]));
        yield return StartCoroutine(Typing(mom, scripts[sID][1]));
        yield return StartCoroutine(Typing(mom, scripts[sID][2]));
        yield return StartCoroutine(Typing(player, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(mom);
        Destroy(doctor);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Ending2()
    {
        // Story On
        // ???? ????
        yield return StartCoroutine(SetCam(true, 8, -34));

        // ��???? ????
        GameObject player = NPC(0, 8, -34);
        GameObject friend1 = NPC(1, 9, -34);
        GameObject friend2 = NPC(2, 10, -34);
        // ��???? ???
        yield return StartCoroutine(Typing(friend1, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));
        yield return StartCoroutine(Typing(player, scripts[sID][5]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(friend1);
        Destroy(friend2);

        // ???? ???????
        yield return StartCoroutine(SetCam(false));
    }

    // ?????? ???��??? ??????? ??? ??? ?????

    IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        {
            //// time?? ?????????? ????? ??
            //float time = 0f;

            //// ????? ??????
            //Image sprite = obj.GetComponentInChildren<Image>();

            //// ?��? ??????
            //Color color = sprite.color;

            //if (IsShowing)
            //{
            //    color.a = 0f;
            //    sprite.color = color;

            //    // 1?? ?????? ????? ????? ????
            //    while (color.a < 1f)
            //    {
            //        time += 0.02f;
            //        color.a = Mathf.Lerp(0, 1, time);
            //        sprite.color = color;
            //        yield return null;
            //    }
            //}
            //else if (!IsShowing)
            //{
            //    // ???????? ?????? ?????? ????? ?? ????
            //    while (color.a > 0f)
            //    {
            //        time += Time.deltaTime;
            //        color.a = Mathf.Lerp(1, 0, time);
            //        sprite.color = color;
            //        yield return null;
            //    }
            //}
        }

        Animation anim = obj.GetComponent<Animation>();

        anim.enabled = false;
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

    }

    public void Talk(GameObject talker, string chat, float tDestroy = 2)
    {
        GameObject chatBox = Instantiate(ChatPrefab, talker.transform);
        chatBox.transform.SetParent(talker.transform);
        TextMeshProUGUI chatUI = chatBox.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        chatUI.text = chat;
        Destroy(chatBox, tDestroy);
    }
    IEnumerator Typing(GameObject talker, string chat, float tDestroy = 1)
    {
        GameObject chatBox = Instantiate(ChatPrefab, talker.transform);
        chatBox.transform.SetParent(talker.transform);
        TextMeshProUGUI chatUI = chatBox.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        for(int i = 0; i < chat.Length; i++)
        {
            chatUI.text += chat[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(tDestroy);
        Destroy(chatBox);
        yield return null;
    }

    IEnumerator Move(GameObject npc, Vector3 head, int length = 1, float speed = 0.05f)
    {
        npc.GetComponent<Animator>().SetBool("Walk", true);
        for (int i = 0; i < length; i++)
        {
            Vector3 curPos = npc.transform.localPosition;
            Vector3 headPos = curPos + head;
            // ��???? ??? ???
            while (true)
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
        npc.GetComponent<Animator>().SetBool("Walk", false);
    }

    // npc ��???? ????
    public GameObject NPC(int npcidx, float _x, float _y)
    {
        GameObject npc = Instantiate(characterprefeb[npcidx]);
        npc.transform.position = new Vector2(_x, _y);
        npc.transform.SetParent(storyObject);

        return npc;
    }

    IEnumerator Jump(GameObject npc, float jumppower, float jumpspeed)
    {
        float goal = npc.transform.position.y + jumppower;
        float y = Mathf.Lerp(npc.transform.position.y, goal, jumpspeed);
        while(Mathf.Abs( goal - y )> 0.1f)
        {
            y = Mathf.Lerp(npc.transform.position.y, goal, jumpspeed);
            npc.transform.position = new Vector2(npc.transform.position.x, y);
            yield return null;
        }
        npc.transform.position = new Vector2(npc.transform.position.x, goal);

        goal = npc.transform.position.y - jumppower;
        y = Mathf.Lerp(npc.transform.position.y, goal, jumpspeed);
        while (Mathf.Abs( goal - y )> 0.1f)
        {
            y = Mathf.Lerp(npc.transform.position.y, goal, jumpspeed);
            npc.transform.position = new Vector2(npc.transform.position.x, y);
            yield return null;
        }
        npc.transform.position = new Vector2(npc.transform.position.x, goal);
    }
}
