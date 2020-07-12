using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AttackState : IState
{
    private Enemy parent;

    private float attackCooldown = 3;
    public void Enter(Enemy parent)
    {
        this.parent = parent;
        parent.Direction = Vector3.zero;
    }

    public void Update()
    {
        Vector3 tempdirec = parent.Target.position - parent.transform.position;
        parent.Animationmanager.AttackAnimation(tempdirec);
        if (parent.MyAttackTime >= attackCooldown && !parent.IsAttacking)
        {
            parent.Animationmanager.MyAnimator.SetTrigger("Attack");
            parent.MyAttackTime = 0;
            parent.StartCoroutine(Attack());
        }
        if (parent.Target != null){
            float distance = Vector2.Distance(parent.Target.position, parent.transform.position);

            if(distance >= parent.MyAttackRange)
            {
                parent.IsAttacking = false;
                parent.ChangeState(new FollowState());
            }
            //Attack
        } 
        else
        {
            parent.IsAttacking = false;
            parent.ChangeState(new IdleState());
        }
    }


    public IEnumerator Attack()
    {
        parent.IsAttacking = true;
        parent.Target.GetComponent<Player>().TakeDamage(parent.DamageAmount);
        Debug.Log("Attacked!!!");
        
        yield return new WaitForSeconds(parent.Animationmanager.MyAnimator.GetCurrentAnimatorStateInfo(2).length);
        parent.IsAttacking = false;

    }
    public void Exit()
    {
        
    }

}
