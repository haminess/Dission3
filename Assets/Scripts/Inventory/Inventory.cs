using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public List<Item> items; //item ����Ʈ
    private Dictionary<Item, int> itemDic; //item, item ���� ����

    [SerializeField] private Transform slotParent; //slotParent = Bag
    [SerializeField] private Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate() //����Ƽ �����Ϳ��� �ٷ� �۵� (���� �������� �� �𸣰��� �����غ����ҵ�)
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif
    void Awake()
    {
        itemDic = new Dictionary<Item, int>();
        FreshSlot();
    }
    
    public void FreshSlot()
    {
        //items.Sort((a, b) => a.itemNum.CompareTo(b.itemNum)); //itemNum��� ����
        int i = 0; //�� �ۿ� �����ؼ� �� ���� for���� ���� ���
        //for (; i < items.Count && i < slots.Length; i++)
        //{
        //    slots[i].item = items[i]; //slot�� item ����
        //}
        //for (; i < slots.Length; i++)
        //{
        //    slots[i].item = null; //�������� �� �� ���� �������� null�� ó��
        //}

        foreach(var kvp in itemDic)
        {
            if(i<slots.Length)
            {
                slots[i].SetSlot(kvp.Key, kvp.Value); //slot�� item, item ���� ����
                i++;
            }            
        }
        for (; i < slots.Length; i++)
        {
            slots[i].SetSlot(null, 0); //�� slot�� null ó��
        }
    }

    public void AddItem(Item _item)
    {
        if(itemDic.ContainsKey(_item))
        {
            itemDic[_item]++;
            Debug.Log("���� ���� item ���� = " + itemDic[_item]);
        }
        else
        {
            if (itemDic.Count < slots.Length)
            {
                itemDic.Add(_item, 1);
                Debug.Log("new item Add = " + _item.itemName);
            }
            else
            {
                print("������ ���� �� �ֽ��ϴ�.");
                return;
            }
        }
        FreshSlot();
        Debug.Log("Inventory > AddItem���� FreshSlot() �Ϸ�");
    }

    // Start is called before the f��irst frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
