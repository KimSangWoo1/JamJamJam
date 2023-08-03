using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : EnemyBase
{
    #region Ani
    public void Hit()
    {
        ani.SetTrigger("Hit");
        hp--;
        enemyHpControl.ChangeHP();
    }

    public void Dead()
    {
        ani.SetTrigger("Dead");
        hp--;
        enemyHpControl.ChangeHP();

        StartCoroutine(RemoveEnemy());
    }

    #endregion

    #region  Coroutine
    IEnumerator RemoveEnemy()
    {
        yield return wait;
        SpawnManager.Instance.PoolPush(enemy.enemyType, this);
    }
    #endregion
}
