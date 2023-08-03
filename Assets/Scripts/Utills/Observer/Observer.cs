
using UnityEngine;

public abstract class Observer :MonoBehaviour
{
    public virtual void FeverNotify() { }
    public virtual void FeverEndNotify() { }

    public virtual void EnemyHitNotify() { }
    public virtual void EnemyDeadNotify() {}

    public virtual void PlayerHitNotify() {}
    public virtual void EndNotify() { }
}
