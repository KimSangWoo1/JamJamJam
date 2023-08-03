using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public class ScreenManager : Observer
{
    // UI
    [Header("Player 상단 정보 UI")]
    [SerializeField]
    private Image[] hearts;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("공격 버튼들")]
    [SerializeField]
    private Button[] attackBtns;
    [SerializeField]
    private GameObject feverBtn;

    [Header("게임 종료 후 버튼들")]
    [SerializeField]
    private Button[] gameEndBtns;
    [Header("게임 진행 설명 Text")]
    [SerializeField]
    private TextMeshProUGUI gameInfoTxt;

    int score = 0;

    readonly WaitForSeconds wait = new WaitForSeconds(0.2f);
    
    private void Start()
    {
        StartCoroutine(AutoComplete("Ready..."));
    }

    private void AttackBtenOff(bool check)
    {
        for (int i = 0; i < attackBtns.Length; i++)
        {
            attackBtns[i].transform.parent.gameObject.SetActive(check);
        }
    }

    public void AttackBtnSetting(bool check)
    {
        for (int i = 0; i < attackBtns.Length; i++)
        {
            attackBtns[i].interactable = check;
        }
    }

    public void GameEndBtnOff()
    {
        for (int i = 0; i < gameEndBtns.Length; i++)
        {
            gameEndBtns[i].interactable = false;
        }
    }

    #region Observer
    public override void FeverNotify()
    {
        AttackBtnSetting(false);
        AttackBtenOff(false);
        feverBtn.gameObject.SetActive(true);
    }

    public override void FeverEndNotify()
    {
        AttackBtnSetting(true);
        AttackBtenOff(true);
        feverBtn.gameObject.SetActive(false);
    }

    public override void PlayerHitNotify()
    {
        for(int i=0; i<hearts.Length; i++)
        {
            if (hearts[i].gameObject.activeSelf)
            {
                hearts[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    public override void EnemyHitNotify()
    {
        score += 10;
        scoreText.text = "Score : " + score.ToString();
    }

    public override void EnemyDeadNotify()
    {
        score += 100;
        scoreText.text = "Score : "+score.ToString();
    }

    //게임 종료
    public override void EndNotify()
    {
        AttackBtnSetting(false);
        feverBtn.gameObject.SetActive(false);

        PlayerHitNotify();
        for (int i=0; i< gameEndBtns.Length; i++)
        {
            gameEndBtns[i].gameObject.SetActive(true);
        }
        StartCoroutine(TextShow("YOU DIE",false));

        DataManager.Instance.SaveBestScore(score);
    }
    #endregion
    #region Coroutine
    IEnumerator AutoComplete(string content)
    {
        AttackBtnSetting(false);

        gameInfoTxt.text = "";

        for (int i = 0; i < content.Length; i++)
        {
            yield return wait;
            gameInfoTxt.text += content[i];

        }
        yield return wait;

        StartCoroutine(TextShow("START",true));
    }

    IEnumerator TextShow(string content, bool check)
    {
        gameInfoTxt.text = content;

        yield return wait;
        yield return wait;

        if (check)
        {
            AttackBtnSetting(true);
            gameInfoTxt.text = "";
        }
    }
    #endregion
}
