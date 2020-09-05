using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerAim m_PlayerAim;

    public PlayerAim playerAim
    {
        get
        {
            if (m_PlayerAim == null)
                m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            return m_PlayerAim;
        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        animator.SetFloat("VerticalSet", GameManager.Instance.inputControllers.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.inputControllers.Horizontal);
        animator.SetBool("IsWalking", GameManager.Instance.inputControllers.IsWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.inputControllers.IsSprinting);
        animator.SetBool("IsCrouching", GameManager.Instance.inputControllers.IsCrouching);
        animator.SetFloat("AimAngle", playerAim.GetAngle());
        animator.SetBool("IsAiming", GameManager.Instance.inputControllers.Fire2);
    }
}
