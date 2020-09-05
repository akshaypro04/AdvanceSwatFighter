using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigdoll_Test : Destroyable
{
    private Rigidbody[] bodyparts;
    public Animator anim;
    [SerializeField] SpawnPoint[] spawnPoints;


    void Start()
    {
        bodyparts = transform.GetComponentsInChildren<Rigidbody>();
        enableRagdoll(false);
    }

    public override void Die()
    {
        base.Die();
        enableRagdoll(true);
        anim.enabled = false;
        GameManager.Instance.Timer.add(() =>
        {
            SpawnAtNewSpawnPoint();
            anim.enabled = true;
            Reset();
        }, 5f);
    }

    void Update()
    {
        if (!IsAlive)
            return;
    }

    void enableRagdoll(bool value)
    {
        for(int i = 0; i < bodyparts.Length; i++)
        {
            bodyparts[i].isKinematic = !value;
        }
    }

    void SpawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }
}
