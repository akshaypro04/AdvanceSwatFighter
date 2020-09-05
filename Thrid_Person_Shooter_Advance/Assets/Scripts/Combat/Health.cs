using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destroyable
{
    [SerializeField] float InSeconds;

    public override void Die()
    {
        base.Die();
        print("WE DIE");
        GameManager.Instance.ReSpawner.DeSpawn(gameObject, InSeconds);
    }

    void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount); 
    }


}
