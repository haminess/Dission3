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

    // 스토리 id는 무엇을 기준으로 구분하나?
    // 1. json 파일           => 파일명
    // 2. excel 파일 sheet    => 변수명
    // 3. key값으로 구분
    public string fileName = "ExcelTest";
    public LineList data;



    void Start()
    {
        //// 로컬 경로에서 JSON 파일 읽어옴
        //string filePath = Application.dataPath + "Resources/ExcelTest.json";
        //string jsonFile = File.ReadAllText(filePath);


        // Resources 폴더에서 JSON 파일을 읽어옴
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
