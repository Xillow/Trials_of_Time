using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public PatrolPath patrolPath;

    [SerializeField]
    private bool isWaiting = false;
    [SerializeField]
    Vector2 currentPatrolTarget = Vector2.zero;
    bool isInitialized = false;

    private int currentIndex = -1;


    public override void AwakeExt()
    {
        base.AwakeExt();
        if (patrolPath == null)
            patrolPath = GetComponentInChildren<PatrolPath>();
    }

    public override void FixedUpdateExt()
    {
        base.FixedUpdateExt();
        if (!isWaiting)
        {
            if (patrolPath.Length < 2)
                return;
            if (!isInitialized)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(enemy.body.transform.position);
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInitialized = true;
            }
                     
            enemy.MoveEnemy((currentPatrolTarget - (Vector2)enemy.body.transform.position).normalized);

            if (Vector2.Distance(enemy.body.transform.position, currentPatrolTarget) < enemy.EnemyInfo.arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }

        }

    }

    public override void OnDisable()
    {
        base.OnDisable();
        enemy.patroling = false;
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(enemy.EnemyInfo.waitTime);
        var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
        currentPatrolTarget = nextPathPoint.Position;
        currentIndex = nextPathPoint.Index;
        isWaiting = false;
    }

}

