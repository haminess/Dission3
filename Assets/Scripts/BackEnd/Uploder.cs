using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Uploder : MonoBehaviour
{
    // ���ε��� JSON ������ ��ο� ���ϸ��� �����մϴ�.
    public string jsonFilePath = Application.persistentDataPath + "/" + "GameData.json";
    

    // API ��������Ʈ URL�� �����մϴ�.
    public string apiURL;

    // JSON ���ε� �Լ��� ȣ���ϴ� ���� �Լ��Դϴ�.
    public void UploadJSON()
    {
        StartCoroutine(UploadJSONCoroutine());
    }

    private IEnumerator UploadJSONCoroutine()
    {
        // JSON ������ �ؽ�Ʈ �������� �о�ɴϴ�.
        string jsonString = System.IO.File.ReadAllText(jsonFilePath);

        // UnityWebRequest�� �����մϴ�.
        UnityWebRequest request = new UnityWebRequest(apiURL, "POST");

        // ���ε��� JSON �����͸� ����Ʈ ���·� ��ȯ�մϴ�.
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        // ���ε��� �����͸� �����մϴ�.
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // ��û�� �����ϴ�.
        yield return request.SendWebRequest();

        // ��û�� ���������� �Ϸ�Ǿ����� Ȯ���մϴ�.
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("JSON ���ε� ����!");
            Debug.Log("���� ����: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("JSON ���ε� ����: " + request.error);
        }

        // ����� ���� ��û�� �����մϴ�.
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
