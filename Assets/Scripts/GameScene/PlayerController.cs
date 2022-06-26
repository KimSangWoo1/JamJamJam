using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private AttackChannel attackChannel;
    [SerializeField]
    private float moveSpeed;

    // Component
    private Animator ani;


    // Variable
    private bool attack;
    private bool back;
    [SerializeField]
    private bool combo;

    private Vector3 startPosition;
    private Vector3 attackPos;


    public GameObject target;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attackChannel.plyerAttackRequested += PlayerAttack;    
    }

    void Start()
    {
        startPosition = transform.position;    
    }

    private void Update()
    {
        PlayerMove();
    }

    private void OnDisable()
    {
        attackChannel.plyerAttackRequested -= PlayerAttack;
    }
    
    private void PlayerMove()
    {
        if (combo)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                attack = false;
                combo = false;
                Attack(attack);
                Combo(combo);
                back = true;
            }
        }
        else if (attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackPos, Time.deltaTime * moveSpeed);

            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1")  && ani.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.7f)
            {
                attack = false;
                Attack(attack);
                back = true;
            }
        }
        else if (back)
        {
            if(transform.position == startPosition)
            {
                back = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * moveSpeed *2f);
            }
        }
    }
    
    #region Ani
    private void Attack(bool check)
    {
        ani.SetBool("Attack", check);
    }

    private void Combo(bool check)
    {
        ani.SetBool("Combo", check);
    }
    #endregion

    #region Channel
    // Channel
    public void PlayerAttack(GameObject enemy)
    {
        attackPos = enemy.transform.position + enemy.transform.forward;
        attackPos.y = transform.position.y;
        if (attack)
        {
            combo = true;
            Combo(combo);
        }
        else
        {
            attack = true;
            Attack(attack);
        }
    }
    #endregion
}
