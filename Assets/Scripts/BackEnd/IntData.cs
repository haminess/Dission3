using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntData //: MonoBehaviour
{
    public int[,] Int2DArray = new int[3, 5];

    public IntData()
    {
        // TwoDimensionalArray�� �⺻ ������ �ʱ�ȭ�մϴ�. (�ʿ信 ���� ���� �����մϴ�.)
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Int2DArray[i, j] = 0; // �⺻ ������ �����մϴ�. (�ʿ信 ���� ���� �����մϴ�.)
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
