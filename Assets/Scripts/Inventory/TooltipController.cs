using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponentInChildren<Slot>().item;

        if (item != null)
        {
            tooltip.gameObject.SetActive(true); //활성화
            tooltip.SetupTooltip(item.itemName, item.itemContext); //정보 전달
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false); //비활성화
        
        //정보 초기화도?
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
