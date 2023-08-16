using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public int storyID = 0;

    public bool showButton;

    // �� ������Ʈ
    public GameObject player;
    public GameObject playerCam;
    public GameObject storyCamera;
    public GameObject gameCanvas;
    public GameObject playerPrefab;
    public GameObject npcPrefab;
    public GameObject stage1;     // �Ϲ�
    public GameObject stage2;     // ����
    public GameObject stage3;     // ����
    public GameObject stage4;     // ����

    // ĳ���� �̹��� ��������Ʈ
    public Sprite baby;
    public Sprite student;

    public GameObject ChatPrefab;
    public Image black;

    // ��ũ��Ʈ ����
    public float chatSpeed = 2;

    // ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        storyCamera.SetActive(false);
        gameCanvas.SetActive(true);

        // �������� ��
        // �� ������Ʈ Ȱ��ȭ
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
        // �÷��̾� ������ Off
        player.GetComponent<Player>().enabled = false;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // ���� UI Off
        gameCanvas.SetActive(false);

        // ���丮 ����
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



        // �÷��̾� ������ On
        player.GetComponent<Player>().enabled = true;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;

        // ���� UI On
        gameCanvas.SetActive(true);
    }

    IEnumerator Story1()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));
        StartCoroutine(Fade(black, false));

        // ī�޶� ����
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
        Talk(teacher, "�츮 �ݿ� ���л��� �Ծ��");
        yield return new WaitForSeconds(chatSpeed);
        Talk(teacher, "��� ���л����� ����~");
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
        Talk(players, "�ȳ�~ ���� 00�̾�");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�ٵ� �� ��Ź��!");
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

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));
        Destroy(teacher);
        Destroy(players);

        // # 2
        storyCamera.transform.position = new Vector3(0, -7, -10);
        GameObject minji = NPC(student, 0, 0);
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
        Talk(minji, "�ȳ�! ���� �� �� ȸ�� ������");
        yield return new WaitForSeconds(chatSpeed);
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;
        Talk(minji, "���ݺ��� �ʿ��� �б��� �Ұ������ٰ�!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�׷�!");
        yield return new WaitForSeconds(chatSpeed);

        // Story Off
        yield return StartCoroutine(Fade(black));
        Destroy(players);
        Destroy(minji);

        // ī�޶� �ǵ�����
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story1_1()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // ȭ�� ����
        GameObject players = NPC(student, 0, 17);
        GameObject minji = NPC(student, 4, 17);
        players.GetComponentInChildren<SpriteRenderer>().flipX = true;

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(minji, "��ȭ�ϴ� ���� �츮 �ϳ� �������� ���� �� ����!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(minji, "�����ε� ģ�ϰ� ������~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "��! �б��� �Ұ����� �༭ ������~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "���� �� ��Ź��!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story1_2()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(2, 18, -10);

        // ȭ�� ����
        GameObject minji = NPC(student, 4, 17);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(minji, "��? 00�̰� ���� �� ����?");
        yield return new WaitForSeconds(chatSpeed);


        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));
        storyCamera.transform.position = new Vector3(0, 0, -10);
        GameObject players = NPC(student, 0, 0);
        yield return StartCoroutine(Fade(black, false));

        Talk(players, "���Ⱑ �����?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "��� �Ѵ� �Ĵ� ���̿� ������ �������..");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(minji);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story2()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(11, -68, -10);

        // ���丮 ����
        GameObject hanni = NPC(student, 5, -70);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        GameObject players = NPC(student, 7, -72);
        yield return StartCoroutine(Fade(players));
        yield return StartCoroutine(Move(players, Vector2.up));
        yield return StartCoroutine(Move(players, Vector2.up));

        Talk(hanni, "�� �̷��� �ʰ� �Ծ�!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "���� �����ΰ� ����?!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�� �ʾ �̾�, ���� ����!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "���� �ʹ� ��հڴ� ����");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story2_1()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // ���丮 ����
        GameObject hanni = NPC(student, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(hanni, "�츮 ���� ���� ������!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(hanni, "�ٵ� �����߾�~");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�ٵ� ���� ���Ҿ�! ���Ϻ���!!");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story2_2()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);

        // ���丮 ����
        GameObject hanni = NPC(student, 39, -55);
        hanni.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 41, -55);
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(hanni, "���� �ǻ��� �� ã�Ƽ� �ᱹ ���뿡 �� �ö󰡴ٴ�..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "�׷���.. �ʹ� �ƽ���...");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(hanni);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story3()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject players = NPC(student, 46, -2);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        GameObject diniel = NPC(student, 34, -2);
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

        Talk(diniel, "ū�ϳ���..!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�츮 ��Ƽ ����� ��Ҵ�..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�б޺� �������..!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�̵����� �ϸ鼭 ��� ����߸� �� ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "Ȥ�� �������� ���� ���� ã���ٷ�..?");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    IEnumerator Story3_1()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(7, -18, -10);
        GameObject diniel = NPC(student, 8, -20);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 10, -20);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(diniel, "�� �ƴϿ����� ���� ū�ϳ� ���߾�..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�� ���� �ְ��� ģ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "���� ������!!");
        yield return new WaitForSeconds(chatSpeed);
        Talk(players, "ģ����� �翬�� ���;���~");

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story3_2()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(43, 0, -10);
        GameObject diniel = NPC(student, 42, -2);
        diniel.GetComponentInChildren<SpriteRenderer>().flipX = true;
        GameObject players = NPC(student, 44, -2);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);


        Talk(players, "�����.. �ƹ��� ã�Ƶ� �Ⱥ���");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�׷���.. ��¿ �� ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�� ���� ä���� �� �� ����..");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "�� ���� ���� ��� ������..?");
        yield return new WaitForSeconds(chatSpeed);
        Talk(diniel, "��_��");
        yield return new WaitForSeconds(chatSpeed);

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        Destroy(diniel);
        Destroy(players);
        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story4()
    {
        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(40, -53, -10);
        GameObject players = NPC(student, 38, -55);
        
        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);

        Talk(players, "");

        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }
    IEnumerator Story()
    {

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return StartCoroutine(Fade(black, false));

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        // ȭ�� ��ȯ
        yield return StartCoroutine(Fade(black));

        storyCamera.SetActive(false);
        playerCam.SetActive(true);

        yield return StartCoroutine(Fade(black, false));
    }

    // ������ ���¿��� ��Ÿ���� �ϴ� ȿ�� �޼���
    IEnumerator Fade(GameObject obj, bool IsShowing = true)
    {
        // time�� ���������� �ö󰡴� ��
        float time = 0f;

        // �̹��� �޾ƿ���
        SpriteRenderer sprite = obj.GetComponentInChildren<SpriteRenderer>();

        // �÷� �޾ƿ���
        Color color = sprite.color;

        if(IsShowing)
        {
            color.a = 0f;
            sprite.color = color;

            // 1�� ������ ��µǴ� ������ ����
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
            // �������� ������ ���ҵǴ� ������ �� ����
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

            // 1�� ������ ��µǴ� ������ ����
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
            // �������� ������ ���ҵǴ� ������ �� ����
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
        // ĳ���� ��ǥ �̵�
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


    public void Skip()
    {
        storyCamera.SetActive(false);
        playerCam.SetActive(true);
        player.SetActive(true);
    }

    // npc ĳ���� ����
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
        // ī�޶� ����
        playerCam.SetActive(false);
        storyCamera.SetActive(true);
        storyCamera.transform.position = new Vector3(0, 0, -10);

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(1);

        storyCamera.SetActive(false);
        playerCam.SetActive(true);
    }

}