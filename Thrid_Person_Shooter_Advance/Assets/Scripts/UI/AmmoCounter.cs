using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] Text Text;
    PlayerShooter playerShooter;
    WeaponReloader Reloader;
    
    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoin += HandleOnLocalPlayerJoined;
    }

    private void HandleOnLocalPlayerJoined(Player player)
    {
        playerShooter = player.GetComponent<PlayerShooter>();
        playerShooter.OnWeponSwitch += HandleOnWeaponChange;
    }

    private void HandleOnWeaponChange(Bullet activeWeapon)                   // weapon Reloader
    {
        Reloader = activeWeapon.reloader;
        Reloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
    }

    private void HandleOnAmmoChanged()
    {
        int amountInInventory = Reloader.RoundRemainingInInventory;
        int amountInClip = Reloader.RoundRemainingClip;
        if(amountInInventory > 0)
            Text.text = (amountInClip + "/" + amountInInventory).ToString();
        else
            Text.text = "00/00";
    }

    void Update()
    {
        
    }
}
