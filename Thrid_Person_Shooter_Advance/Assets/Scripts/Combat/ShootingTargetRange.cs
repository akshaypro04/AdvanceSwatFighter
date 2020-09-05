using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetRange : Destroyable
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float repairTime;

    Quaternion initalRotation;
    Quaternion targetRotation;
    bool requiresRotation;

    void Awake()
    {
        initalRotation = transform.rotation;
    }

    public override void Die()
    {
        base.Die();
        targetRotation = Quaternion.Euler(transform.right * 80);
        requiresRotation = true;
        GameManager.Instance.Timer.add(() =>
        {
            targetRotation = initalRotation;
            requiresRotation = true;
        },repairTime);
    }

    void Update()
    {
        if (!requiresRotation)
            return;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.rotation == targetRotation)
            requiresRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GunsBullet")
        {
            print("collide");
        }
    }

}
