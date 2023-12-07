using System.IO;
using UnityEngine;
using TMPro;
using System;

public class Ranking : MonoBehaviour
{
    private MainGameData maingamedata => DataManager.Instance.maingamedata;
    public TextMeshProUGUI title;
    public TextMeshProUGUI names;
    public TextMeshProUGUI scores;
    public StageManager stagemanager;

    public int[] first_stage;
    public int[] second_stage;
    public int[] third_stage;
    public int[] fourth_stage;
    public int[] fifth_stage;

    public string[] first_stage_n;
    public string[] second_stage_n;
    public string[] third_stage_n;
    public string[] fourth_stage_n;
    public string[] fifth_stage_n;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRank()
    {
        string temp = string.Empty;
        string temp_n = string.Empty;
        DataManager.Instance.LoadMainGameData();
        reload();
        switch(stagemanager.curStage)
        {
            case 1:
                title.text = "1 Stage Rank";
                for (int i = 0; i < maingamedata.first_stage.Length; i++)
                {
                    temp = temp + (maingamedata.first_stage[i].ToString() + "\n");
                }
                scores.text = temp;

                for (int i = 0; i < maingamedata.first_stage_n.Length; i++)
                {
                    temp_n = temp_n + i + "등 " + (maingamedata.first_stage_n[i] + "\n");
                }
                names.text = temp_n;
                break;
            case 2:
                title.text = "2 Stage Rank";
                for (int i = 0; i < maingamedata.second_stage.Length; i++)
                {
                    temp = temp + (maingamedata.second_stage[i].ToString() + "\n");
                }
                scores.text = temp;

                for (int i = 0; i < maingamedata.second_stage_n.Length; i++)
                {
                    temp_n = temp_n + i + "등 " + (maingamedata.second_stage_n[i] + "\n");
                }
                names.text = temp_n;
                break;
            case 3:
                title.text = "3 Stage Rank";
                for (int i = 0; i < maingamedata.third_stage.Length; i++)
                {
                    temp = temp + (maingamedata.third_stage[i].ToString() + "\n");
                }
                scores.text = temp;

                for (int i = 0; i < maingamedata.third_stage_n.Length; i++)
                {
                    temp_n = temp_n + i + "등 " + (maingamedata.third_stage_n[i] + "\n");
                }
                names.text = temp_n;
                break;
            case 4:
                title.text = "4 Stage Rank";
                for (int i = 0; i < maingamedata.fourth_stage.Length; i++)
                {
                    temp = temp + (maingamedata.fourth_stage[i].ToString() + "\n");
                }
                scores.text = temp;

                for (int i = 0; i < maingamedata.fourth_stage_n.Length; i++)
                {
                    temp_n = temp_n + i + "등 " + (maingamedata.fourth_stage_n[i] + "\n");
                }
                names.text = temp_n;
                break;
            case 5:
                title.text = "5 Stage Rank";
                for (int i = 0; i < maingamedata.fifth_stage.Length; i++)
                {
                    temp = temp + (maingamedata.fifth_stage[i].ToString() + "\n");
                }
                scores.text = temp;

                for (int i = 0; i < maingamedata.fifth_stage_n.Length; i++)
                {
                    temp_n = temp_n + i + "등 " + (maingamedata.fifth_stage_n[i] + "\n");
                }
                names.text = temp_n;
                break;
        }
    }
    public void Resetrank()
    {
        Array.Resize(ref maingamedata.first_stage, 0);
        Array.Resize(ref maingamedata.second_stage, 0);
        Array.Resize(ref maingamedata.third_stage, 0);
        Array.Resize(ref maingamedata.fourth_stage, 0);
        Array.Resize(ref maingamedata.fifth_stage, 0);
        Array.Resize(ref maingamedata.first_stage_n, 0);
        Array.Resize(ref maingamedata.second_stage_n, 0);
        Array.Resize(ref maingamedata.third_stage_n, 0);
        Array.Resize(ref maingamedata.fourth_stage_n, 0);
        Array.Resize(ref maingamedata.fifth_stage_n, 0);
        reload();
        scores.text = "";
        names.text = "";
        DataManager.Instance.SaveMainGameData();
    }

    public void reload()
    {
        Array.Resize(ref first_stage, maingamedata.first_stage.Length);
        if(maingamedata.first_stage.Length > 0)
        {
            for (int i = 0; i < first_stage.Length; i++)
            {
                first_stage[i] = maingamedata.first_stage[i];
            }
        }
        Array.Resize(ref second_stage, maingamedata.second_stage.Length);
        for (int i = 0; i < second_stage.Length; i++)
        {
            if (maingamedata.second_stage.Length > 0)
            {
                second_stage[i] = maingamedata.second_stage[i];

            }
        }
        Array.Resize(ref third_stage, maingamedata.third_stage.Length);
        for (int i = 0; i < third_stage.Length; i++)
        {
            if (maingamedata.third_stage.Length > 0)
            {
                third_stage[i] = maingamedata.third_stage[i];

            }
        }
        Array.Resize(ref fourth_stage, maingamedata.fourth_stage.Length);
        for (int i = 0; i < fourth_stage.Length; i++)
        {
            if (maingamedata.fourth_stage.Length > 0)
            {
                fourth_stage[i] = maingamedata.fourth_stage[i];

            }
        }
        Array.Resize(ref fifth_stage, maingamedata.fifth_stage.Length);
        for (int i = 0; i < fifth_stage.Length; i++)
        {
            if (maingamedata.fifth_stage.Length > 0)
            {
                fifth_stage[i] = maingamedata.fifth_stage[i];

            }
        }

        Array.Resize(ref first_stage_n, maingamedata.first_stage_n.Length);
        if (maingamedata.first_stage_n.Length > 0)
        {
            for (int i = 0; i < first_stage_n.Length; i++)
            {
                first_stage_n[i] = maingamedata.first_stage_n[i];
            }
        }
        Array.Resize(ref second_stage_n, maingamedata.second_stage_n.Length);
        for (int i = 0; i < second_stage_n.Length; i++)
        {
            if (maingamedata.second_stage_n.Length > 0)
            {
                second_stage_n[i] = maingamedata.second_stage_n[i];

            }
        }
        Array.Resize(ref third_stage_n, maingamedata.third_stage_n.Length);
        for (int i = 0; i < first_stage_n.Length; i++)
        {
            if (maingamedata.third_stage_n.Length > 0)
            {
                third_stage_n[i] = maingamedata.third_stage_n[i];

            }
        }
        Array.Resize(ref fourth_stage_n, maingamedata.fourth_stage_n.Length);
        for (int i = 0; i < fourth_stage_n.Length; i++)
        {
            if (maingamedata.fourth_stage_n.Length > 0)
            {
                fourth_stage_n[i] = maingamedata.fourth_stage_n[i];

            }
        }
        Array.Resize(ref fifth_stage_n, maingamedata.fifth_stage_n.Length);
        for (int i = 0; i < fifth_stage_n.Length; i++)
        {
            if (maingamedata.fifth_stage_n.Length > 0)
            {
                fifth_stage_n[i] = maingamedata.fifth_stage_n[i];

            }
        }
    }
}
