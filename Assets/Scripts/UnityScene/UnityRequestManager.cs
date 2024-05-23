using System;
using UnityEngine;
using HpRequester;
using TMPro;

namespace UnityScene
{
    [Serializable]
    public class JsonData
    {
        // Add your fields here
    }

    public class UnityRequestManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private TMP_InputField pathInput;

        [Header("Info")]
        [SerializeField]
        private string URL;

        [SerializeField]
        private JsonData jsonData;

        private HttpRequester requester;

        private void Awake()
        {
            requester = new HttpRequester(URL);
        }

        public void GetRequest()
        {
            requester.GetRequest(pathInput.text, (string content) =>
            {
                Debug.Log(content);
            });

            pathInput.text = "";
        }

        public void PostRequest()
        {
            string json = JsonUtility.ToJson(jsonData);

            requester.PostRequest(pathInput.text, json, (string content) =>
            {
                Debug.Log(content);
            });

            pathInput.text = "";
        }
    }
}