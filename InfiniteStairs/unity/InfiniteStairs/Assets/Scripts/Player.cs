using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject PlayerParent;
    [SerializeField] List<GameObject> PlayerStanding;
    [SerializeField] List<GameObject> PlayerRunning;
    [SerializeField] GameObject PlayerDie;
    [SerializeField] GameObject PlayerBag;

    [SerializeField] AudioSource stairUp;

    public int PLAYER_STANDING = 0;
    public int PLAYER_RUNNING = 1;
    public int PLAYER_DIE = 2;
    
    public int playerStatus = 0;

    bool goUp = false;
    bool turnAndGoUp = false;
    bool isPlayerLeft = true;

    float animationTime = 0;
    float dyingTime = 0;

    ArduinoCommunication AC;
    GameManager GM;
    private void Start()
    {
        AC = transform.GetComponent<ArduinoCommunication>();
        GM = transform.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatus == PLAYER_DIE)
        {
            dyingTime += Time.deltaTime;

            hideAllImages();
            PlayerDie.SetActive(true);

            PlayerBag.SetActive(true);
            for (int i = 0; i < GM.StairY.Count; i++)
            {
                if (GM.StairY[i] == GM.PlayerY - 1)
                {
                    PlayerBag.transform.position = new Vector3(GM.StairX[i], GM.StairY[i], 0);
                }
            }

            if (dyingTime > 1f)
            {
                PlayerParent.transform.position -= new Vector3(0, Time.deltaTime * 10f, 0);
            }

            return;
        }
        hideAllImages();
        checkButtons();

        if (goUp)
        {
            stairUp.Play();
            GM.whenGoUp();

            GM.AddStair();
            animationTime = 0f;
            goUp = false;
            playerStatus = PLAYER_RUNNING;

            PlayerParent.transform.position += new Vector3(isPlayerLeft ? -1 : 1, 1, 0);
            GM.PlayerX += isPlayerLeft ? -1 : 1;
            GM.PlayerY += 1;
        }
        else if (turnAndGoUp)
        {
            stairUp.Play();
            GM.whenGoUp();

            GM.AddStair();
            animationTime = 0f;
            turnAndGoUp = false;
            isPlayerLeft = !isPlayerLeft;
            if (isPlayerLeft)
            {
                PlayerParent.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                PlayerParent.transform.localScale = new Vector3(-1, 1, 1);
            }

            playerStatus = PLAYER_RUNNING;

            PlayerParent.transform.position += new Vector3(isPlayerLeft ? -1: 1, 1, 0);
            GM.PlayerX += isPlayerLeft ? -1 : 1;
            GM.PlayerY += 1;
        }



        
        if(playerStatus == PLAYER_RUNNING)
        {
            if(animationTime < 0.2f)
            {
                if (animationTime < 0.2f / 3f)
                {
                    PlayerRunning[0].SetActive(true);
                }
                else if (animationTime < 0.2f / 3f * 2f)
                {
                    PlayerRunning[1].SetActive(true);
                }
                else
                {
                    PlayerRunning[2].SetActive(true);
                }
            }
            else
            {
                playerStatus = PLAYER_STANDING;
            }
        }

        if (playerStatus == PLAYER_STANDING)
        {
            animationTime %= 2f;
            if (animationTime < 1f)
            {
                PlayerStanding[0].SetActive(true);
            }
            else
            {
                PlayerStanding[1].SetActive(true);
            }
        }

        animationTime += Time.deltaTime;
    }
    void checkButtons()
    {
        if (!goUp)
        {
            goUp = Input.GetKeyDown(KeyCode.J) || AC.goUp;
            AC.goUp = false;
        }
        if(!turnAndGoUp)
        {
            turnAndGoUp = Input.GetKeyDown(KeyCode.F) || AC.turnAndGoUp;
            AC.turnAndGoUp = false;
        }
    }

    void hideAllImages()
    {
        PlayerStanding[0].SetActive(false);
        PlayerStanding[1].SetActive(false);

        PlayerRunning[0].SetActive(false);
        PlayerRunning[1].SetActive(false);
        PlayerRunning[2].SetActive(false);

        PlayerDie.SetActive(false);
    }
}
