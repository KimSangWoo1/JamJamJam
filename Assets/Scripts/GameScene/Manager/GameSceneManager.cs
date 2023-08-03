using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private ScreenManager screenManager;

    public void GameReTry()
    {
        screenManager.AttackBtnSetting(false);
        screenManager.GameEndBtnOff();
        SceneChanger.Instance.SceneChange("BlankScene");

    }

    public void GameEnd()
    {
        screenManager.AttackBtnSetting(false);
        screenManager.GameEndBtnOff();
        SceneChanger.Instance.SceneChange("StartScene");
    }
}
