using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntData //: MonoBehaviour
{
    public int[,] Int2DArray = new int[3, 5];

    public IntData()
    {
        // TwoDimensionalArray를 기본 값으로 초기화합니다. (필요에 따라 변경 가능합니다.)
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Int2DArray[i, j] = 0; // 기본 값으로 설정합니다. (필요에 따라 변경 가능합니다.)
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
