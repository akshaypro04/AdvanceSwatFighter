using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] Texture2D Image;
    [SerializeField] int size;

    void OnGUI()
    {
        if(!(GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerStates.EMoveStates.SPRINTING))
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUI.DrawTexture(new Rect(screenPosition.x -size/2, screenPosition.y -size/2, size, size), Image);
        }


    }

 
}
