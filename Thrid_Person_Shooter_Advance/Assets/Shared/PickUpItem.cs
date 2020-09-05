using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player")
            return;

        pickUp(collider.transform);
    }

    public virtual void OnPickUp(Transform item)
    {
        // now for now
    }

    void pickUp(Transform item)
    {
        OnPickUp(item);
    }
}
