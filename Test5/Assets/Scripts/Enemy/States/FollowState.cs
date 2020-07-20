using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

class FollowState : IState
{
    private Enemy parent;
    private float nextWaypointDistance = 3f;

    private Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    private Seeker seeker;


    public void Enter(Enemy parent)
    {
        this.parent = parent;

        seeker = parent.GetComponent<Seeker>();

        
    }

     public void Exit()
    {

    }

    public void Update(){
        if (!parent.InRange){
            parent.ChangeState(new EvadeState());
        } else {
            if (seeker.IsDone()){
            seeker.StartPath(parent.transform.position, parent.Target.transform.position, OnPathComplete);
            }
            if (path == null)
                return;
            
            FixedUpdate();
            
            //parent.transform.position = Vector2.MoveTowards(parent.transform.position, (Vector3)path.vectorPath[currentWaypoint], parent.CurrentSpeed * Time.deltaTime);

            float waypointDistance = Vector2.Distance(parent.transform.position, path.vectorPath[currentWaypoint]);

            if (waypointDistance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            float distance = Vector2.Distance(parent.Target.position, parent.transform.position);
            if(distance <= parent.MyAttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }
        
    }

    public void FixedUpdate()
    {
        if (parent.InRange){
            if (path == null)
                return;
                parent.Direction = ((Vector3)path.vectorPath[currentWaypoint] - parent.transform.position).normalized;
                Vector3 force = parent.Direction * parent.CurrentSpeed * Time.deltaTime;

                parent.MyRigidBody.AddForce(force);
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