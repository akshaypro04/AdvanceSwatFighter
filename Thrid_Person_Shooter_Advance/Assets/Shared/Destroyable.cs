using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destroyable : MonoBehaviour            // multiplayer script
{
    [SerializeField] float HitPoint;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken;

    public  float HitPointRemaining
    {
        get
        {
            return HitPoint - damageTaken;
        }
    }

    public bool IsAlive
    {
        get
        {
            return HitPointRemaining > 0;
        }
    }


    public virtual void TakeDamage(float amount)            // call bullet 
    {
        damageTaken += amount;

        if (OnDamageReceived != null)
            OnDamageReceived();                              // event

        if (HitPointRemaining <= 0)
            Die();
    }

    public virtual void Die()
    {
        if (OnDeath != null)
            OnDeath();                                      // event
    }

    public void Reset()
    {
        damageTaken = 0;
    }


}
