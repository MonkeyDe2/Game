using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EvadeState : IState
{
    private Enemy parent;

    private float nextWaypointDistance = 3f;

    private Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    private Seeker seeker;
    private float regen = 1f;
    private bool regenerating = false;
    public void Enter(Enemy parent)
    {
        this.parent = parent;
        seeker = parent.GetComponent<Seeker>();
    }

     public void Exit()
    {
        parent.Direction = Vector2.zero;
    }

    public void Update(){
        if (parent.InRange){
            parent.ChangeState(new FollowState());
        }
        if (seeker.IsDone()){
            seeker.StartPath(parent.transform.position, parent.OrigPosition, OnPathComplete);
        }
        if (path == null)
            return;
        parent.Direction = ((Vector3)path.vectorPath[currentWaypoint] - parent.transform.position).normalized;
        parent.transform.position = Vector2.MoveTowards(parent.transform.position, (Vector3)path.vectorPath[currentWaypoint], parent.CurrentSpeed * Time.deltaTime);
        float waypointDistance = Vector2.Distance(parent.transform.position, path.vectorPath[currentWaypoint]);
        if (waypointDistance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

        float distance = Vector2.Distance(parent.OrigPosition, parent.transform.position);      
        parent.heal();
        if(distance <= 1f)
            {
                parent.ChangeState(new IdleState());
            }     
        
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            //currentWaypoint = 0;
        }
    }

}
