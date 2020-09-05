using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum EMoveStates
    {
        WALKING,
        RUNNING,
        CROUCHING,
        SPRINTING
    }

    public enum EWeaponState
    {
        IDEL,
        FIRING,
        AIMING,
        AIMINGFIREING
    }

    public EMoveStates MoveState;
    public EWeaponState WeaponState;

    private InputControllers m_InputController;
    public InputControllers InputControllers
    {
        get
        {
            if (m_InputController == null)
                m_InputController = GameManager.Instance.inputControllers;
            return m_InputController;

        }
    }


    void Update()
    {
        SetMoveState();
        SetWeaponState();
    }

    void SetMoveState()
    {
        MoveState = EMoveStates.RUNNING;

        if (InputControllers.IsWalking)
            MoveState = EMoveStates.WALKING;

        if (InputControllers.IsCrouching)
            MoveState = EMoveStates.CROUCHING;

        if (InputControllers.IsSprinting)
            MoveState = EMoveStates.SPRINTING;

    }

    void SetWeaponState()
    {
        WeaponState = EWeaponState.IDEL;

        if (InputControllers.Fire1)
            WeaponState = EWeaponState.FIRING;

        if (InputControllers.Fire2)
            WeaponState = EWeaponState.AIMING;

        if (InputControllers.Fire1 && InputControllers.Fire2)
            WeaponState = EWeaponState.AIMINGFIREING;
    }
}
