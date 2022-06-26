using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private Data data;

    public Data Data => data;

    private void Awake()
    {
        base.Awake();

        StartLoadData();
    }

    //게임 시작 Data Load
    public void StartLoadData()
    {
        data = new Data();

        //data.Name = PlayerPrefs.GetString("Name");
        data.Score = PlayerPrefs.GetInt("Score");
        data.PlyaerNum = PlayerPrefs.GetInt("PlayerNum");
    }
}
