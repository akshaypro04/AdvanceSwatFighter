using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float TimeToLive;
    [SerializeField] float Damge;
    [SerializeField] Transform BulletHole;

    Vector3 Destination;

    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    void Update()
    {
        if (IsDestinationReached())
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Destination != Vector3.zero)           
            return;

        RaycastHit Hit;
        if(Physics.Raycast(transform.position,transform.forward,out Hit, 5f)){
            CheckDestructable(Hit);
        }
    }

    void CheckDestructable(RaycastHit hitInfo)                     // Instantiate & takeDamager     1
    {
        var destructable = hitInfo.transform.GetComponent<Destroyable>();

        Destination = hitInfo.point + hitInfo.normal * 0.01f;                

        Transform hole =  (Transform)Instantiate(BulletHole, Destination, Quaternion.LookRotation(hitInfo.normal)*Quaternion.Euler(0,180,0));
        hole.SetParent(hitInfo.transform);

        if (destructable == null)
            return;

        destructable.TakeDamage(Damge);
    }
       

    bool IsDestinationReached()                                  //  2
    {
        if (Destination == Vector3.zero)
            return false;

        Vector3 directiontoDestination = Destination - transform.position;
        float dot = Vector3.Dot(directiontoDestination, transform.forward);          // multipiaction of 2 vector == 0

        if (dot < 0)
            return true;

        return false;

    }
}
