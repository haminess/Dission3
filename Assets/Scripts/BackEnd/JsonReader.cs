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

    // LineList 사용 예제
    // 변수       public LineList data;
    // 읽기       data = JsonUtility.FromJson<LineList>(jsonContent);
    // 사용       foreach (var i in data.script) { 값 : i.Column }

    [System.Serializable]
    public class DataItem
    {
        public string Column0;
        public string Column1;
        public string Column2;
    }

    // DataItem 사용 예제
    // 라이브러리  using Newtonsoft.Json;
    // 변수       public List<DataItem> data; 리스트로 사용
    // 읽기       data = JsonConvert.DeserializeObject<List<DataItem>>(jsonContent);
    // 사용       foreach (var i in data) { 값 : i.Column }

    // 스토리 id는 무엇을 기준으로 구분하나?
    // File과 Sheet로 구분
    public string filePath = "파일 경로를 입력해주세요";   // 필수 입력
    public string sheetName;    // 필수 입력
    public string folderPath = "폴더 경로를 입력해주세요";
    public string fileName = "파일 이름을 입력해주세요";
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

            // 데이터 출력 (확인용)
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


    // 특정 시트의 데이터를 가져오는 메서드
    public List<DataItem> GetSheetData(string sheetName)
    {
        if (dataList != null && dataList.ContainsKey(sheetName))
        {
            return dataList[sheetName];
        }
        return null;
    }

}
