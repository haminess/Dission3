using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using UnityEngine.Networking;

[System.Serializable]
public class User
{
    public string Name;
    public string ID;

}

[System.Serializable]
public class Users
{
    public List<User> users = new List<User>();
}

public class Test : MonoBehaviour
{
    void Start()
    {
        //Test용 User create
        User user1 = new User
        {
            Name = "Kim",
            ID = "qq"
        };

        User user2 = new User
        {
            Name = "Lee",
            ID = "tt"
        };

        Users user_arr = new Users();

        //Add user
        user_arr.users.Add(user1);
        user_arr.users.Add(user2);

        //Convert JsonString
        string json = JsonUtility.ToJson(user_arr);

        //request Post
        StartCoroutine(Upload("http://URL", json));
    }

    IEnumerator Upload(string URL, string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }

        }
    }
}