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
            tooltip.gameObject.SetActive(true); //Ȱ��ȭ
            tooltip.SetupTooltip(item.itemName, item.itemContext); //���� ����
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false); //��Ȱ��ȭ
        
        //���� �ʱ�ȭ��?
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
