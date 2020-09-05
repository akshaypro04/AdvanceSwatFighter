using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;                               // size of mazzling fixed
    [SerializeField] Container Inventory;
    [SerializeField] EWeaponType eWeaponType;

    public int shotsFiredClip;
    bool isReloading;
    System.Guid containerItemId;
    bool AddItemToInventory = true;
    public event System.Action OnAmmoChanged;

    public int RoundRemainingClip                               // left bullets in clips
    {
        get
        {
            return clipSize - shotsFiredClip;
        }
    }

    public int RoundRemainingInInventory                               // left bullets in clips
    {
        get
        {
            return Inventory.GetAmountRemaining(containerItemId);
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
        set
        {
            isReloading = value;
        }
    }

    void Awake()
    {
        Inventory.OnContainerReady += () =>
        {  
           containerItemId = Inventory.Add(eWeaponType.ToString(), maxAmmo);
           AddItemToInventory = false;
        };

    }

    void OnEnable()
    {
        if(AddItemToInventory == true)
            containerItemId = Inventory.Add(eWeaponType.ToString(), maxAmmo);

        AddItemToInventory = false;
    }


    public void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
        GameManager.Instance.Timer.add(()=> 
        {
            ExecuteReload(Inventory.TakeFromContainer(containerItemId, clipSize - RoundRemainingClip));
        }, reloadTime);
    }

    private void ExecuteReload(int amount)
    {
        isReloading = false;
        shotsFiredClip -= amount;
        HandleOnAmmoChanged();
    }

    public void TakeFromClip(int amount)
    {
        shotsFiredClip += amount;                                    // numbers of bullets shots
        HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        if (OnAmmoChanged != null)
            OnAmmoChanged();                                         // if gun change in game
    }

}

