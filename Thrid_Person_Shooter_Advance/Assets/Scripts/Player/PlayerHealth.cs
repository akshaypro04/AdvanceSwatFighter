using UnityEngine;

public class PlayerHealth : Destroyable
{

    //[SerializeField] SpawnPoint[] spawnPoints;

    [SerializeField]RagDoll ragDoll;
    public override void Die()
    {
        base.Die();
        ragDoll.EnableRagdoll(true);
    }

    //void SpawnAtNewSpawnPoint()
    //{
    //    ragDoll.EnableRagdoll(false);
    //    int spawnIndex = Random.Range(0, spawnPoints.Length);
    //    print(spawnPoints.Length);
    //    transform.position = spawnPoints[spawnIndex].transform.position;
    //    transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    //}

    //[ContextMenu("Test Die")]
    //void TestDie()
    //{
    //    Die();
    //}


}
