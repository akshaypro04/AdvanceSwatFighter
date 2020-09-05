using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    float MinAngle;
    [SerializeField]
    float MaxAngle;

    public void SetRotation(float amount)
    {
        float clampAngle =  GetClampAngle(amount);
        transform.eulerAngles = new Vector3(clampAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private float GetClampAngle(float amount)
    {
        float newAngle = ChangeAngle(transform.eulerAngles.x - amount);
        float clampAngle = Mathf.Clamp(newAngle, MinAngle, MaxAngle);
        return clampAngle;
    }

    public float GetAngle()
    {
        return ChangeAngle(transform.eulerAngles.x);
    }

    public float ChangeAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;
        return angle + 180;
    }

}
