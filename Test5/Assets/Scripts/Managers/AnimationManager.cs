using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    public Animator MyAnimator
    {
      get
      {
        return animator;
      }
      set
      {
        animator = value;
      }
    }

    void Start(){
      animator = gameObject.GetComponent<Animator>();
    }

    public void IdleAnimation(Vector3 Direction){
      ActivateLayer("Idle");
      animator.SetFloat("x", Direction.x);
      animator.SetFloat("y", Direction.y);
    }

    public void WalkAnimation(Vector3 Direction){
      if (Direction.x != 0 || Direction.y !=0){
        ActivateLayer("Walk");
        animator.SetFloat("x", Direction.x);
        animator.SetFloat("y", Direction.y);
      }
    }
    public void RollAnimation(Vector3 Direction){
      animator.SetFloat("x", Direction.x);
      animator.SetFloat("y", Direction.y);
      ActivateLayer("Roll");
    }

    public void AttackAnimation(Vector3 Direction){
      animator.SetFloat("x", Direction.x);
      animator.SetFloat("y", Direction.y);
      ActivateLayer("Attack");
    }



    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName),1);
    }

    public bool CheckAnimationFinish(int layer, string name){
      return animator.GetCurrentAnimatorStateInfo(layer).IsName(name);
    }

    public bool GetBool(string name){
      return  animator.GetBool(name);
    }
}

