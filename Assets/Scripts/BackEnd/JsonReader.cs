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

    // 스토리 id는 무엇을 기준으로 구분하나?
    // 1. json 파일           => 파일명
    // 2. excel 파일 sheet    => 변수명
    // 3. key값으로 구분
    public string filePath = "파일 경로를 입력해주세요";
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
