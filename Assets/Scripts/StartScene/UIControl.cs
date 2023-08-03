using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    public Text playerName;
    [SerializeField]
    public Text score;

    [SerializeField]
    public Button setting;
    [SerializeField]
    public Button store;
    [SerializeField]
    public Button quest;
    [SerializeField]
    public Button reward;


    private void OnEnable()
    {
        score.text = "Score : " + DataManager.Instance.GetBestScore();
    }

    private void Start()
    {
        string name = DataManager.Instance.Data.Name.ToString();
        if(name!=null || string.IsNullOrWhiteSpace(name))
        {
            name = "Guest1";
        }
        playerName.text = name;
    }
}
