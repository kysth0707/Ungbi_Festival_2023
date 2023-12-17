using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject StairPrefab;
    public GameObject CoinPrefab;
    GameManager GM;

    List<GameObject> Stairs = new List<GameObject>();
    List<GameObject> Coins = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GM = transform.GetComponent<GameManager>();
        for (int i = 0; i < 20; i++)
        {
            Stairs.Add(Instantiate(StairPrefab));
            Coins.Add(Instantiate(CoinPrefab));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i < GM.StairX.Count)
            {
                Stairs[i].SetActive(true);
                Stairs[i].transform.position = new Vector3(GM.StairX[i], GM.StairY[i], 0);
            }
            else
            {
                Stairs[i].SetActive(false);
            }

            if (i < GM.IsCoin.Count)
            {
                if (GM.IsCoin[i])
                {
                    Coins[i].SetActive(true);
                    Coins[i].transform.position = new Vector3(GM.StairX[i], GM.StairY[i], 0);
                }
                else
                {
                    Coins[i].SetActive(false);
                }
            }
            else
            {
                Coins[i].SetActive(false);
            }
        }
    }
}
