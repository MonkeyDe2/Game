using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private int index;
    [SerializeField] private Tooltip tooltip;


    public void OnPointerEnter(PointerEventData eventData)
    {

        tooltip.ShowText(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideText();
    }
}
