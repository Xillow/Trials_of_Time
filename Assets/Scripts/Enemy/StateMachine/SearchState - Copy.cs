//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Pathfinding;

//public class SearchState : State
//{
    
//    public float nextWaypointDistance = 3f;
    
//    Path path;
//    int currentWaypoint = 0;
//    bool reachedEndofPath = false, crStarted = false;
//    //float distanceToLoc;

//    Seeker seeker;



//    public override void AwakeExt()
//    {
//        base.AwakeExt();
//        seeker = GetComponent<Seeker>();

        
//        //InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);

//    }

//    public override void FixedUpdateExt()
//    {
//        base.FixedUpdateExt();
        

//        //distanceToLoc = Vector2.Distance((Vector2)enemy.body.transform.position, enemy.soundPosition);
//        if (path == null)
//            return;

        

//        if (reachedEndofPath && !crStarted)
//        {
//            StartCoroutine(LostInterest());
//            crStarted = true;
//            return;

//        }

//        if (currentWaypoint >= path.vectorPath.Count)
//        {
//            reachedEndofPath = true;
//            return;
//        }
//        else
//        {
//            reachedEndofPath = false;
//            StopAllCoroutines();
//            crStarted = false;
            

//        }

//        //if (distanceToLoc > 0.05)
//        //{
//            enemy.direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)enemy.body.transform.position).normalized;
//            enemy.MoveEnemy();

//        //}

//        float distance = Vector2.Distance((Vector2)enemy.body.transform.position, (Vector2)path.vectorPath[currentWaypoint]);
        


//        if (distance < nextWaypointDistance)
//        {

//            currentWaypoint++;
//        }
    
//    }

//    public override void OnDisable()
//    {
//        base.OnDisable();
        
//    }

//    public override void OnEnable()
//    {
//        base.OnEnable();
//        enemy.newSound = false;
//        seeker.StartPath(enemy.body.transform.position, enemy.soundPosition, OnPathComplete);
//    }

//    void OnPathComplete(Path p)
//    {
//        if (!p.error)
//        {

//            path = p;
//            currentWaypoint = 0;
//        }


//    }

//    IEnumerator LostInterest()
//    {
//        yield return new WaitForSeconds(5);
//        enemy.interested = false;

//    }

    
    

////    void UpdatePath()
////    {
////        if(seeker.IsDone() && distanceToLoc > 0.5)
////        seeker.StartPath(enemy.body.transform.position, enemy.soundPosition, OnPathComplete);
////    }

//}
