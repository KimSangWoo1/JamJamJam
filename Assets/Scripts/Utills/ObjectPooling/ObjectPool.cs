using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class ObjectPool
{
    public enum PollType { ENMEY, COIN, SCORE, FX, GROUND}; //������Ʈ Ǯ�� ����
    public PollType pollType;

    //Ǯ�� ����
    private GameObject parent;

    private GameObject prefab; //Prefab
    private GameObject clone; //Clone

    private Queue<GameObject> pool = new Queue<GameObject>();

    private int size;

    public ObjectPool(PollType _pollType, GameObject _prefab, int _size)
    {
        pollType = _pollType;
        prefab = _prefab;
        size = _size;

        Creation();
    }

    //������Ʈ ����
    public void Creation()
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
            clone = GameObject.Instantiate(prefab, parent.transform.position, Quaternion.identity, parent.transform);
            clone.SetActive(false);
            pool.Enqueue(clone);
        }
    }

    //������Ʈ �ֱ�
    public void Push(GameObject temp)
    {
        if (temp.activeSelf)
        {
            temp.SetActive(false);
        }
        temp.transform.parent = parent.transform;
        pool.Enqueue(temp);
    }

    //������Ʈ ����
    public GameObject Pop()
    {
        if (pool.Count == 0)
        {
            Creation();
        }
        return pool.Dequeue();
    }
}

