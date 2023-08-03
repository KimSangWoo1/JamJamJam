using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CamControl : Observer
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float moveSpeed;

    Vector3 movePos;

    private void Start()
    {
        movePos = transform.position;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, movePos, Time.smoothDeltaTime * moveSpeed);
    }

    public override void EnemyDeadNotify()
    {
        movePos = transform.position + transform.forward * 2f;
    }
}
