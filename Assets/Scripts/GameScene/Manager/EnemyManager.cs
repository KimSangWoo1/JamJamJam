using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [Header("Enemy Lists")]
    [SerializeField]
    private List<GameObject> redList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> blueList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> greenList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> yellowList = new List<GameObject>();

    public Queue<EnemyControl> enemyQueue = new Queue<EnemyControl>();

    [Header("Parents")]
    [SerializeField]
    private GameObject redParent;
    [SerializeField]
    private GameObject blueParent;
    [SerializeField]
    private GameObject greenParent;

    // Properties
    public List<GameObject> RedList => redList;
    public List<GameObject> BlueList => blueList;
    public List<GameObject> GreenList => greenList;
    public List<GameObject> YellowList => yellowList;

    public void SetEnemy(EnemyType enemyType, EnemyControl clone)
    {
        switch (enemyType)
        {
            case EnemyType.RED:
                redList.Add(clone.gameObject);
                clone.transform.parent = redParent.transform;
                break;
            case EnemyType.BLUE:
                clone.transform.parent = blueParent.transform;
                blueList.Add(clone.gameObject);
                break;
            case EnemyType.GREEN:
                clone.transform.parent = greenParent.transform;
                greenList.Add(clone.gameObject);
                break;
        }
        enemyQueue.Enqueue(clone);
    }
}
