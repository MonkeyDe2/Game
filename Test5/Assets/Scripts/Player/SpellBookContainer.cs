using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] spellIcons;
    [SerializeField] private Image[] spellHotbar;
    public int[] spellIndexInHotbar;
    private CanvasGroup canvasGroup;
    private string[] slot = new string[] {"Slot 1","Slot 2","Slot 3"};
    public int selected = -1;
    private bool GUIEnabled = true;


    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        spellIndexInHotbar = new int[] {-1,-1,-1};
    }

    void Update()
    {
        
        //Debug.Log(spellIndexInHotbar[0]);
        if (canvasGroup.alpha == 0) return;
     
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            selected = 0;
            //show indicator
            GUIEnabled = !GUIEnabled;

            //check if others have that spell            
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            selected = 1;
            //show indicator
            GUIEnabled = !GUIEnabled;
            //check if others have that spell

        }
        
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            selected = 2;
            //show indicator
            GUIEnabled = !GUIEnabled;
            //check if others have that spell

        }

        if (Input.GetKeyDown("t"))
        {
            GUIEnabled = false;
            Debug.Log("testing");
            //Does this fucntion actually work? :D use Debug.Log to test it out
        }


       
    }

    public void Selected(int i){
        //check which spell hotbar is selected
        if (selected >= 0){
           spellHotbar[selected].GetComponent<Image>().sprite = spellIcons[i].GetComponent<Image>().sprite;
           spellIndexInHotbar[selected] = i;
           selected = -1; 
        }
        
    }

    void OnGUI()
    {
        if (selected >= 0)
        {
            if (GUIEnabled)
            {
                GUI.Label(new Rect(Screen.height / 3, Screen.height / 2, 300, 20), slot[selected] + " selected");
                //This works but it would be better if you could display an image instead on top of the spell hot bar and not text. Also test
                //it out I think there are some bugs at the moment. 
                //Hint: Could use canvas instead of GUI to display images and text as this gives more customisation and makes things prettier
            }
        }
    }
}
