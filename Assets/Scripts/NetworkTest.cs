using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using System;

[Serializable]
class AddRankingJson
{
    public string GameID;
    public string UserName;
    public int Score;
}

public class NetworkTest : MonoBehaviour
{
    private TMP_InputField inputField;

    [SerializeField]
    private string URL = "http://localhost:3000/"; //http://ec2-13-211-172-179.ap-southeast-2.compute.amazonaws.com:3000/
    private HttpClient client = new HttpClient();

    [SerializeField]
    private AddRankingJson addRankingJson;

    private void Awake()
    {
        inputField = FindObjectOfType<TMP_InputField>();
        client.BaseAddress = new Uri(URL);
    }

    private void GetRequest(string path)
    {
        string requsetPath = URL + path;

        GetAsync(requsetPath).ContinueWith(task =>
        {
            if(task.IsFaulted)
            {
                Debug.LogError(task.Exception);
                return;
            }

            var response = task.Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Debug.Log(content);
        });
    }

    private void PostRequest(string path, string json)
    {
        string requsetPath = URL + path;

        PostAsync(requsetPath, json).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
                return;
            }

            var response = task.Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Debug.Log(content);
        });
    }

    private Task<HttpResponseMessage> GetAsync(string path)
    {
        return client.GetAsync(path);
    }

    private Task<HttpResponseMessage> PostAsync(string path, string json)
    {
        HttpContent content = new StringContent(json);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        return client.PostAsync(path, content);
    }

    public void ClickGetButton()
    {
        GetRequest(inputField.text);
        inputField.text = "";
    }

    public void ClickPostButton()
    {
        string jsonString = JsonUtility.ToJson(addRankingJson);
        PostRequest(inputField.text, jsonString);
        inputField.text = "";
    }
}
