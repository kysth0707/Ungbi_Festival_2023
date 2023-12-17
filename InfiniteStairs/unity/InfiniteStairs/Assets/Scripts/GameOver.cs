using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] AudioSource gameOverSound;
    GameManager GM;

    public bool isDie = false;
    bool isSoundPlayed = false;
    float dyingTime = 0;
    void Start()
    {
        GM = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDie)
        {
            return;
        }

        dyingTime += Time.deltaTime;
        if (dyingTime > 0.5f)
        {
            if(!isSoundPlayed)
            {
                gameOverSound.Play();
                isSoundPlayed = true;

                StartCoroutine(GetRequest());
            }
        }
        if(dyingTime > 2f)
        {
            SceneManager.LoadScene("Play");
        }
    }

    IEnumerator GetRequest()
    {
        string url = "http://nojam.easylab.kr:9999/add/infinitestairs/" + GM.PlayerScore.ToString();

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
