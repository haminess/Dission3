using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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

    [System.Serializable]
    public class DataItem
    {
        public string Column0;
        public string Column1;
    }

    // ���丮 id�� ������ �������� �����ϳ�?
    // 1. json ����           => ���ϸ�
    // 2. excel ���� sheet    => ������
    // 3. key������ ����
    public string filePath = "���� ��θ� �Է����ּ���";
    //public LineList data;
    public List<DataItem> data;



    private void Start()
    {
        ReadJsonFromPath(filePath);
    }

    public void ReadJsonFromPath(string path)
    {
        if (File.Exists(path))
        {
            string jsonContent = File.ReadAllText(path);
            //data = JsonUtility.FromJson<LineList>(jsonContent);
            data = JsonConvert.DeserializeObject<List<DataItem>>(jsonContent);
            Debug.Log("JSON file successfully read from: " + path);
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + path);
        }
    }
}
