using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SimpleSingleton<SpawnManager>
{
    [SerializeField]
    private EnemyChannel enemyChannel;
    [SerializeField]
    private EnemyManager enemyManager;

    [Header("Spanw Pos")]
    [SerializeField]
    private Transform redSapwn;
    [SerializeField]
    private Transform greenSapwn;
    [SerializeField]
    private Transform blueSapwn;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject redPrefab;
    [SerializeField]
    private GameObject bluePrefab;
    [SerializeField]
    private GameObject greenPrefab;

    //ObjectPool
    ObjectPool redObjectPool;
    ObjectPool blueObjectPool;
    ObjectPool greenObjectPool;

    bool startSpawn;

    private void Awake()
    {
        redObjectPool = new ObjectPool(ObjectPool.PollType.ENMEY, redPrefab);
        blueObjectPool = new ObjectPool(ObjectPool.PollType.ENMEY, bluePrefab);
        greenObjectPool = new ObjectPool(ObjectPool.PollType.ENMEY, greenPrefab);
    }

    void Start()
    {
        startSpawn = true;
    }

    void Update()
    {
        if (startSpawn)
        {
            StartCoroutine(StartGameSetting(12, true));
            startSpawn = false;
        }
    }

    public void SpawnEnemy()
    {
        StartCoroutine(StartGameSetting(1,false));

        redSapwn.transform.position += -redSapwn.transform.forward * 2f;
        blueSapwn.transform.position += -blueSapwn.transform.forward * 2f;
        greenSapwn.transform.position += -greenSapwn.transform.forward * 2f;
    }

    public void PoolPush(EnemyType enemyType, EnemyControl enemy)
    {
        switch (enemyType)
        {
            case EnemyType.RED:
                redObjectPool.EnmeyPush(enemy);
                break;
            case EnemyType.BLUE:
                blueObjectPool.EnmeyPush(enemy);
                break;
            case EnemyType.GREEN:
                greenObjectPool.EnmeyPush(enemy);
                break;
        }
    }

    IEnumerator StartGameSetting(int num, bool start)
    {
        EnemyControl clone = null;

        UnityEngine.Random.InitState((int)redSapwn.position.x %50);

        for (int i = 0; i < num; i++)
        {
            int index = UnityEngine.Random.Range(0, 3);
            int hp = UnityEngine.Random.Range(1, 4);
            switch (index)
            {
                case 0:
                    clone = redObjectPool.EnmeyPop();
                    clone.gameObject.SetActive(true);
                    clone.transform.position = redSapwn.position + redSapwn.forward * 2f;
                    clone.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
                    clone.SetHp(hp);
                    enemyManager.SetEnemy(EnemyType.RED, clone);
                    break;
                case 1:
                    clone = blueObjectPool.EnmeyPop();
                    clone.gameObject.SetActive(true);
                    clone.transform.position = blueSapwn.position + blueSapwn.forward * 2f;
                    clone.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
                    clone.SetHp(hp);
                    enemyManager.SetEnemy(EnemyType.BLUE, clone);
                    break;
                case 2:
                    clone = greenObjectPool.EnmeyPop();
                    clone.gameObject.SetActive(true);
                    clone.transform.position = greenSapwn.position + greenSapwn.forward * 2f;
                    clone.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
                    clone.SetHp(hp);
                    enemyManager.SetEnemy(EnemyType.GREEN, clone);
                    break;
            }
            if (start)
            {
                enemyChannel.Event(); //ÇÑÄ­ ÀüÁø
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
}
