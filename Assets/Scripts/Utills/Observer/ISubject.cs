using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject 
{
    void FeverNotify();
    void FeverEndNotify();

    void EnemyHitNotify();
    void EnemyDeadNotify();

    void PlayerHitNotify();

    void EndNotify();
}
