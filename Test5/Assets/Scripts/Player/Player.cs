using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Player : MonoBehaviour
{
    GameObject gameManager;
    [SerializeField] private LayerMask dashLayerMask;
    private CanvasGroup canvasGroup;


    private enum State
    {
      Normal,
      Rolling,
      Casting,
      Attack,
    }
    private Rigidbody2D rigidbody2D;
    private PlayerStat playerStat;
    private Vector3 moveDir;
    private AnimationManager animationManager;
    [SerializeField] private AnimationManager animationManager1;
    [SerializeField] private AnimationManager animationManager2;
    [SerializeField] private AnimationManager animationManager3;
    [SerializeField] private AnimationManager animationManager4;
    [SerializeField] private Vector3[] offset = new Vector3[4];
    private int offsetIndex; 
    
    private Vector3 lastMoveDir;
    private State state;
    private bool isDashButton = false;
    private float dashTimer = 1f;
    private float dashDuration;
    private float damageAmount = 10f;
    public bool open = false;
    private Vector3 min,max;
    private ParticleSystem dust;
    private float speed;
    public bool Active = true;
    private SpellBook spellBook;
    
    public Vector3 LastMoveDir{
      get
      {
        return lastMoveDir;
      }
      set
      {
        lastMoveDir = value;
      }
    }


    private void Start()
    {

      rigidbody2D = GetComponent<Rigidbody2D>();
      animationManager = GetComponent<AnimationManager>();
      canvasGroup = GameObject.FindGameObjectWithTag("UI").transform.GetChild(2).GetComponent<CanvasGroup>();
      gameManager = GameObject.FindGameObjectWithTag("GameController");
      playerStat = gameManager.GetComponent<PlayerStat>();
      dust = GetComponentInChildren<ParticleSystem>();
      spellBook = GetComponent<SpellBook>();
      

      speed = playerStat.Speed;
      state = State.Normal;

    }


    private void Update()
    {

      switch (state){
        case State.Normal:
          float moveX = 0f;
          float moveY = 0f;
          PauseDust();
          
          
        if (Active && Input.GetKeyDown(KeyCode.T)){
           canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
           canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
           open = open == false ? true: false;
        }   

        dashTimer += Time.deltaTime;
        if (Active){
          if (!open){
            if (Input.GetKey(KeyCode.W)){
              moveY = +1f;
            
            }
            if (Input.GetKey(KeyCode.S)){
              moveY = -1f;
              
            }
            if (Input.GetKey(KeyCode.A)){
              moveX = -1f;
              
            }
            if (Input.GetKey(KeyCode.D)){
              moveX = +1f;
              
            }
            if (Input.GetKeyDown(KeyCode.O)){
            TakeDamage(10);
            }
            if (Input.GetKeyDown(KeyCode.O)){
            SpendMana(10);
            }
        }
        }
          

            moveDir = new Vector3(moveX, moveY).normalized;
            
            if (moveX != 0 || moveY != 0){
              lastMoveDir = moveDir;
              PlayDust(moveDir);
            } else {
              PauseDust();
            }

            animationManager.IdleAnimation(lastMoveDir);
            animationManager1.IdleAnimation(lastMoveDir);
            animationManager2.IdleAnimation(lastMoveDir);
            animationManager3.IdleAnimation(lastMoveDir);
            animationManager4.IdleAnimation(lastMoveDir);
            animationManager.WalkAnimation(moveDir);
            animationManager1.WalkAnimation(moveDir);
            animationManager2.WalkAnimation(moveDir);
            animationManager3.WalkAnimation(moveDir);
            animationManager4.WalkAnimation(moveDir);

            CastSpellAttacks(); 
            PhysicalAttack();
            
          
            if (Input.GetKeyDown(KeyCode.Space) && dashTimer > 1 && Active){
              dashDuration = 0;
              // isDashButton = true;
              animationManager.RollAnimation(lastMoveDir);
              animationManager1.RollAnimation(lastMoveDir);
              animationManager2.RollAnimation(lastMoveDir);
              animationManager3.RollAnimation(lastMoveDir);
              animationManager4.RollAnimation(lastMoveDir);
              state = State.Rolling;
            }
          
          
          
          
          break;

          

        case State.Rolling:
          dashTimer = 0;
          

          break;

        case State.Casting:
          
          

          break;

        case State.Attack:

          break;
        }
    }

    private void FixedUpdate(){
      switch (state){
      case State.Normal:
        rigidbody2D.velocity = moveDir * speed;
        break;
      case State.Rolling:
        float rollSpeed = speed * 10;
        dashDuration += Time.deltaTime;
        rigidbody2D.velocity = lastMoveDir * rollSpeed;

        if (dashDuration > 0.15){
          state = State.Normal;
        }

        // float dashAmount = 100f;
        // Vector3 dashPosition = transform.position + lastMoveDir * dashAmount;
        // RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, lastMoveDir, dashAmount, dashLayerMask);
        // if (raycastHit2D.collider != null){
        //     dashPosition = raycastHit2D.point;
        //   }

        // rigidbody2D.MovePosition(dashPosition);
        // isDashButton = false;
        
        
        
        break;
      case State.Casting:
        rigidbody2D.velocity = moveDir * 0f;
        break;
      case State.Attack:
        rigidbody2D.velocity = moveDir * 0f;
        break;
      }
    }


    private void PhysicalAttack(){
      if (Input.GetMouseButtonDown(0) && Active && open == false){
        state = State.Attack;

        Vector3 mousePosition = UtilClass.GetMouseWorldPosition();
        Vector3 mouseDir = (mousePosition - transform.position).normalized;
        
        int dir = BasicAttackOffset(mousePosition);
        //Debug.Log(lastMoveDir);

        animationManager.AttackAnimation(lastMoveDir);
        animationManager1.AttackAnimation(lastMoveDir);
        animationManager2.AttackAnimation(lastMoveDir);
        animationManager3.AttackAnimation(lastMoveDir);
        animationManager4.AttackAnimation(lastMoveDir);
        animationManager.MyAnimator.SetTrigger("Attack");
        animationManager1.MyAnimator.SetTrigger("Attack");
        animationManager2.MyAnimator.SetTrigger("Attack");
        animationManager3.MyAnimator.SetTrigger("Attack");
        animationManager4.MyAnimator.SetTrigger("Attack");
        StartCoroutine(UtilClass.Wait(0.45f, () => BasicAttack(dir, mousePosition, mouseDir)));
        StartCoroutine(UtilClass.Wait(0.5f, () => state = State.Normal));
        
        
      }
    }

    public void TakeDamage(float damage){

      playerStat.CurrentHealth -= damage;

      if (playerStat.CurrentHealth <= 0){
        Debug.Log("DEADDDDD!!!!!");
      }
      playerStat.UpdateHPStat();
    }


    public bool SpendMana(float mana){
      if (playerStat.CurrentMana - mana < 0){
        Debug.Log("Not enough mana");
        return false;
      } else {
        playerStat.CurrentMana -= mana;
        playerStat.UpdateMPStat();
        return true;
      }
    }

    private void CastSpellAttacks(){
      if (!open && Active){
            if (Input.GetKeyDown(KeyCode.Alpha1)){
            
            spellBook.CastSpellMain(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)){
            
            spellBook.CastSpellMain(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)){
            
            spellBook.CastSpellMain(2);
            }
        }
      
      if (spellBook.castingProgess)
      {
        state = State.Casting;
        StartCoroutine(UtilClass.Wait(0.5f, () => state = State.Normal));
        spellBook.castingProgess = false;
      } else {
        state = State.Normal;
      } 
    }

    public void SetLimits(Vector3 min, Vector3 max){
      this.min = min;
      this.max = max;
    }

    void PlayDust(Vector3 dir){
      float angle = UtilClass.GetAngleFromVectorFloat(dir);
      dust.transform.localRotation = Quaternion.Euler(angle,-90,0);
      dust.Play();
    }
    void PauseDust(){
      dust.Stop();
    }
    void BasicAttack(int dir, Vector3 mousepos, Vector3 mousedir){
      
      float rotation = UtilClass.GetAngleFromVectorFloat(mousedir);

      SpellScript basicAttack = Instantiate(GameAssets.i.basicAttack, transform.position + offset[dir], Quaternion.Euler(0,0,rotation)).GetComponent<SpellScript>();            
        basicAttack.TargetPos = mousepos;
        basicAttack.source = transform;
        basicAttack.damage = damageAmount;
        basicAttack.Speed = 3f;

    }

    int BasicAttackOffset(Vector3 mousePos){
      float distDown = Vector3.Distance(mousePos,transform.position + new Vector3(0,-70,0));
      float distUp = Vector3.Distance(mousePos,transform.position + new Vector3(0,70,0));
      float distLeft = Vector3.Distance(mousePos,transform.position + new Vector3(-70,0,0));
      float distRight = Vector3.Distance(mousePos,transform.position + new Vector3(70,0,0));

      float smallestDist1 = Mathf.Min(distDown, distUp);
      float smallestDist2 = Mathf.Min(distLeft, distRight);

      float finalSmallest = Mathf.Min(smallestDist1,smallestDist2);

      if (finalSmallest == distDown){
        lastMoveDir.x = 0f;
        lastMoveDir.y = -1f;
        return 0;
      }
      if (finalSmallest == distUp){
        lastMoveDir.x = 0f;
        lastMoveDir.y = 1f;
        return 1;
      }
      if (finalSmallest == distLeft){
        lastMoveDir.x = -1f;
        lastMoveDir.y = 0f;
        return 3;
      }
      if (finalSmallest == distRight){
        lastMoveDir.x = 1f;
        lastMoveDir.y = 0f;
        return 2;
      } else{
        return 0;
      }



      // if (0 < LastMoveDir.x){
      //   if ( LastMoveDir.y < 0){
      //     offsetIndex = 0; //Down
      //   } else if (LastMoveDir.y > 0){
      //      offsetIndex = 1; //Up
      //   } else {
      //     offsetIndex = 2; //Left
      //   }

      // } else {
      //   if ( LastMoveDir.y < 0){
      //     offsetIndex = 0;
      //   } else if (LastMoveDir.y > 0){
      //     offsetIndex = 1;
      //     } else {
      //         offsetIndex = 3;
      //     }
      // }
    }
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawLine(transform.position, transform.position + offset[0]);
    //     Gizmos.DrawLine(transform.position, transform.position + offset[1]);
    //     Gizmos.DrawLine(transform.position, transform.position + offset[2]);
    //     Gizmos.DrawLine(transform.position, transform.position + offset[3]);
    // }
}
