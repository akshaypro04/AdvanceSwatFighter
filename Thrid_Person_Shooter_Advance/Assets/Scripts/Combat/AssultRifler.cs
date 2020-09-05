using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifler : Bullet
{
    public override void fire()
    {
        base.fire();
        if (canFire)
        {
            // we can fire
        }

    }

    public void Update()
    {
        //transform.Rotate(Vector3.right * GameManager.Instance.inputControllers.MouseInput.x * GameManager.Instance.LocalPlayer.MouseControl.Sensitivity.x);
        if (GameManager.Instance.inputControllers.Reload)
            Reload();
    }
}
