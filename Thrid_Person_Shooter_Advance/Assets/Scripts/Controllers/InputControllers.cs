using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllers : MonoBehaviour
{
    [HideInInspector] public float Vertical;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public Vector2 MouseInput;
    [HideInInspector] public bool Fire1;
    [HideInInspector] public bool Fire2;
    [HideInInspector] public bool Reload;
    [HideInInspector] public bool IsSprinting;
    [HideInInspector] public bool IsWalking;
    [HideInInspector] public bool IsCrouching;
    [HideInInspector] public bool IsCrouchSprint;
    [HideInInspector] public bool MouseWheelUp;
    [HideInInspector] public bool MouseWheelDown;


    void Start()
    {
        
    }

    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        Reload = Input.GetKey(KeyCode.R);
        IsCrouching = Input.GetKey(KeyCode.C);
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
        IsWalking = Input.GetKey(KeyCode.X);
        IsCrouchSprint = IsCrouching && IsSprinting;
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
    }
}
