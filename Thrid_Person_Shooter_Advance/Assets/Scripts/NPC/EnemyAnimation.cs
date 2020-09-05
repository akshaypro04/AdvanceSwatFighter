using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    Vector3 lastPosition;

    PathFinder pathFinder;

    EnemyPlayer enemyPlayer;
    

    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer = GetComponent<EnemyPlayer>();
    }

    void Update()
    {
        float Velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("IsWalking", enemyPlayer.enemyState.CurrentMode == EnemyStates.Emode.UNAWARE);
        animator.SetFloat("VerticalSet", Velocity/pathFinder.agent.speed);
    }

}
