using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private static List<Enemy> enemyList;
    public Enemy Create(Vector3 position){
      Transform enemyTransform = Instantiate(GameAssets.i.pfEnemy, position, Quaternion.identity).transform;
      Enemy enemy = enemyTransform.GetComponent<Enemy>();

      if(enemyList == null) enemyList = new List<Enemy>();
      enemyList.Add(enemy);

      return enemy;
    }

    public static Enemy GetClosestEnemy(Vector3 position, float range){
      if (enemyList == null) return null;
      Enemy closestEnemy = null;

      for (int i = 0; i < enemyList.Count; i++) {
        Enemy testEnemy = enemyList[i];
        if (Vector3.Distance(position, testEnemy.GetPosition()) > range) {
          //Enemy too far, skip
          continue;
        }

        if (closestEnemy == null){
          //No closest enemy
          closestEnemy = testEnemy;
        } else {
          if (Vector3.Distance(position,testEnemy.GetPosition())< Vector3.Distance(position, closestEnemy.GetPosition()));
          {
            closestEnemy = testEnemy;
          }
        }
      }
      return closestEnemy;
    }



    private Transform target;
    private float damageAmount = 10f;
    private Vector3 direction;
    private Vector3 lastMoveDir;
    [SerializeField] private float baseSpeed;
    private float currentSpeed;
    private float health;
    [SerializeField] private float maxHealth;
    private AnimationManager animationManager;
    public float MyAttackRange { get; set; }
    private Vector3 origPos;
    [SerializeField] private float initAggroRange;
    public float MyAggroRange { get; set; }
    public bool InRange{
      get
      {
        return Vector2.Distance(transform.position, Target.position) < MyAggroRange;
      }
    }
    private IState currentState;

    public bool IsAttacking { get; set; } = false;
    public float MyAttackTime { get; set; }
    private Image image;
    private Text levelText;
    private bool regenerating;
    private float regen = 1f;
    private int level;
    private PlayerStat playerStat;
    [SerializeField] private Color hurtcolor;

    private SpriteRenderer spriteRenderer;
    private Color currentColor = Color.white; 
    private Rigidbody2D rigidBody;

    




    public float DamageAmount
            {
              get
              {
                return damageAmount;
              }
              set
              {
                damageAmount = value;
              }
            }
    public AnimationManager Animationmanager
        {
          get
          {
            return animationManager;
          }
          set
          {
            animationManager = value;
          }
        }
    public Transform Target
    {
      get
      {
        return target;
      }
      set
      {
        target = value;
      }
    }
    public Vector3 OrigPosition
      {
        get
        {
          return origPos;
        }
        set
        {
          origPos = value;
        }
      }
    public Vector3 Direction
    {
      get
      {
        return direction;
      }
      set
      {
        direction = value;
      }
    }
    public Vector3 LastMoveDir
    {
      get
      {
        return lastMoveDir;
      }
    }
    public float BaseSpeed
      {
        get
        {
          return baseSpeed;
        }
        set
        {
          baseSpeed = value;
        }
      }
      public float CurrentSpeed
      {
        get
        {
          return currentSpeed;
        }
        set
        {
          currentSpeed = value;
        }
      }
      public SpriteRenderer MySpriteRenderer
      {
        get
        {
          return spriteRenderer;
        }
        set
        {
          spriteRenderer = value;
        }
      }
      public Color CurrentColor
      {
        get
        {
          return currentColor;
        }
        set
        {
          currentColor = value;
        }
      }
      public Rigidbody2D MyRigidBody
      {
        get
        {
          return rigidBody;
        }
        set
        {
          rigidBody = value;
        }
      }


  
    

    void Awake(){
      MyAggroRange = initAggroRange;
      MyAttackRange = 90f;
      OrigPosition = transform.position;
      ChangeState(new IdleState());
      IsAttacking = false;
    }
    void Start()
    {
     
      Animationmanager = GetComponent<AnimationManager>();
      image = GetComponentInChildren<Image>();
      levelText = GetComponentInChildren<Text>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      rigidBody = GetComponent<Rigidbody2D>();

      
      playerStat = GameObject.FindWithTag("GameController").GetComponent<PlayerStat>();
 

      health = maxHealth;
      currentSpeed = baseSpeed;
      image.fillAmount = health;
      level = Random.Range(1,11);
      levelText.text = level.ToString();

    }

    // Update is called once per frame
    void Update()
    {

      if (direction.x != 0 || direction.y != 0){
            lastMoveDir = direction;
          }

      if (!IsAttacking)
      {
        MyAttackTime += Time.deltaTime;
        Animationmanager.IdleAnimation(lastMoveDir);
        Animationmanager.WalkAnimation(direction);
      }

      
      currentState.Update();

    }
    
    private void CheckDeath(){
      if (health <= 0){
        //Instantiate(GameAssets.i.Puff, transform.position, Quaternion.identity);
        Instantiate(GameAssets.i.bloodstain1, transform.position, Quaternion.identity);
        enemyList.Remove(this);
        playerStat.GainXP(level);
        Destroy(gameObject);
      }
    }


    public void TakeDamage(Vector3 attackPosition, float damageAmount, Transform source){
      health -= damageAmount;
      
      CheckDeath();
      SetTarget(source);
      image.fillAmount = health / maxHealth;
      Vector3 dirFromAttacker = (transform.position - attackPosition).normalized;

      float knockbackDistance = 1f;
      transform.position += dirFromAttacker * knockbackDistance;
      SpawnBlood();
      DamagePopUp.Create(transform.position, damageAmount);

      Debug.Log("YOU HIT AN ENEMYYYYY");
    }

    public Vector3 GetPosition(){
      return transform.position;
    }

    public void SetTarget(Transform source){       
      
      
      float distance = Vector2.Distance(transform.position, source.position);
      if (distance > MyAggroRange){
        MyAggroRange = initAggroRange;
        MyAggroRange += distance;
        Target = source;
      }
    }

    public void Reset()
    {
      target = null;
      MyAggroRange = initAggroRange;
      health = maxHealth;
      
      GetComponentInChildren<Image>().fillAmount = health;
    }

    public void ChangeState(IState newState){
      if (currentState != null){
        currentState.Exit();
      }

      currentState = newState;

      currentState.Enter(this);
    }

    public void heal(){
      if (health <= maxHealth && regenerating == false){
        regenerating = true;
        health += 5;
        image.fillAmount = health/maxHealth;
        StartCoroutine(Wait(regen, () => regenerating = false));
      }
    }

    

    public void SpawnBlood(){
      Instantiate(GameAssets.i.Blood, transform.position, Quaternion.identity);
      StartCoroutine(Flash());
    }

    public delegate void Callback();
    public IEnumerator Wait(float duration, Callback callback){
      yield return new WaitForSeconds(duration);
      if(callback != null){
        callback();
      }
      
    }
    public void Burn(float burnDamage){
        health -= burnDamage;
        SpawnBlood();
        DamagePopUp.Create(transform.position, burnDamage);
        image.fillAmount = health/maxHealth;
        CheckDeath();
    }
    public void Slow(float newspeed){
        currentSpeed = newspeed;
    }

    void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, MyAggroRange);

      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, MyAttackRange);
    }

    IEnumerator Flash(){
      spriteRenderer.color = hurtcolor;
      yield return new WaitForSeconds(0.1f);
      spriteRenderer.color = currentColor;
    }

}
