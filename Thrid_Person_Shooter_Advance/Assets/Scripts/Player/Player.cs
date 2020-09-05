using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStates))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }

    [SerializeField] SwatSoilder PlayerSetting;
    [SerializeField] AudioController footSteps;
    [SerializeField] public MouseInput MouseControl;
    [SerializeField] float minimalMovePosition;
    public PlayerAim playerAim;

    Vector3 perviousPosition;

    private CharacterController m_MoveController;
    public CharacterController MoveController
    {
        get
        {
            m_MoveController = GetComponent<CharacterController>();
            return m_MoveController;
        }
    }

    private PlayerStates m_PlayerState;
    public PlayerStates PlayerState
    {
        get
        {
            m_PlayerState = GetComponent<PlayerStates>();
            return m_PlayerState;
        }
    }

    private PlayerHealth m_playerHealth;
    public PlayerHealth playerHealth
    {
        get
        {
            m_playerHealth = GetComponent<PlayerHealth>();
            return m_playerHealth;
        }
    }

    InputControllers PlayerInput;
    Vector2 mouseInput;


    void Awake()
    {
        PlayerInput = GameManager.Instance.inputControllers;
        GameManager.Instance.LocalPlayer = this;
        Cursor.visible = false;
        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    void Update()
    {
        if (!playerHealth.IsAlive)
            return;

        PlayerMovement();
        LookAround();
    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, PlayerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, PlayerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    void PlayerMovement()
    {
        float speed = PlayerSetting.runSpeed;

        if (PlayerInput.IsWalking)
            speed = PlayerSetting.walkSpeed;

        if (PlayerInput.IsSprinting)
            speed = PlayerSetting.sprintSpeed;

        if (PlayerInput.IsCrouching)
            speed = PlayerSetting.crouchSpeed;

        if (PlayerInput.IsCrouchSprint)
            speed = PlayerSetting.runSpeed;

        Vector2 Direction = new Vector2(PlayerInput.Vertical * (speed), PlayerInput.Horizontal * (speed));
        MoveController.SimpleMove(transform.forward * Direction.x + transform.right * Direction.y);

        if (Vector3.Distance(transform.position, perviousPosition) > minimalMovePosition)
            footSteps.play();

        perviousPosition = transform.position;

    }
}
