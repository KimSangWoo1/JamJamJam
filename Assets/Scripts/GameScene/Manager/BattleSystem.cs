using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;

public class BattleSystem : SimpleSingleton<BattleSystem>, ISubject
{
    [Header("UI")]
    [SerializeField]
    private Image feverImage;

    [Header("Battle 메인 컴포턴트")]
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private EnemyManager enemyManager;

    [Header("옵저버s")]
    [SerializeField]
    private List<Observer> observers;

    [Header("피버")]
    [SerializeField]
    private float feverSpeed;
    [SerializeField]
    private float feverPower;
    private bool fever;

    [SerializeField]
    private float delay;

    private void Update()
    {
        if (fever)
        {
            StartCoroutine(FeverAttackMode());
            fever = false;
        }
        else
        {
            feverImage.fillAmount -= Time.deltaTime * feverSpeed;
        }
    }

    #region Btn Click 
    public void Attack(int color)
    {
        if (enemyManager.enemyQueue.Count != 0)
        {
            //공격 성공
            if (UnsafeUtility.EnumToInt<EnemyType>(enemyManager.enemyQueue.Peek().enemy.enemyType) == color)
            {
                EnemyControl enemy = enemyManager.enemyQueue.Peek();
                playerController.AttackEnmey(enemy.gameObject);

                //Dead 확인
                if (enemy.Hp - 1 <= 0)
                {
                    //Dead  
                    enemy.Dead();
                    enemyManager.enemyQueue.Dequeue();

                    SpawnManager.Instance.SpawnEnemy(); // SPAWN
                    playerController.ForwardMove(enemy.transform);// 앞으로 전진
                    EnemyDeadNotify();

                    feverImage.fillAmount += 0.05f;

                }
                else
                {
                    enemy.Hit(); //적 HP--;
                    if (!playerController.Attacking)
                    {
                        playerController.backMove();// 뒤로
                    }
                    EnemyHitNotify();

                    feverImage.fillAmount += 0.03f;
                }
            }
            //공격 실패
            else
            {
                if (playerController.Hp - 1 > 0)
                {
                    playerController.Hit();
                    PlayerHitNotify();

                    feverImage.fillAmount -= 0.25f;
                }
                else
                {
                    playerController.Dead();
                    EndNotify();
                }
            }
        }

        FeverCheck();
    }

    public void FeverAttackClick()
    {
        feverImage.fillAmount += 0.009f;
    }
    #endregion

    IEnumerator FeverAttackMode()
    {
        while (feverImage.fillAmount>=0.09f)
        {
            feverImage.fillAmount -=  feverSpeed * feverPower;

            if (enemyManager.enemyQueue.Count != 0)
            {
                EnemyControl enemy = enemyManager.enemyQueue.Peek();
                playerController.AttackEnmey(enemy.gameObject);

                //Dead 확인
                if (enemy.Hp - 1 <= 0)
                {
                    //Dead  
                    enemy.Dead();
                    enemyManager.enemyQueue.Dequeue();

                    SpawnManager.Instance.SpawnEnemy(); // SPAWN
                    playerController.ForwardMove(enemy.transform);// 앞으로 전진
                    EnemyDeadNotify();
                }
                else
                {
                    enemy.Hit(); //적 HP--;
                    EnemyHitNotify();
                }
            }

            yield return new WaitForSeconds(delay);
        }
        playerController.Fever(false);
        fever = false;
        FeverEndNotify();
        yield return null;
    }


    private void FeverCheck()
    {
        if (feverImage.fillAmount >= 0.98f)
        {
            fever = true;
            FeverNotify();
            playerController.Fever(true);
        }
    }

    #region ISubject Method
    public void FeverNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].FeverNotify();
        }
    }

    public void FeverEndNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].FeverEndNotify();
        }
    }

    public void EnemyDeadNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].EnemyDeadNotify();
        }
    }
    public void EnemyHitNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].EnemyHitNotify();
        }
    }

    public void PlayerHitNotify()
    {
        // 1. hp --,  2. 카메라 흔들림
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].PlayerHitNotify();
        }
    }

    public void EndNotify()
    {
        // 버튼 Off
        // You Die On
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].EndNotify();
        }
    }
    #endregion
}
