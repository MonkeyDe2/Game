using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Util;
using UnityEngine.UI;


public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Spell[] spells;
    private float[] currentCooldowns;
    [SerializeField]
    private float lightingRange;
    [SerializeField] private Vector3[] offset;
    private int offsetIndex;

    private Player player;

    public Spell GetSpell(string spellName){
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        return spell;
    }
    public bool castingProgess;
    [SerializeField] GameObject gatherEffect;

    private GameObject Canvas;

    private Image fireCD; 
    private Image iceCD; 
    private Image lightCD; 
    


    void Start(){
        player = GetComponent<Player>();
        currentCooldowns = new float[spells.Length + 1];
        Canvas = GameObject.FindGameObjectWithTag("UI");

        fireCD = Canvas.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        iceCD = Canvas.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        lightCD = Canvas.transform.GetChild(1).GetChild(2).GetComponent<Image>();
 
    }
    
    void Update(){
        for (int i = 0; i < currentCooldowns.Length; i++)
        {
            currentCooldowns[i] -= Time.deltaTime;
        }

        Canvas.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = currentCooldowns[0]/spells[0].Cooldown;
        Canvas.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = currentCooldowns[1]/spells[1].Cooldown;
        Canvas.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = currentCooldowns[2]/spells[2].Cooldown;

        
    }

    public void CastSpell(string spellname, int index){

        Spell spell = GetSpell(spellname);
        if (currentCooldowns[index] <= 0){
            if (player.SpendMana(spell.ManaCost)){
                currentCooldowns[index] = spell.Cooldown;
                castingProgess = true;

                if (0 < player.LastMoveDir.x){
                    if ( player.LastMoveDir.y < 0){
                        offsetIndex = 0; //Down
                    } else if (player.LastMoveDir.y > 0){
                        
                        offsetIndex = 1; //Up
                    } else {
                        offsetIndex = 2; //Left
                    }

                } else {
                    if ( player.LastMoveDir.y < 0){
                        offsetIndex = 0;
                    } else if (player.LastMoveDir.y > 0){
                        offsetIndex = 1;
                    } else {
                        offsetIndex = 3;
                    }
                }

                Vector3 mousepos = UtilClass.GetMouseWorldPosition();
                Vector3 mousedir = (mousepos - (transform.position + offset[offsetIndex]));
                float rotation = UtilClass.GetAngleFromVectorFloat(mousedir);

                Instantiate(gatherEffect, transform.position + offset[offsetIndex], Quaternion.identity);
                StartCoroutine(UtilClass.Wait(0.2f,() => ActualCast(spell, rotation, mousepos)));
                
                }
        }                
    }

    public void CastSpellLight(string spellname, int index){

        Vector3 mousepos = UtilClass.GetMouseWorldPosition();
        

        float distance = Vector3.Distance(mousepos,transform.position);
            if (distance <= lightingRange)
                {
                    Spell spell = GetSpell(spellname);

                if (currentCooldowns[index] <= 0){
                    if (player.SpendMana(spell.ManaCost)){
                        currentCooldowns[index] = spell.Cooldown;
                        castingProgess = true;

                        Vector3 origOffset = new Vector3(mousepos.x, mousepos.y + 130f, mousepos.z);
                        Vector3 mousedir = (mousepos - origOffset);
                        float rotation = UtilClass.GetAngleFromVectorFloat(mousedir);
                        LightSpell lightSpell = Instantiate(spell.MySpellPrefab, origOffset, Quaternion.Euler(0,0,rotation)).GetComponent<LightSpell>();            
                        lightSpell.TargetPos = mousepos;
                        lightSpell.source = transform;
                        lightSpell.damage = spell.MyDamage;
                        lightSpell.Speed = 1f;


                        }
            }
        }                
    }

    private void ActualCast(Spell spell, float rotated, Vector3 mousepos){
        SpellScript spellScript = Instantiate(spell.MySpellPrefab, transform.position + offset[offsetIndex], Quaternion.Euler(0,0,rotated)).GetComponent<SpellScript>();            
        spellScript.TargetPos = mousepos;
        spellScript.source = transform;
        spellScript.damage = spell.MyDamage;
        spellScript.Speed = 3f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + offset[0]);
        Gizmos.DrawLine(transform.position, transform.position + offset[1]);
        Gizmos.DrawLine(transform.position, transform.position + offset[2]);
        Gizmos.DrawLine(transform.position, transform.position + offset[3]);
    }
}
