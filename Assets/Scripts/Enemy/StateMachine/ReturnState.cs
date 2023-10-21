using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ReturnState : State

{
    public float nextWaypointDistance = 3f;

    Path path;
    [SerializeField]
    int currentWaypoint = 0;
    [SerializeField]
    bool reachedEndofPath = false;
    public GameObject startingPos;

    Seeker seeker;



    public override void AwakeExt()
    {
        base.AwakeExt();
        seeker = GetComponent<Seeker>();

        seeker.StartPath(enemy.body.transform.position, startingPos.transform.position, OnPathComplete);

    }

    public override void FixedUpdateExt()
    {
        base.FixedUpdateExt();
        if (path == null)
            return;


        if (reachedEndofPath)
        {
            enemy.patroling = true;
            return;

        }

        if (currentWaypoint >= path.vectorPath.Count)
        {

            reachedEndofPath = true;
            return;

        }
        else
        {
            reachedEndofPath = false;
        }
                
        enemy.MoveEnemy(((Vector2)path.vectorPath[currentWaypoint] - (Vector2)enemy.body.transform.position).normalized);

        float distance = Vector2.Distance((Vector2)enemy.body.transform.position, (Vector2)path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {

            currentWaypoint++;
        }
          



    }

    public override void OnDisable()
    {
        base.OnDisable();
        
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {

            path = p;
            currentWaypoint = 0;
        }


    }


}
