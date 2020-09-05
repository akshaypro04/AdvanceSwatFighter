using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform Hand;
    [SerializeField] AudioController AudioShoot;
    [SerializeField] AudioController AudioReload;
    [SerializeField] AudioController AudioOutOfAmmo;
    [SerializeField] int TakeBulletAtTime; 
    [HideInInspector] public WeaponReloader reloader;

    public Transform aimTraget;
    public Vector3 AimTargetOfFset;

    public ParticleSystem muzzleParticalSystem;


    float nextFireAllowed;
    Transform muzzle;

    public bool canFire;
    
    void Awake()
    {
        muzzle = transform.Find("Model/MuzzlesGameObject");
        reloader = GetComponent<WeaponReloader>();
        muzzleParticalSystem = muzzle.GetComponent<ParticleSystem>();
    }

     public void Equip()
     {
        transform.SetParent(Hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }


    void FireEffect()
    {
        if (muzzleParticalSystem == null)
            return;

        muzzleParticalSystem.Play();
    }


    public void Reload()                                // assult Rifler
    {
        if (reloader == null)
            return;
        reloader.Reload();
        AudioReload.play();
    }

    public virtual void fire()
    {
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundRemainingClip == 0)
            {
                AudioOutOfAmmo.play();
                return;
            }
            else
            {
                reloader.TakeFromClip(TakeBulletAtTime);                          //take 1 bullet from clip
            }
        }
        
        nextFireAllowed = Time.time + rateOfFire;

        muzzle.LookAt(aimTraget.position + AimTargetOfFset);
        FireEffect();
        foreach (Transform muzzleTransform in muzzle.GetComponentsInChildren<Transform>())
        {
            Instantiate(projectile, muzzleTransform.position, muzzleTransform.rotation);
        }

        
        AudioShoot.play();
        canFire = true;
    }


}
