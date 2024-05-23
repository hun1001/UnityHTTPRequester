using UnityEngine;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;

namespace HpRequester
{
    public class HttpRequester
    {
        private readonly string URL;
        private HttpClient client;

        public HttpRequester(string URL)
        {
            this.URL = URL;
            client = new HttpClient();
        }

        public void GetRequest(string path, Action<string> callback = null)
        {
            string requsetPath = GetPath(path);

            GetAsync(requsetPath).ContinueWith(task =>
            {
                if(task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                var response = task.Result;
                var content = response.Content.ReadAsStringAsync().Result;

                callback?.Invoke(content);
            });
        }

        public void PostRequest(string path, string json, Action<string> callback)
        {
            string requsetPath = GetPath(path);

            PostAsync(requsetPath, json).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                var response = task.Result;
                var content = response.Content.ReadAsStringAsync().Result;

                callback?.Invoke(content);
            });
        }

        private Task<HttpResponseMessage> GetAsync(string path)
        {
            return client.GetAsync(path);
        }

        private Task<HttpResponseMessage> PostAsync(string path, string json)
        {
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return client.PostAsync(path, content);
        }

        private string GetPath(string path)
        {
            return URL + path;
        }
    }
}