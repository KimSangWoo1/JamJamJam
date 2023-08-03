using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    private Button startBtn;

   public void GameStart()
    {
        SceneChanger.Instance.SceneChange("GameScene");
        startBtn.interactable = false;
    }
}
