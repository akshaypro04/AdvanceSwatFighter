using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{

    public enum Emode
    {
        AWARE,
        UNAWARE
    }

    private Emode m_CurrentMode;
    public Emode CurrentMode
    {
        get
        {
            return m_CurrentMode;
        }
        set
        {
            if (m_CurrentMode == value)
                return;

            m_CurrentMode = value;
            if (OnModeChange != null)
                OnModeChange(value);
            
        }
    }


    public event System.Action<Emode> OnModeChange;


}
