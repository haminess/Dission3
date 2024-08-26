using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] button;
    public int selectedNum = 0;
    public string[][] info;

    public TextMeshProUGUI nameUI;
    public TextMeshProUGUI infoUI;

    void Start()
    {
        button = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            button[i] = transform.GetChild(i).gameObject;
            button[i].GetComponent<Button>().onClick.AddListener(() => ShowInfo());
        }
        Select(0);

        info = new string[5][];
        info[0] = new string[2] { "������", "���� ���� �����ϰ� ����ϰ� �����ϴ�\n���ο� �Ϳ� �����ϴ� ���� �����Ѵ�" };
        info[1] = new string[2] { "�����", "�׻� ������ �Ⱥη��� ���ݿ� å�Ӱ��� ���ϴ�\nS�뿡 ���� ���� ��ǥ�� �Ѵ�" };
        info[2] = new string[2] { "������", "������ �����ϰ� ���� ���ϴ� ADHD�� ��Ʃ���� Ȱ���Ѵ�\n�̽��� ��Ʈ�� ���� ��Ʃ�� �Կ��ϴ� ���� ���̴�" };
        info[3] = new string[2] { "������", "�ÿ��ÿ��ϰ� ���� ������ ���� �������\n�ø��� �����ϴ� ���� ���̴�" };
        info[4] = new string[2] { "���ѳ�", "Ÿ�ο��� ������ ����\n�����鿡�Դ� ���� �� ���� ģ���� ����" };
    }

    public void Select(int num)
    {
        // ��ü ����
        selectedNum = num;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform character = button[i].transform;
            if(i == num)
            {
                character.GetChild(0).gameObject.SetActive(false);
                character.GetChild(1).gameObject.SetActive(true);
                character.GetChild(1).GetComponent<Animator>().SetBool("Walk", true);
            }
            else
            {
                character.GetChild(0).gameObject.SetActive(true);
                character.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void ShowInfo()
    {
        nameUI.text = info[selectedNum][0];
        infoUI.text = info[selectedNum][1];

    }
}
