using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    // Component
    private Animator ani;

    // Variable
    private bool attack;
    private bool forward;
    private bool back;

    private Vector3 startPosition;
    private Vector3 attackPos;
    private Vector3 movePos;

    //State
    [SerializeField]
    private int hp = 3;

    // Properties
    public int Hp => hp;
    public bool Attacking => attack;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        hp = 3;
    }

    void Start()
    {
        startPosition = transform.position;    
    }

    private void Update()
    {
        PlayerMove();
        PlayerAttack();
    }

    private void PlayerAttack()
    {
        if (attack)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsTag("Attack1") || ani.GetCurrentAnimatorStateInfo(0).IsTag("Attack2") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                attack = false;
                back = true;
                Attack(attack);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, attackPos, Time.deltaTime * moveSpeed);

                back = false;
            }
        }
    }

    private void PlayerMove()
    {
        if(forward || back)
        {
            if(forward) startPosition = movePos;

            if (transform.position == startPosition)
            {
                forward = false;
                back = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * moveSpeed * 2f);
            }
        }
    }

    public void ForwardMove(Transform enemy)
    {
        movePos = enemy.transform.position + enemy.transform.forward;
        movePos.y = transform.position.y;
        forward = true;
    }

    public void backMove()
    {
        back = true;
    }

    public void AttackEnmey(GameObject enemy)
    {
        attackPos = enemy.transform.position + enemy.transform.forward;
        attackPos.y = transform.position.y;
        attack = true;
        Attack(attack);
    }

    #region Ani
    private void Attack(bool check)
    {
        ani.SetBool("Attack", check);
    }

    public void Hit()
    {
        hp--;
        ani.SetTrigger("Hit");
    }

    public void Dead()
    {
        ani.SetTrigger("Dead");
    }

    public void Fever(bool check)
    {
        ani.SetBool("Fever",check);
    }
    #endregion

}
