using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController
{
    [SerializeField] float shootingSpeed;

    [SerializeField] float burstDurationMin;

    [SerializeField] float burstDurationMax;

    bool ShootFire;

    EnemyPlayer enemyPlayer;

    void Start()
    {
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTergetSelected += EnemyPlayer_OnTergetSelected;

    }

    private void EnemyPlayer_OnTergetSelected(Player target)
    {
        activeWeapon.aimTraget = target.transform;
        activeWeapon.AimTargetOfFset = Vector3.up * 1;
        StartBurst();
    }

    void StartBurst()
    {
        if (!enemyPlayer.enemyHealth.IsAlive)
            return;

        CheckReload();
        ShootFire = true;

        GameManager.Instance.Timer.add(EndBurst,Random.Range(burstDurationMax,burstDurationMin));
    }

    void EndBurst()
    {
        ShootFire = false;
        if (!enemyPlayer.enemyHealth.IsAlive)
            return;

        GameManager.Instance.Timer.add(StartBurst, shootingSpeed);

    }

    void CheckReload()
    {
        if (activeWeapon.reloader.RoundRemainingClip == 0)
            activeWeapon.Reload();

    }

    void Update()
    {
        if (!ShootFire || !canFire || !enemyPlayer.enemyHealth.IsAlive)
            return;

        activeWeapon.fire();
    }
}
