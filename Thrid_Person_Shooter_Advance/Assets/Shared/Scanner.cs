using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField] float scanSpeed;
    [Range(0,360)]
    [SerializeField] float fieldOfView;
    [SerializeField] LayerMask mask;
    SphereCollider rangeTriger;

    public float ScanRange
    {
        get
        {
            if (rangeTriger == null)
                rangeTriger = GetComponent<SphereCollider>();
            return rangeTriger.radius;
        }
    }


    public event System.Action OnScanReady;


    void PrepareScan()
    {
        GameManager.Instance.Timer.add(()=>{
            if (OnScanReady != null)
                OnScanReady();
        }, scanSpeed);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(fieldOfView / 2) * GetComponent<SphereCollider>().radius);
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent<SphereCollider>().radius);

    }

    Vector3 GetViewAngle(float angle)
    {
        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    public List<T> ScanForTarget<T>()
    {
        List<T> targets = new List<T>();
        Collider[] results = Physics.OverlapSphere(transform.position, ScanRange);

        for(int i = 0; i < results.Length; i++)
        {
            var player = results[i].transform.GetComponent<T>();

            if (player == null)
                continue;

            if (!IsInLineOfSight(Vector3.up*5f, results[i].transform.position)){
                continue;
            }

            targets.Add(player);
        }
        PrepareScan();
        return targets;
    }

    bool IsInLineOfSight(Vector3 eyeHeight, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        if (Vector3.Angle(transform.forward, direction.normalized) < fieldOfView / 2)
        {
            float distanctionToTarget = Vector3.Distance(transform.position, targetPosition);
            print("find out");
            // something blocking our view
            if (Physics.Raycast(transform.position + eyeHeight + transform.forward*0.3f, direction.normalized, distanctionToTarget, mask))
                return false;
                
            return true;
        }
        return false;
    }
}
