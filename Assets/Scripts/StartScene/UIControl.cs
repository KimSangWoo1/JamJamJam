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
        score.text = "Score : "+DataManager.Instance.Data.Score.ToString();
    }

    private void Start()
    {
        playerName.text = DataManager.Instance.Data.Score.ToString();
    }
}
