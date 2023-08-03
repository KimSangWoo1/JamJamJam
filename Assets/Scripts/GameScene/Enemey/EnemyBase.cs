using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // SO
    public Enemy enemy;
    [SerializeField]
    private EnemyChannel enemyChannel;

    // Serial Component
    [SerializeField]
    protected EnemyHpControl enemyHpControl;
    // Serial Variable
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    protected int hp;

    // Component
    protected Animator ani;
    // Variable
    private Vector3 movePos;
    private bool move;

    // Properties
    public int Hp => hp;

    //
    protected readonly WaitForSeconds wait = new WaitForSeconds(1f);

    #region MonoBehaviour
    private void OnEnable()
    {
        enemyChannel.OnRequested += MoveRequestd;
    }

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        enemyChannel.OnRequested -= MoveRequestd;
    }

    private void Update()
    {
        FrontMove();
    }
    #endregion

    #region Move
    protected void FrontMove()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * moveSpeed);
        }

        if(transform.position == movePos)
        {
            move = false;
        }
    }

    private void MoveRequestd()
    {
        movePos = transform.position + transform.forward * 2f;
        move = true;
    }
    #endregion

    public void SetHp(int amount)
    {
        hp = amount;
        enemyHpControl.SetHP(amount);
    }
}
