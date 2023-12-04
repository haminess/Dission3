using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    public TextMeshPro names;
    public TextMeshPro scores;
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
        names.text = "";
        scores.text = "";
        for(int i = 0; i < 10; i++)
        {
            string name = PlayerPrefs.GetString(i + "", "없음");
            int score = PlayerPrefs.GetInt(i + "", 0);

            if(name != "없음")
            {

            }
        }
    }


}
