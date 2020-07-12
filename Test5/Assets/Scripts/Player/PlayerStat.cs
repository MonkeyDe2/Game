using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;

    [SerializeField] private float regenHealth;
    [SerializeField] private float regenMana;

    [SerializeField] private float defaultSpeed;

    private float xpForNextLevel;
    private float currentXP = 0;
    private float currentLevel = 1;

    private float currentHealth;
    private float currentMana;


    public float MaxHealth 
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
  }

  public float MaxMana 
    {
        get
        {
            return maxMana;
        }
        set
        {
            maxMana = value;
        }
  }

  public float CurrentHealth 
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
  }

  public float CurrentMana 
    {
        get
        {
            return currentMana;
        }
        set
        {
            currentMana = value;
        }
  }
  public float Speed 
    {
        get
        {
            return defaultSpeed;
        }
        set
        {
            defaultSpeed = value;
        }
  }








    [SerializeField] private Image healthImage;
    [SerializeField] private Image manaImage;
    [SerializeField] private Text levelText;

    private bool regenerating = false;

    void Awake(){
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateHPStat();
        UpdateMPStat();
        xpForNextLevel = NextLevelXP(currentLevel);
        levelText.text = currentLevel.ToString();
        
    }

    private void Update(){
        regen();
        if (Input.GetKeyDown(KeyCode.I)){
            GainXP(100);
        }
        
        
    }
    public void regen(){
        if (regenerating == false){
            if (currentHealth + regenHealth <= maxHealth){
                currentHealth += regenHealth;
                UpdateHPStat();
            }
            if (currentMana + regenMana <= maxMana){
                currentMana += regenMana;
                UpdateMPStat();
            }
        regenerating = true;
        StartCoroutine(Wait());
        }
    }

    public void UpdateHPStat(){
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateMPStat(){
        manaImage.fillAmount = currentMana / maxMana;
    }


    public void GainXP(int enemylevel){
        currentXP += (currentLevel * 5) + ((enemylevel - currentLevel) * 5); 
        Debug.Log("gain xp");

        if (currentXP >= xpForNextLevel){
            currentXP -= xpForNextLevel;
            currentLevel++;
            xpForNextLevel = NextLevelXP(currentLevel);
        }
        levelText.text = currentLevel.ToString();
    }

    private float NextLevelXP(float level){
        return 100 * currentLevel * Mathf.Pow(currentLevel,0.5f);
    }
    private IEnumerator Wait(){
        yield return new WaitForSeconds(1);
        regenerating = false;
    }
}
