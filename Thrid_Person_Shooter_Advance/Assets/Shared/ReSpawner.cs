using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawner : MonoBehaviour
{
    GameObject go;

    public ReSpawner(GameObject go)
    {
        this.go = go;
    }


    public void DeSpawn(GameObject go, float InSeconds)
    {
        go.SetActive(false);
        GameManager.Instance.Timer.add(()=> {
            print("respawn   ");
            go.SetActive(true);
        }, InSeconds);
    }
}
