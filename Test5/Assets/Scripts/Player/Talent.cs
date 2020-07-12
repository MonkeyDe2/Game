using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talent : MonoBehaviour
{
    private Image sprite;
    [SerializeField] private Text countText;
    [SerializeField] private int maxCount;
    private int currentCount = 0;
    [SerializeField] private bool unlocked;

    [SerializeField] private Talent childTalent;
    [SerializeField] private Image arrowSprite;

    public int CurrentCount
    {
        get
        {
            return currentCount;
        }
    }

    public int MaxCount
    {
        get
        {
            return maxCount;
        }
    }

    private void Awake(){
        sprite = GetComponent<Image>();
        countText.text = currentCount.ToString() + '/' + maxCount.ToString();
        
    }

    void Start()
    {
        if (unlocked)
        {

            Unlock();
        }
    }

    public bool Click(){
        if (currentCount < maxCount && unlocked)
        {
            currentCount ++;
            countText.text = currentCount.ToString() + '/' + maxCount.ToString();

            if (currentCount == maxCount){
                if (childTalent != null){
                    childTalent.Unlock();
                }
            }
            return true;
        }
        return false;
    }
    public void Lock(){
        sprite.color = Color.gray;
        countText.color = Color.gray;
        if (arrowSprite != null){
            arrowSprite.color = Color.gray;
        }
       
    }
    

    public void Unlock(){
        sprite.color = Color.white;
        if (arrowSprite != null){
            arrowSprite.color = Color.white;
        }

        unlocked = true;
    }
}
