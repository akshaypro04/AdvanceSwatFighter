using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [System.Serializable]
    public class CameraRig
    {
        public Vector3 CameraOffset;
        public float damping;
        public float CrouchHeight;
    }

    [SerializeField] CameraRig DefaultCamera;
    [SerializeField] CameraRig AimCamera;

    Transform CameraLookTarget;
    Player LocalPlayer;

    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoin += HandleOnLocalPlayerJoin;
    }

    private void HandleOnLocalPlayerJoin(Player player)
    {
        LocalPlayer = player;
        CameraLookTarget = LocalPlayer.transform.Find("AimingPrivot");

        if (CameraLookTarget == null)
            CameraLookTarget = LocalPlayer.transform;
    }

    void LateUpdate()
    {
        CameraRig cameraRig = DefaultCamera;

        if (LocalPlayer.PlayerState.WeaponState == PlayerStates.EWeaponState.AIMINGFIREING || LocalPlayer.PlayerState.WeaponState == PlayerStates.EWeaponState.AIMING)
            cameraRig = AimCamera;

        float CrouchHeight = cameraRig.CameraOffset.y + (LocalPlayer.PlayerState.MoveState == PlayerStates.EMoveStates.CROUCHING ? cameraRig.CrouchHeight : 0);

        Vector3 TargetPosition = CameraLookTarget.position + LocalPlayer.transform.forward * cameraRig.CameraOffset.z +
                                                             LocalPlayer.transform.up * (cameraRig.CameraOffset.y + CrouchHeight) +
                                                             LocalPlayer.transform.right * cameraRig.CameraOffset.x;


        transform.position = Vector3.Lerp(transform.position, TargetPosition, cameraRig.damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, CameraLookTarget.rotation, cameraRig.damping * Time.deltaTime);
    
    }
}
