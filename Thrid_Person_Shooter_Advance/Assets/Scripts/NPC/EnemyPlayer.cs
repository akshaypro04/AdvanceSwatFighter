using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyStates))]
public class EnemyPlayer : MonoBehaviour
{
    PathFinder pathFinder;
    [SerializeField]Scanner Playerscanner;
    [SerializeField] SwatSoilder PlayerSetting;

    Player priorityTarget;
    List<Player> myTarget;

    public event System.Action<Player> OnTergetSelected;

    private EnemyHealth m_enemyHealth;
    public EnemyHealth enemyHealth
    {
        get
        {
            if (m_enemyHealth == null)
                m_enemyHealth = GetComponent<EnemyHealth>();
            return m_enemyHealth;
        }
    }

    private EnemyStates m_enemyState;
    public EnemyStates enemyState
    {
        get
        {
            if (m_enemyState == null)
                m_enemyState = GetComponent<EnemyStates>();
            return m_enemyState;
        }
    }


    void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        Playerscanner.OnScanReady += Scanner_OnScanReady;
        Scanner_OnScanReady();
        pathFinder.agent.speed = PlayerSetting.walkSpeed;

        enemyHealth.OnDeath += EnemyHealth_OnDeath;
        enemyState.OnModeChange += EnemyState_OnModeChange;

    }

    private void EnemyState_OnModeChange(EnemyStates.Emode Mode)
    {
        pathFinder.agent.speed = PlayerSetting.walkSpeed;

        if (Mode == EnemyStates.Emode.AWARE)
        {
            pathFinder.agent.speed = PlayerSetting.runSpeed;
        }
    }

    private void EnemyHealth_OnDeath()
    {

    }

    private void Scanner_OnScanReady()
    {
        if (priorityTarget != null)
            return;

        myTarget = Playerscanner.ScanForTarget<Player>();

        if (myTarget.Count == 1)
            priorityTarget = myTarget[0];
        else
            SelectClosestTarget();

        if (priorityTarget != null)
        {
            if(OnTergetSelected != null)
            {
                OnTergetSelected(priorityTarget);
            }
        }



    }

    private void SelectClosestTarget()
    {
        float closestTarget = Playerscanner.ScanRange;
        foreach(var possibleTarget in myTarget)
        {
            if(Vector3.Distance(transform.position, possibleTarget.transform.position) > closestTarget)
            {
                priorityTarget = possibleTarget;
            }
        }
    }

    void Update()
    {
        if (priorityTarget == null)
            return;

        transform.LookAt(priorityTarget.transform.position);
    }
}
