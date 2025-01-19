using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 1. 먼저 씬에서 찾아보기
                instance = FindObjectOfType<GameManager>();

                // 2. 씬에도 없다면 생성
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public MODE mode = MODE.DEBUG;

    public void PlayGame()
    {

    }
}
