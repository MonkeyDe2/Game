using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Spell
{
  [SerializeField]
  private string name;

  [SerializeField]
  private int damage;
  [SerializeField]
  private float cooldown;

  [SerializeField]
  private float manaCost;

  [SerializeField]
  private GameObject spellPrefab;



  public string MyName {
    get{
      return name;
    }
  }
  public int MyDamage {
    get{
      return damage;
    }
  }
  public float ManaCost {
    get{
      return manaCost;
    }
  }
  public float Cooldown {
    get{
      return cooldown;
    }
  }
  public GameObject MySpellPrefab {
    get{
      return spellPrefab;
    }
  }
  

}