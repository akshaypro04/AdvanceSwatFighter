using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Bullet[] weapons;

    [HideInInspector]public bool canFire;

    public int WeaponSwitchTime;

    Transform weaponHolster;

    int currentWeaponIndex;

    public event System.Action<Bullet> OnWeponSwitch;

    Bullet m_activeWeapon;

    public Bullet activeWeapon
    {
        get
        {
            return m_activeWeapon;
        }
        set
        {
            m_activeWeapon = value;
        }
    }

    [System.Obsolete]
    void Awake()
    {
        canFire = true;
        weaponHolster = transform.FindChild("Weapons");
        weapons = weaponHolster.GetComponentsInChildren<Bullet>();
        if (weapons.Length > 0)
            Equip(0);
    }

    internal void SwitchWeapon(int Direction)
    {
        deactivateWeapon();
        canFire = false;
        currentWeaponIndex += Direction;
        if (currentWeaponIndex > weapons.Length - 1)
            currentWeaponIndex = 0;

        if (currentWeaponIndex < 0)
            currentWeaponIndex = weapons.Length - 1;
        GameManager.Instance.Timer.add(() =>
        {
            Equip(currentWeaponIndex);
        }, WeaponSwitchTime);
    }

    internal void Equip(int index)
    {
        deactivateWeapon();
        canFire = true;
        activeWeapon = weapons[index];
        activeWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        if (OnWeponSwitch != null)
            OnWeponSwitch(activeWeapon);
    }

    void deactivateWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolster);                   // all are gose to player weapon child
        }

    }
}
