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
        info[0] = new string[2] { "오선지", "남을 먼저 생각하고 배려하고 착실하다\n새로운 것에 도전하는 것을 좋아한다" };
        info[1] = new string[2] { "김민지", "항상 당차고 똑부러진 성격에 책임감이 강하다\nS대에 들어가는 것을 목표로 한다" };
        info[2] = new string[2] { "정지원", "가만히 차분하게 있지 못하는 ADHD에 유튜버로 활동한다\n미스터 비스트와 같이 유튜브 촬영하는 것이 꿈이다" };
        info[3] = new string[2] { "한주이", "시원시원하고 쿨한 성격을 가진 운동선수로\n올림픽 출전하는 것이 꿈이다" };
        info[4] = new string[2] { "강한남", "타인에게 관심이 없음\n동물들에게는 그저 한 없이 친절한 남자" };
    }

    public void Select(int num)
    {
        // 전체 해제
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
