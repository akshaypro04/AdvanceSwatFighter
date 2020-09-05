using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShooter : WeaponController
{
    bool IsPlayerAlive;

    void Start()
    {
        IsPlayerAlive = true;
        GetComponent<Player>().playerHealth.OnDeath += PlayerHealth_OnDeath;
    }

    private void PlayerHealth_OnDeath()
    {
        IsPlayerAlive = false;
    }

    void Update()
    {
        if (!IsPlayerAlive)
            return;

        if (GameManager.Instance.inputControllers.MouseWheelDown)
            SwitchWeapon(1);

        if (GameManager.Instance.inputControllers.MouseWheelUp)
            SwitchWeapon(-1);

        if (GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerStates.EMoveStates.SPRINTING)
            return;

        if (!canFire)
            return;

        if (GameManager.Instance.inputControllers.Fire1)
            activeWeapon.fire();
    }

}
