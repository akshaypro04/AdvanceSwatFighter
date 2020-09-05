using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public event System.Action<Player> OnLocalPlayerJoin;

    private GameObject gameObject;

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_GameManager");
                m_Instance.gameObject.AddComponent<InputControllers>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<ReSpawner>();
            }
            return m_Instance;
        }
    }

    private Timer m_Timer;
    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    private ReSpawner m_ReSpawner;
    public ReSpawner ReSpawner
    {
        get
        {
            if (m_ReSpawner == null)
                m_ReSpawner = gameObject.GetComponent<ReSpawner>();
            return m_ReSpawner;
        }
    }


    private InputControllers m_inputControllers;
    public InputControllers inputControllers
    {
        get
        {
            if (m_inputControllers == null)
                m_inputControllers = gameObject.GetComponent<InputControllers>();
            return m_inputControllers;
        }
    }

    private Player m_LocalPlayer;
    public Player LocalPlayer
    {
        get { 
            return m_LocalPlayer;
            }
        set { 
            m_LocalPlayer = value;
            if (OnLocalPlayerJoin != null)
                OnLocalPlayerJoin(m_LocalPlayer);
            }
    }

    private EventBus m_eventBus;
    public EventBus EventBus
    {
        get
        {
            if (m_eventBus == null)
                m_eventBus = new EventBus();
            return m_eventBus;
        }
    }

}
