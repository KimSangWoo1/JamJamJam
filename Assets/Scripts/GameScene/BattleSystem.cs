using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager;
    [SerializeField]
    private AttackChannel attackChannel;

    //Test
    GameObject enemy;
    private void OnEnable()
    {
        attackChannel.attackRequested += Attack;
    }

    private void OnDisable()
    {
        attackChannel.attackRequested -= Attack;
    }

    public void Attack(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.RED:
                enemy = enemyManager.RedList[0].gameObject;
                break;
            case AttackType.GREEN:
                enemy = enemyManager.GreenList[0].gameObject;
                break;
            case AttackType.BLUE:
                enemy = enemyManager.BlueList[0].gameObject;
                break;
            case AttackType.YELLOW:
                enemy = enemyManager.YellowList[0].gameObject;
                break;
            default:
                enemy = enemyManager.RedList[0].gameObject;
                break;
        }
        attackChannel.PlayerAttackEvent(enemy);
    }
}
