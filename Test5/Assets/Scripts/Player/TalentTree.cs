using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalentTree : MonoBehaviour
{

    private int points = 10;
    [SerializeField]
    private Talent[] talents;
    [SerializeField] private Text talentPointText;
    

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
            UpdateTalentPointText();
        }
    }
    void Start()
    {
        
        ResetTalents();
    }

    public void TryUseTalent(Talent talent){
        if (Points > 0 && talent.Click())
        {
            Points--;
        }
        if (Points == 0){
            foreach (Talent t in talents){
                if(t.CurrentCount != t.MaxCount){
                    t.Lock();
                }
            }
        }
    }

    private void ResetTalents(){

        UpdateTalentPointText();

        foreach (Talent talent in talents)
        {
            talent.Lock();
        }
    }

    private void UpdateTalentPointText(){
        talentPointText.text = Points.ToString();
    }
}
