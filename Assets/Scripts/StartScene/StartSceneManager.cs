using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    
   public void GameStart()
    {
        SceneChanger.Instance.SceneChange("GameScene");
    }
}
