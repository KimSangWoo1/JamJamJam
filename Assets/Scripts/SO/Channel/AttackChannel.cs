using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AttackChannel", menuName = "Assets/SO/Channel/AttackChannel", order =1)]
public class AttackChannel : ScriptableObject
{
    public UnityAction<AttackType> attackRequested;

    public UnityAction<GameObject> plyerAttackRequested;

    public void AttackEvent(AttackType attack)
    {
        if(attackRequested != null)
        {
            attackRequested.Invoke(attack);
        }
        else
        {
            Debug.LogWarning("attackRequested Null");
        }
    }
}
