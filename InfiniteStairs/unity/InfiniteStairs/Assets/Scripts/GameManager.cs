using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<TMP_Text> ScoreObjs = new List<TMP_Text>();

    [SerializeField] Slider gagueFill;
    
    [SerializeField] AudioSource coin;
    [SerializeField] AudioSource gameoverSound;

    public int PlayerX = 0;
    public int PlayerY = 0;

    public int LastStairX = 0;
    public int LastStairY = 0;
    bool isStairLeft = true;

    public List<int> StairX;
    public List<int> StairY;
    public List<bool> IsCoin = new List<bool>();

    public int PlayerScore = 0;
    public int CoinScore = 0;

    float timeValue = 0f;
    float timeLimit = 0f;
    float firstTimeLimit = 5f;

    Player player;
    GameOver GO;

    private void Awake()
    {
        player = GetComponent<Player>();
        GO = GetComponent<GameOver>();

        StairX.Add(0);
        StairY.Add(0);
        IsCoin.Add(false);
        for (int i = 0; i < 5; i++)
        {
            AddStair(false);
        }
        for(int i = 0; i < 10; i++)
        {
            AddStair();
        }
    }

    ScoreManager SM;
    private void Start()
    {
        SM = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        whenGoUp();
    }

    private void Update()
    {
        timeValue += Time.deltaTime;

        PlayerScore = PlayerY + CoinScore;
        for(int i = 0; i < ScoreObjs.Count; i++)
        {
            ScoreObjs[i].text = PlayerScore.ToString();
        }

        for (int i = 0; i < StairY.Count; i++)
        {
            if(StairY[i] == PlayerY)
            {
                if(StairX[i] != PlayerX)
                {
                    if(!GO.isDie)
                    {
                        playerDie();
                    }
                    // »ç¸Á¾²~
                }
                else
                {
                    if(IsCoin[i])
                    {
                        coin.Play();
                        CoinScore += 1;
                        IsCoin[i] = false;
                    }
                }
            }
        }
        if(PlayerScore > 5) updateGague();
    }

    public void whenGoUp()
    {
        timeLimit = firstTimeLimit - PlayerScore / 30;
        if (timeLimit < 0.7f) timeLimit = 0.7f;

        timeValue = 0f;
    }

    private void updateGague()
    {
        if (GO.isDie) return;

        if (timeValue > timeLimit)
        {
            playerDie();
            return;
        }
        try
        {
            gagueFill.value = 1 - (timeValue / timeLimit);
        }
        catch
        {

        }
    }

    private void playerDie()
    {
        player.playerStatus = player.PLAYER_DIE;
        GO.isDie = true;
        SM.Scores.Add(PlayerScore);
        SM.SortIt();
    }

    public void AddStair(bool turn = true)
    {
        LastStairY += 1;

        int turnPercent = 10 + (PlayerScore / 20);
        if (turnPercent > 30) turnPercent = 30;

        int coinPercent = 30 + (PlayerScore / 20);
        if (coinPercent > 50) coinPercent = 50;

        if (turn && Random.Range(0, 100) < turnPercent)
        {
            isStairLeft = !isStairLeft;
        }

        if(Random.Range(0, 100) < coinPercent)
        {
            IsCoin.Add(true);
        }
        else
        {
            IsCoin.Add(false);
        }

        if(isStairLeft)
        {
            LastStairX -= 1;
        }
        else
        {
            LastStairX += 1;
        }
        StairX.Add(LastStairX);
        StairY.Add(LastStairY);

        if(StairX.Count > 20)
        {
            StairX.RemoveAt(0);
            StairY.RemoveAt(0);
            IsCoin.RemoveAt(0);
        }
    }
}
