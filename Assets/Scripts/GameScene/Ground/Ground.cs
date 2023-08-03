using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Observer
{
    public static Ground Instance;

    [Header("Prefabs")]
    [SerializeField]
    private List<GameObject> blocks;
    [SerializeField]
    private List<GameObject> props;

    [Header("Create Postion")]
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject probsTarget;

    private ObjectPool groundPool;
    [SerializeField]
    private int count; //8 
    private int createCount;
    private int propsIndex;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        createCount = 0;
    }

    private void Start()
    {
        groundPool = new ObjectPool(ObjectPool.PollType.GROUND , blocks[0],20);
    }

    public override void EnemyDeadNotify()
    {
        GenerateGround();
    }

    private void GenerateGround()
    {
        GameObject groundClone = groundPool.Pop();
        groundClone.transform.position = target.transform.position;
        groundClone.transform.parent = transform;
        groundClone.gameObject.SetActive(true);
        createCount++;

        if (createCount != 0 && createCount >= count)
        {
            GameObject.Instantiate(props[propsIndex], probsTarget.transform.position + Vector3.up, Quaternion.identity, transform);
            createCount = 0;
        }
        probsTarget.transform.position += Vector3.left * 2f;
        target.transform.position += Vector3.left * 2f;
        propsIndex++;
        if (propsIndex >= props.Count)
        {
            propsIndex = 0;
        }
    }

    public void PushPool(GameObject groundClone)
    {
        groundPool.Push(groundClone);
    }
}
