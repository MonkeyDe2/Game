using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{

    private PlayerSpellStat playerSpellStat;
    private Enemy enemy;
    public PlayerSpellStat PlayerSpellStat
    {
        get
        {
            return playerSpellStat;
        }
        set
        {
            playerSpellStat = value;
        }
    }

    public Enemy Enemy
    {
        get
        {
            return enemy;
        }
        set
        {
            enemy = value;
        }
    }
    


    private Vector3 offset;
    private int burning;
    private int slowed;
    private float newspeed;
    private float basespeed;
    [SerializeField] private Color slowColor;
    
 

    void Start(){
        playerSpellStat = GameObject.FindWithTag("GameController").GetComponent<PlayerSpellStat>();  
        enemy = GetComponent<Enemy>();
        basespeed = enemy.BaseSpeed;
        offset = new Vector3(0, -0.5f, 0);

    }


    public void StartBurn(string effect){
        //Instantiate(GameAssets.i.flametrail, transform.position + offset, Quaternion.identity).transform.SetParent(transform);
        if (burning < playerSpellStat.BurnDuration)
        {
            CancelInvoke(effect);
        }
        burning = 0;
        InvokeRepeating(effect, 1f, 1f);
    }
    public void StartSlow(string effect){        
        newspeed = enemy.BaseSpeed * (1 - playerSpellStat.SlowPercentage);
        if (slowed < playerSpellStat.BurnDuration)
        {
            CancelInvoke(effect);
        }
        slowed = 0;
        InvokeRepeating(effect, 0.1f, 1f);
    }
    
    void Burn(){
        //Instantiate(GameAssets.i.flametrail, transform.position + offset, Quaternion.identity).transform.SetParent(transform);
        enemy.Burn(playerSpellStat.BurnDamage);
        burning++;
        if (burning >= playerSpellStat.BurnDuration){
            CancelInvoke("Burn");
        }               
    }
    void Slow(){
        //Instantiate(GameAssets.i.icetrail, transform.position + offset, Quaternion.identity).transform.SetParent(transform);
        enemy.Slow(newspeed);
        enemy.CurrentColor = slowColor;
        enemy.MySpriteRenderer.color = enemy.CurrentColor;
        slowed++;
        if (slowed >= playerSpellStat.SlowDuration){
            CancelInvoke("Slow");
            enemy.CurrentSpeed = basespeed;
            enemy.CurrentColor = Color.white;
            enemy.MySpriteRenderer.color = enemy.CurrentColor;

        }               
    }      
}
