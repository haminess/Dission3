using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JsonReader : MonoBehaviour
{
    [System.Serializable]
    public class Line
    {
        public string name;
        public string line;
    }

    [System.Serializable]
    public class LineList
    {
        public List<Line> script;
    }

    // ���丮 id�� ������ �������� �����ϳ�?
    // 1. json ����           => ���ϸ�
    // 2. excel ���� sheet    => ������
    // 3. key������ ����
    public string fileName = "ExcelTest";
    public LineList data;



    void Start()
    {
        //// ���� ��ο��� JSON ���� �о��
        //string filePath = Application.dataPath + "Resources/ExcelTest.json";
        //string jsonFile = File.ReadAllText(filePath);


        // Resources �������� JSON ������ �о��
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        if (jsonFile != null)
        {
            data = JsonUtility.FromJson<LineList>(jsonFile.text);

            //foreach (var data in myScript.script)
            //{
            //    Debug.Log(data.name + ": " + data.line);
            //}
        }
        else
        {
            Debug.LogError("JSON file not found!");
        }
    }
}
