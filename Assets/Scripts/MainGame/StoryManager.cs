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

    // �� ������Ʈ
    public GameObject player;
    public GameObject playerCam;
    public GameObject storyCamera;
    public GameObject gameCanvas;
    public GameObject playerPrefab;
    public GameObject npcPrefab;
    public GameObject[] stageObject;     // �������� ���� ������Ʈ
    public Transform storyObject;

    // ĳ���� �̹��� ��������Ʈ
    public Sprite[] character;
    public Sprite baby;
    public Sprite student;
    public Sprite friend1;
    public GameObject black;

    public GameObject ChatPrefab;

    // ��ũ��Ʈ ����
    public float chatSpeed = 2;
    Dictionary<int, string[]> scripts = new Dictionary<int, string[]>();
    int[][] npcNum;

    // ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        storyCamera.SetActive(false);
        gameCanvas.SetActive(false);
        CreateScripts();

        // �������� ��
        // �� ������Ʈ Ȱ��ȭ
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
        // 1��������
        scripts.Add(0, new string[] { "�ٵ� ������ħ~",                      // ������
                                      "������ �츮 �ݿ� ���л��� �Ծ��~",
                                      "���л��� �ڱ�Ұ� �غ���?",
                                      "�ȳ� ���� ��������� ��!",             // ���ΰ�
                                      "������ �� ��Ź��, ģ�ϰ� ������!",
                                      // �ڸ��� �ɴ´�.
                                      "�ȳ� ������, ������ �ݰ���!!",          // ģ��1
                                      "���� �б� �Ұ������״ϱ� ���� ����?",
                                      "����!"});                             // ���ΰ�
        scripts.Add(1, new string[] { "�̰ɷ� �б� �Ұ��� ���̾�!",            // ģ��1
                                      "������ ����ְ� �б���Ȱ �غ���",
                                      "�� �´�! �� ������ ������ ���� ��!!",
                                      "�ͽ��� �鷶�ٴ� �ҹ��� �����ϱ�~ ����",
                                      "(��.. ���� �����ϰ� �����..)"});       // ���ΰ�
        scripts.Add(2, new string[] { "��? ������ ���� �� ����?",
                                      "�б� ������ �����ؼ� ���� �Ҿ���Ⱦ�..",
                                      "������ ��𿴴���?"});
        npcNum = new int[16][];
        npcNum[0] = new int[] { 1, 1, 1, 0, 0, 2, 2, 0 };
        npcNum[1] = new int[] { 2, 2, 2, 2, 0 };
        npcNum[2] = new int[] { 0, 0 };

        // 2��������
        scripts.Add(3, new string[] { "�� �� �ҹ� �����?",                    // ģ��1
                                      "�츮 �б� �޽Ľǿ��� ���Ḷ��..",
                                      "�̻��� �Ҹ��� ����..!!",
                                      "���� �����ȵ�! �����̰���~",            // ģ��2
                                      "��¥��ϱ�!!",                         // ģ��1
                                      "..�׷� ���� �ѹ� Ȯ���غ���?",          // ģ��2
                                      "�׷���? ���� ���..",                 // ģ��1
                                      "�츮 �����̼� ����׽�Ʈ �غ���?!?!",    // ģ��1
                                      "����! �׷� ���ῡ �޽Ľǿ��� ������!!",  // ģ��2
                                      "(��.. ������ �� ������;;)"});           // ���ΰ�
        scripts.Add(4, new string[] { "��� ���� ���� �޽Ľǿ��� ����̸� Ű��� �־���..", // ģ��3
                                      "�� ����� ��� ���� Ÿ��",
                                      "���� ��������� ������ �� ����..",
                                      "�̷��� ���� Ŀ�� �� �����µ�.. �̾�!",
                                      "�׷� �� ������.. �׷� �ͽ��� ���°ǰ�?", // ģ��1
                                      "��? �������� ���µ�?",
                                      // ����̰� ������ ����� ���ΰ����� �ǳ��ش�.
                                      "(���? �� ���� �ƴ� �����ε�..)",         // ���ΰ�
                                      "(��� �ô���..)"});
        scripts.Add(5, new string[] { "���� �ʹ� ������..",
                                      "�׳� �����ϰ� �����߰ھ�.."});

        npcNum[3] = new int[] { 1, 1, 1, 2, 1, 2, 1, 1, 2, 0 };
        npcNum[4] = new int[] { 3, 3, 3, 3, 1, 1, 0, 0 };
        npcNum[5] = new int[] { 0, 0 };

        // 3��������
        scripts.Add(6, new string[] { "������ ���� ��ٸ��� ��ٸ���..",        // ģ��1
                                      "�ٷ� ü�� ��ȸ��!!",
                                      "�ٵ� �ּ��� ���ؼ� ����ְ� �غ��ڤ���",
                                      "������ ���� �Ǳ� ���� ����! ��ġ�� ����!!", // ģ��2
                                      "�� ����!!"});                           // ���ΰ�
        scripts.Add(7, new string[] { // ��� ���� ��Ȳ�� �����Ű�� ����� ª�� ��������.
                                      // (������ ���ϴ� ���� ���� ������ ����..)
                                      // ���ǽǿ��� ���ΰ��� �����.
                                      "������??", // ģ��2
                                      "�� �Ǳ� ���� �ϴٰ� ���ڱ� ��������!!",
                                      "��ü �� �׷��ž�?",
                                      "���� �� �𸣰ھ�..",                       // ���ΰ�
                                      "���ڱ� �Ӹ��� ���ļ� ������ ���� �� ����..",
                                      "(���� �������� �ߴ� �� ������..)"});
        scripts.Add(8, new string[] { // ���ǽǿ��� ���ΰ��� �����.
                                      "�� �Ǳ� ���� �ϴٰ� ���ڱ� ��������!!",      // ģ��2
                                      "��ü �� �׷��ž�?",
                                      "���ڱ� �Ӹ��� ���ļ� ������ ���� �� ����.."}); // ���ΰ�

        npcNum[6] = new int[] { 1, 1, 1, 2, 0 };
        npcNum[7] = new int[] { 2, 2, 2, 0, 0, 0 };
        npcNum[8] = new int[] { 2, 2, 0 };

        // 4��������
        scripts.Add(9, new string[] { "���..? �� ���� �� ����ִ°���?",           // ���ΰ�
                                      "�ȿ��� ���� �Ҹ��� �鸮�� �� ���⵵ �ϰ�..",
                                      "������ ������..",
                                      "�б� �ȿ� �� ���� ������ ���� ������?",
                                      "�ѹ� ã�ƺ���!"});
        scripts.Add(10, new string[] { // ���� ���� ������ �ſ�(��������)���� �Ҹ��� �����ִ�.
                                      "���� �� �̻� ���� �� �� �ִ°� ������.",
                                      "���� ȯ�ں��� �����Ա⿡ �޷Ƚ��ϴ�..",
                                      "������.. ������ �ϰ� �־�..",
                                      "�ٸ� ������� �ƹ��� �����϶�� �ص�",
                                      "������ ���� ���� ���� ���ϰ� ��� ��ٸ�����..",
                                      "������ �����..",
                                      "������",
                                      "�¾�.. ����� �� ����� ����� �� �� �� ���迴��..",
                                      "������ �ʹ� ���ο��� ���� ��� �ʹ� ���� �ٰ� �־�����",
                                      "�� �̰��� �ͼ� ������ �ູ�ѵ�.. ��� �ؾ� �ұ�"});
        scripts.Add(11, new string[] { "�� ���� ������ ��ã�ڳ�..",
                                      "�׳� ����� â����� ��!"});
        npcNum[9] = new int[] { 0, 0, 0, 0, 0 };
        npcNum[10] = new int[] { 1, 1, 2, 2, 2, 2, 0, 0, 0, 0 };
        npcNum[11] = new int[] { 0, 0 };

        // 5��������
        scripts.Add(12, new string[] { // ģ���� ���ΰ��� å�� ��ó���� ����ִ�.
                                      "������! ���� ������ � ���ٱ�??",
                                      "������ �?",
                                      "��! �׳� å�� �켭 ���� �������� ��� �˾ƺ��°ž�!!",
                                      "���� ������ ���..",
                                      "�������� ���ϴ� ������ ���� ��µ�?",
                                      "(�� ������ ���ϴ� ��..?)"});
        scripts.Add(13, new string[] { "ȯ�ں� ������ �弼��!?",                 
                                      "������!! ���� �������..!",
                                      "������ ����.. �ʹ� ����...",
                                      "(���Ⱑ �����̱���.. ���� ���ƿԾ�..)",
                                      "(���..? ���� �� ���� ��� �� ������.. ��������?)"});
        scripts.Add(14, new string[] { "������! ���� �޽� ���Ƕ��̽� ���´�!!",    // ���ΰ�
                                      "�ʰ� ���� �����ϴ� ���ݾ� ����",
                                      "�� ���� �� ġ�ڸ��� �پ��!!!",
                                      "�� ����!!",
                                      "(���� �̰��� ������ �� �ູ�� �� ����..)",
                                      "(���� ���Ⱑ �� �����̾�!! �ٸ��� �ƹ��͵� �ʿ����..)"});
        scripts.Add(15, new string[] { "���� ���� �ٸ� ���� �Ϸ翴��!", // ���ΰ�
                                      "��� �ǹ̰� ���� �𸣰ڳ�..",
                                      "���� �ϰų����ų��ΰ�!",
                                      "���� ���� �� ������.. ���ſ�̰���?"});
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
        // �÷��̾� ������ Off
        player.GetComponent<Player>().enabled = false;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // ���� UI Off
        gameCanvas.SetActive(false);

        // ���丮 ����
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


        // �÷��̾� ������ On
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;

        // ���� UI On
        gameCanvas.SetActive(true);
    }

    // ���丮 ��ŵ
    public void Skip()
    {
        StopAllCoroutines();
        StartCoroutine(SkipCo());
    }

    public IEnumerator SkipCo()
    {
        yield return StartCoroutine(Fade(black));

        storyCamera.SetActive(false);
        playerCam.SetActive(true);
        player.SetActive(true);
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        yield return StartCoroutine(Fade(black, false));

        for(int i = 0; i < storyObject.childCount; i++)
        {
            Destroy(storyObject.GetChild(0));
        }

        if (!MainGame.instance.stageMode)
            yield break;

        if (!MainGame.instance.isEnd)
        {
            MainGame.instance.GameStart();
        }
        else
        {
            MainGame.instance.GameEnd();
        }
    }

    IEnumerator Story()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 0, 0));


        // ĳ���� ����
        GameObject teacher = NPC(character[(int)SpriteNum.teacher], 6, 0);
        // ĳ���� ����
        Destroy(teacher);
        // ĳ���� �̵�
        yield return StartCoroutine(Move(teacher, Vector3.left, 6));
        // ĳ���� ���
        yield return StartCoroutine(Typing(teacher, scripts[sID][0]));

        // Story Off
        yield return StartCoroutine(Fade(black));

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));

    }

    IEnumerator SetCam(bool _isOn, float _x = 0, float _y = 0)
    {
        if (_isOn)
        {
            yield return StartCoroutine(Fade(black));

            // ī�޶� ����
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
        // ȭ�� ��ȯ
        yield return StartCoroutine(SetCam(true, 8, -27));

        // # 1
        GameObject teacher = NPC(character[(int)SpriteNum.teacher], 14, -27);
        yield return StartCoroutine(Fade(teacher));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(teacher, Vector3.left, 6));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(teacher, scripts[sID][0]));
        yield return StartCoroutine(Typing(teacher, scripts[sID][1]));
        yield return StartCoroutine(Typing(teacher, scripts[sID][2]));

        GameObject player = NPC(character[(int)SpriteNum.student], 14, -27);
        yield return StartCoroutine(Fade(player));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(player, Vector3.left, 5));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(player, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        yield return StartCoroutine(Move(player, Vector3.down, 8));
        yield return StartCoroutine(Move(player, Vector3.left));

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));
        Destroy(teacher);
        Destroy(player);

        // # 2
        // ȭ�� ��ȯ
        yield return StartCoroutine(SetCam(true, 8, -34));
        GameObject minji = NPC(character[(int)SpriteNum.friend1], 9, -27);
        player = NPC(character[(int)SpriteNum.student], 8, -35);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Move(minji, Vector3.down, 8));

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Typing(minji, scripts[sID][5]));
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Typing(minji, scripts[sID][6]));
        yield return StartCoroutine(Typing(player, scripts[sID][7]));

        yield return StartCoroutine(Move(player, Vector3.down));
        yield return StartCoroutine(Move(player, Vector3.right, 10));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(minji);

        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story1Happy()
    {

        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 56, -8));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 55, -11);
        GameObject friend = NPC(character[(int)SpriteNum.friend1], 57, -11);
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;


        // ĳ���� ���
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend, scripts[sID][2]));
        yield return StartCoroutine(Typing(friend, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // ÷��..!
        // ������...!!
        // ��� �� ���� �� ���� �� �� ��
        // �������� ������ �θ��� ������~~
        // ...
        // ���� ������ ���� �� ������?
        // �̰� ȭ��� ���� �ƴϾ�? ȭ��� ���� �����ε�?
        // �� ������.. ����

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(friend);
        Destroy(player);

        // ī�޶� �ǵ�����
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));

        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story1Sad()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 56, -8));

        // ĳ���� ����
        GameObject friend = NPC(character[(int)SpriteNum.friend1], 57, -11);
        yield return StartCoroutine(Fade(friend));
        // ĳ���� �̵�
        yield return StartCoroutine(Move(friend, Vector3.left, 3));
        // ĳ���� ���
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));

        yield return StartCoroutine(Fade(black));
        // ĳ���� ����
        Destroy(friend);


        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 2, 18));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 3, 18);
        yield return StartCoroutine(Fade(player));
        // ĳ���� �̵�
        yield return StartCoroutine(Move(player, Vector3.left, 2));
        // ĳ���� ���
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));



        // Story Off
        yield return StartCoroutine(Fade(black));
        // ĳ���� ����
        Destroy(player);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story2()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 8, -34));


        // ĳ���� ����
        GameObject friend1 = NPC(character[(int)SpriteNum.friend1], 8, -34);
        GameObject friend2 = NPC(character[(int)SpriteNum.friend2], 8, -35);
        GameObject player = NPC(character[(int)SpriteNum.student], 7, -35);
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;

        friend1.SetActive(false);
        friend2.SetActive(false);
        yield return StartCoroutine(Fade(player));
        friend1.SetActive(true);
        yield return StartCoroutine(Fade(friend1));
        friend2.SetActive(true);
        yield return StartCoroutine(Fade(friend2));

        // ĳ���� ���
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
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 27, -4));


        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 27, -1);
        GameObject friend1 = NPC(character[(int)SpriteNum.friend1], 27, -2);
        GameObject friend2 = NPC(character[(int)SpriteNum.friend2], 28, -2);
        GameObject friend3 = NPC(character[(int)SpriteNum.friend1], 30, -1);
        friend3.GetComponentInChildren<SpriteRenderer>().flipX = true;
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

        // ĳ���� �̵�
        yield return StartCoroutine(Move(friend3, Vector3.left, 1));
        // ĳ���� ���
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

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story2Sad()
    {

        yield return StartCoroutine(Fade(black));
        GameObject player = NPC(character[(int)SpriteNum.student], 18, -39);
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 18, -39));

        // ĳ���� ���
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));

        yield return StartCoroutine(Move(player, Vector3.left, 10));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story3()
    {
        // Story On
        // #1
        // ĳ���� ����
        yield return StartCoroutine(Fade(black));
        GameObject friend1 = NPC(character[(int)SpriteNum.friend1], 8, -27);
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 8, -27));

        // ĳ���� ���
        yield return StartCoroutine(Typing(friend1, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][1]));
        yield return StartCoroutine(Typing(friend1, scripts[sID][2]));


        yield return StartCoroutine(Fade(black));
        Destroy(friend1);

        // #2
        yield return StartCoroutine(SetCam(true, 8, -34));
        GameObject friend2 = NPC(character[(int)SpriteNum.friend2], 15, -36);
        GameObject player = NPC(character[(int)SpriteNum.student], 7, -35);
        player.GetComponentInChildren<SpriteRenderer>().flipX = true;

        yield return StartCoroutine(Move(friend2, Vector2.left, 7));
        yield return StartCoroutine(Move(friend2, Vector2.up, 1));

        yield return StartCoroutine(Typing(friend2, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(friend2);
        Destroy(player);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story3Happy()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(Fade(black));
        GameObject player = NPC(character[(int)SpriteNum.student], 79, -20);
        yield return StartCoroutine(SetCam(true, 76, -21));

        // ĳ���� ����
        GameObject friend = NPC(character[(int)SpriteNum.friend2], 71, -22);
        yield return StartCoroutine(Fade(friend));
        yield return StartCoroutine(Move(friend, Vector3.right, 7));
        yield return StartCoroutine(Move(friend, Vector3.up, 1));
        // ĳ���� ���
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

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story3Sad()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(Fade(black));
        GameObject player = NPC(character[(int)SpriteNum.student], 79, -20);
        yield return StartCoroutine(SetCam(true, 76, -21));

        // ĳ���� ����
        GameObject friend = NPC(character[(int)SpriteNum.friend2], 71, -22);
        yield return StartCoroutine(Fade(friend));
        yield return StartCoroutine(Move(friend, Vector3.right, 7));
        yield return StartCoroutine(Move(friend, Vector3.up, 1));
        // ĳ���� ���
        yield return StartCoroutine(Typing(friend, scripts[sID][0]));
        yield return StartCoroutine(Typing(friend, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);
        Destroy(friend);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story4()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 2, 18));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 9, 12);
        yield return StartCoroutine(Fade(player));
        yield return StartCoroutine(Move(player, Vector2.up, 3));
        yield return StartCoroutine(Move(player, Vector2.left, 14));
        yield return StartCoroutine(Move(player, Vector2.up, 2));
        // ĳ���� ���
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));
        yield return StartCoroutine(Typing(player, scripts[sID][3]));
        yield return StartCoroutine(Typing(player, scripts[sID][4]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story4Happy()
    {
        // Story On
        // ī�޶� ����
        // # 1
        yield return StartCoroutine(SetCam(true, 9, 12));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 9, 12);
        yield return StartCoroutine(Fade(player));

        // ������
        GameObject door = GameObject.Find("Door");
        door.transform.GetChild(0).gameObject.SetActive(false);
        yield return StartCoroutine(Fade(door, false));

        yield return StartCoroutine(Move(player, Vector2.up, 1));
        yield return StartCoroutine(Fade(player, false));

        yield return StartCoroutine(Fade(black));
        Destroy(player);


        // # 2
        yield return StartCoroutine(SetCam(true, -5, 69));

        player = NPC(character[(int)SpriteNum.student], -5, 63);
        yield return StartCoroutine(Fade(player));
        yield return StartCoroutine(Move(player, Vector2.up, 9));

        GameObject mirror = NPC(character[(int)SpriteNum.mom], -5, 75);
        mirror.GetComponentInChildren<SpriteRenderer>().color = Color.clear;
        // ĳ���� ���
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

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story4Sad()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 9, 12));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 9, 12);
        yield return StartCoroutine(Fade(player));
        // ĳ���� ���
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story5()
    {
        yield return StartCoroutine(SetCam(true, 11, -68));


        // ĳ���� ����
        GameObject friend1 = NPC(character[(int)SpriteNum.friend1], 10, -67);
        GameObject friend2 = NPC(character[(int)SpriteNum.friend2], 12, -67);
        GameObject player = NPC(character[(int)SpriteNum.student], 11, -68);
        friend1.SetActive(false);
        friend2.SetActive(false);
        yield return StartCoroutine(Fade(player));
        friend1.SetActive(true);
        yield return StartCoroutine(Fade(friend1));
        friend2.SetActive(true);
        yield return StartCoroutine(Fade(friend2));

        // ĳ���� ���
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

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Story5Sad()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 0, 0));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 0, 0);
        // ĳ���� ���
        yield return StartCoroutine(Typing(player, scripts[sID][0]));
        yield return StartCoroutine(Typing(player, scripts[sID][1]));
        yield return StartCoroutine(Typing(player, scripts[sID][2]));
        yield return StartCoroutine(Typing(player, scripts[sID][3]));

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(player);

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Ending1()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 0, 0));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 0, 0);
        GameObject mom = NPC(character[(int)SpriteNum.mom], -1, 1);
        GameObject doctor = NPC(character[(int)SpriteNum.doctor], 1, 1);
        // ĳ���� ���
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

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }
    IEnumerator Ending2()
    {
        // Story On
        // ī�޶� ����
        yield return StartCoroutine(SetCam(true, 0, 0));

        // ĳ���� ����
        GameObject player = NPC(character[(int)SpriteNum.student], 0, 0);
        GameObject friend1 = NPC(character[(int)SpriteNum.friend1], 1, 0);
        GameObject friend2 = NPC(character[(int)SpriteNum.friend2], 2, 0);
        // ĳ���� ���
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

        // ī�޶� �ǵ�����
        yield return StartCoroutine(SetCam(false));
    }

    // ������ ���¿��� ��Ÿ���� �ϴ� ȿ�� �޼���

    IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        {
            //// time�� ���������� �ö󰡴� ��
            //float time = 0f;

            //// �̹��� �޾ƿ���
            //Image sprite = obj.GetComponentInChildren<Image>();

            //// �÷� �޾ƿ���
            //Color color = sprite.color;

            //if (IsShowing)
            //{
            //    color.a = 0f;
            //    sprite.color = color;

            //    // 1�� ������ ��µǴ� ���� ����
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
            //    // �������� ������ ���ҵǴ� ���� �� ����
            //    while (color.a > 0f)
            //    {
            //        time += Time.deltaTime;
            //        color.a = Mathf.Lerp(1, 0, time);
            //        sprite.color = color;
            //        yield return null;
            //    }
            //}
        }

        Animation anim = obj.GetComponentInChildren<Animation>();

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
        for (int i = 0; i < length; i++)
        {
            Vector3 curPos = npc.transform.localPosition;
            Vector3 headPos = curPos + head;
            // ĳ���� ��ǥ �̵�
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
    }

    // npc ĳ���� ����
    public GameObject NPC(Sprite _sprite, float _x, float _y)
    {
        GameObject npc = Instantiate(npcPrefab);
        npc.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
        npc.transform.position = new Vector2(_x, _y);
        npc.transform.SetParent(storyObject);

        return npc;
    }
}
