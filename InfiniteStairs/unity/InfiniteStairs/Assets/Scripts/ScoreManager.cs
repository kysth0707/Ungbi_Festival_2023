using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    GameObject GUIScores;
    public List<int> Scores;

    float updateTime = 0;

    private void Awake()
    {
        var obj = FindObjectsOfType<ScoreManager>();

        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for(int i = 0; i < 7; i++)
        {
            Scores.Add(0);
        }
    }

    public void SortIt()
    {
        Scores = Scores.OrderByDescending(x => x).ToList();
    }

    private void Update()
    {
        updateTime += Time.deltaTime;

        if(updateTime > 0.5f)
        {
            updateTime = 0;
            updateScores();
        }

    }

    private void updateScores()
    {
        try
        {
            GUIScores = GameObject.Find("Canvas").transform.Find("Scores").gameObject;
        }
        catch
        {
            return;
        }

        for (int i = 0; i < 7; i++)
        {
            Transform child = GUIScores.transform.GetChild(i);

            if (i >= Scores.Count)
                continue;

            child.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString() + ". " + Scores[i].ToString();
            child.GetChild(1).GetComponent<TMP_Text>().text = (i + 1).ToString() + ". " + Scores[i].ToString();
        }
    }
}
