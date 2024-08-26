using UnityEngine;
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

    // LineList ��� ����
    // ����       public LineList data;
    // �б�       data = JsonUtility.FromJson<LineList>(jsonContent);
    // ���       foreach (var i in data.script) { �� : i.Column }

    [System.Serializable]
    public class DataItem
    {
        public string Column0;
        public string Column1;
        public string Column2;
    }

    // DataItem ��� ����
    // ���̺귯��  using Newtonsoft.Json;
    // ����       public List<DataItem> data; ����Ʈ�� ���
    // �б�       data = JsonConvert.DeserializeObject<List<DataItem>>(jsonContent);
    // ���       foreach (var i in data) { �� : i.Column }

    // ���丮 id�� ������ �������� �����ϳ�?
    // File�� Sheet�� ����
    public string filePath = "���� ��θ� �Է����ּ���";   // �ʼ� �Է�
    public string sheetName;    // �ʼ� �Է�
    public string folderPath = "���� ��θ� �Է����ּ���";
    public string fileName = "���� �̸��� �Է����ּ���";
    public Dictionary<string, List<DataItem>> dataList;
    public List<DataItem> data;


    private void Start()
    {
        ReadJsonFromPath(filePath);
        data = dataList[sheetName];
    }

    [ContextMenu("ReadData")]
    public void ReadData()
    {
        ReadJsonFromPath(filePath);
        data = dataList[sheetName];
    }

    public void ReadJsonFromPath(string path)
    {
        if (File.Exists(path))
        {
            string jsonContent = File.ReadAllText(path);
            dataList = JsonConvert.DeserializeObject<Dictionary<string, List<DataItem>>>(jsonContent);
            Debug.Log("JSON file successfully read from: " + path);

            // ������ ��� (Ȯ�ο�)
            foreach (var temp in dataList)
            {
                Debug.Log($"Sheet: {temp.Key}");
                foreach (var item in temp.Value)
                {
                    //Debug.Log($"  Column0: {item.Column0}, Column1: {item.Column1}, Column2: {item.Column2}");
                }
            }
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + path);
        }
    }


    // Ư�� ��Ʈ�� �����͸� �������� �޼���
    public List<DataItem> GetSheetData(string sheetName)
    {
        if (dataList != null && dataList.ContainsKey(sheetName))
        {
            return dataList[sheetName];
        }
        return null;
    }

}
