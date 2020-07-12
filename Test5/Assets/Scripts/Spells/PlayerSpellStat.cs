using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class PlayerSpellStat: MonoBehaviour
{
    [SerializeField] private float burnDamage = 1f;
    [SerializeField] private float burnDuration = 5f;
    [SerializeField] private float slowPercentage = 0.5f;
    [SerializeField] private float slowDuration = 5f;

    public float BurnDamage 
    { 
        get
        {
            return burnDamage;
        } 
        set
        {
            burnDamage = value;
        } 
    }
    public float BurnDuration 
    { 
        get
        {
            return burnDuration;
        } 
        set
        {
            burnDuration = value;
        } 
    }
    public float SlowPercentage 
    { 
        get
        {
            return slowPercentage;
        } 
        set
        {
            slowPercentage = value;
        } 
    }
    public float SlowDuration 
    { 
        get
        {
            return slowDuration;
        } 
        set
        {
            slowDuration = value;
        } 
    }
}
