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

    [Header("Battle ���� ������Ʈ")]
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private EnemyManager enemyManager;

    [Header("������s")]
    [SerializeField]
    private List<Observer> observers;

    [Header("�ǹ�")]
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
            //���� ����
            if (UnsafeUtility.EnumToInt<EnemyType>(enemyManager.enemyQueue.Peek().enemy.enemyType) == color)
            {
                EnemyControl enemy = enemyManager.enemyQueue.Peek();
                playerController.AttackEnmey(enemy.gameObject);

                //Dead Ȯ��
                if (enemy.Hp - 1 <= 0)
                {
                    //Dead  
                    enemy.Dead();
                    enemyManager.enemyQueue.Dequeue();

                    SpawnManager.Instance.SpawnEnemy(); // SPAWN
                    playerController.ForwardMove(enemy.transform);// ������ ����
                    EnemyDeadNotify();

                    feverImage.fillAmount += 0.05f;

                }
                else
                {
                    enemy.Hit(); //�� HP--;
                    if (!playerController.Attacking)
                    {
                        playerController.backMove();// �ڷ�
                    }
                    EnemyHitNotify();

                    feverImage.fillAmount += 0.03f;
                }
            }
            //���� ����
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

                //Dead Ȯ��
                if (enemy.Hp - 1 <= 0)
                {
                    //Dead  
                    enemy.Dead();
                    enemyManager.enemyQueue.Dequeue();

                    SpawnManager.Instance.SpawnEnemy(); // SPAWN
                    playerController.ForwardMove(enemy.transform);// ������ ����
                    EnemyDeadNotify();
                }
                else
                {
                    enemy.Hit(); //�� HP--;
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
        // 1. hp --,  2. ī�޶� ��鸲
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].PlayerHitNotify();
        }
    }

    public void EndNotify()
    {
        // ��ư Off
        // You Die On
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].EndNotify();
        }
    }
    #endregion
}
