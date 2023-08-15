using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Uploder : MonoBehaviour
{
    // 업로드할 JSON 파일의 경로와 파일명을 설정합니다.
    public string jsonFilePath = Application.persistentDataPath + "/" + "GameData.json";
    

    // API 엔드포인트 URL을 설정합니다.
    public string apiURL;

    // JSON 업로드 함수를 호출하는 예시 함수입니다.
    public void UploadJSON()
    {
        StartCoroutine(UploadJSONCoroutine());
    }

    private IEnumerator UploadJSONCoroutine()
    {
        // JSON 파일을 텍스트 형식으로 읽어옵니다.
        string jsonString = System.IO.File.ReadAllText(jsonFilePath);

        // UnityWebRequest를 생성합니다.
        UnityWebRequest request = new UnityWebRequest(apiURL, "POST");

        // 업로드할 JSON 데이터를 바이트 형태로 변환합니다.
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        // 업로드할 데이터를 설정합니다.
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 요청을 보냅니다.
        yield return request.SendWebRequest();

        // 요청이 성공적으로 완료되었는지 확인합니다.
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("JSON 업로드 성공!");
            Debug.Log("서버 응답: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("JSON 업로드 실패: " + request.error);
        }

        // 사용이 끝난 요청을 해제합니다.
        request.Dispose();
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
