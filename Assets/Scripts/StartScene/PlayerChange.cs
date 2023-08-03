using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    // 
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private int playerNumber;

    private void Awake()
    {
        for(int i=0; i<players.Length; i++)
        {
            players[i].SetActive(false);
        }
    }

    private void Start()
    {
        players[DataManager.Instance.Data.PlyaerNum].SetActive(true);
    }

    public void ChangeClick(int count)
    {
        players[playerNumber].SetActive(false);

        playerNumber += count;
        if (playerNumber < 0)
        {
            playerNumber = players.Length - 1;
        }
        else if (playerNumber >= players.Length)
        {
            playerNumber = 0;
        }
        DataManager.Instance.SavePlayerNumber(playerNumber);
        players[playerNumber].SetActive(true);
    }
}
