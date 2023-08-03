using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ObjectPool
{
    private Queue<EnemyControl> enmeyPool = new Queue<EnemyControl>();

    public ObjectPool(PollType _pollType, GameObject _prefab)
    {
        pollType = _pollType;
        prefab = _prefab;
        size = 20;

        EnmeyCreation();
    }

    //오브젝트 생성
    public void EnmeyCreation()
    {
        if (parent == null || !parent.activeInHierarchy)
        {
            parent = GameObject.Find(pollType.ToString());
            if (parent == null)
            {
                parent = new GameObject();
                parent.transform.name = pollType.ToString();
            }
        }

        for (int i = 0; i < size; i++)
        {
            EnemyControl enmeyClone = GameObject.Instantiate(prefab, parent.transform.position, Quaternion.identity, parent.transform).GetComponent<EnemyControl>();
            enmeyClone.gameObject.SetActive(false);
            enmeyPool.Enqueue(enmeyClone);
        }
    }

    //오브젝트 넣기
    public void EnmeyPush(EnemyControl temp)
    {
        if (temp.gameObject.activeSelf)
        {
            temp.gameObject.SetActive(false);
        }
        temp.transform.parent = parent.transform;
        enmeyPool.Enqueue(temp);
    }

    //오브젝트 빼기
    public EnemyControl EnmeyPop()
    {
        if (enmeyPool.Count == 0)
        {
            Creation();
        }
        return enmeyPool.Dequeue();
    }
}