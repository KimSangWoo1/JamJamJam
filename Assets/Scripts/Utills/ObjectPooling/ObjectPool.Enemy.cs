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

    //������Ʈ ����
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

    //������Ʈ �ֱ�
    public void EnmeyPush(EnemyControl temp)
    {
        if (temp.gameObject.activeSelf)
        {
            temp.gameObject.SetActive(false);
        }
        temp.transform.parent = parent.transform;
        enmeyPool.Enqueue(temp);
    }

    //������Ʈ ����
    public EnemyControl EnmeyPop()
    {
        if (enmeyPool.Count == 0)
        {
            Creation();
        }
        return enmeyPool.Dequeue();
    }
}