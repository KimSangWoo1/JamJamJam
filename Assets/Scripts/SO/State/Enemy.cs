using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Assets/SO/State/Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public EnemyType enemyType;
}
