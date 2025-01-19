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
                // 1. ���� ������ ã�ƺ���
                instance = FindObjectOfType<GameManager>();

                // 2. ������ ���ٸ� ����
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
