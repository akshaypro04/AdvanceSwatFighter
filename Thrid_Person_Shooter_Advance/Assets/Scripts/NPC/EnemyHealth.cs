using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Destroyable
{
    [SerializeField]
    RagDoll ragDoll;

    public override void Die()
    {
        base.Die();
        ragDoll.EnableRagdoll(true);
        GameManager.Instance.EventBus.RaiseEvent("EnenyDeath");
    }

}
