using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPicks : PickUpItem
{
    [SerializeField] EWeaponType eWeaponType;
    [SerializeField] float respawnTime;
    [SerializeField] int AmmoAmount;

    public override void OnPickUp(Transform item)
    {
        var playerInventory = item.GetComponentInChildren<Container>();
        GameManager.Instance.ReSpawner.DeSpawn(gameObject, respawnTime);
        playerInventory.Put(eWeaponType.ToString(), AmmoAmount);
        item.GetComponent<PlayerShooter>().activeWeapon.reloader.HandleOnAmmoChanged();
    }

}
