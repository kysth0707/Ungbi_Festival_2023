using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    string MyName;
    string MyScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator getMyName()
    {
        string url = "http://nojam.easylab.kr:9999/latest";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            MyName = www.downloadHandler.text;
            MyName = MyName.Substring(1, MyName.Length - 1);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
