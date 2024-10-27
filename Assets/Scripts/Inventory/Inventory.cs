using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public List<Item> items; //item 리스트
    private Dictionary<Item, int> itemDic; //item, item 개수 저장

    [SerializeField] private Transform slotParent; //slotParent = Bag
    [SerializeField] private Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate() //유니티 에디터에서 바로 작동 (무슨 차이인지 잘 모르겠음 공부해봐야할듯)
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
        //items.Sort((a, b) => a.itemNum.CompareTo(b.itemNum)); //itemNum대로 정렬
        int i = 0; //얠 밖에 선언해서 두 개의 for문에 같이 사용
        //for (; i < items.Count && i < slots.Length; i++)
        //{
        //    slots[i].item = items[i]; //slot에 item 삽입
        //}
        //for (; i < slots.Length; i++)
        //{
        //    slots[i].item = null; //아이템이 꽉 안 차면 나머지는 null로 처리
        //}

        foreach(var kvp in itemDic)
        {
            if(i<slots.Length)
            {
                slots[i].SetSlot(kvp.Key, kvp.Value); //slot에 item, item 개수 삽입
                i++;
            }            
        }
        for (; i < slots.Length; i++)
        {
            slots[i].SetSlot(null, 0); //빈 slot은 null 처리
        }
    }

    public void AddItem(Item _item)
    {
        if(itemDic.ContainsKey(_item))
        {
            itemDic[_item]++;
            Debug.Log("증가 이후 item 개수 = " + itemDic[_item]);
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
                print("슬롯이 가득 차 있습니다.");
                return;
            }
        }
        FreshSlot();
        Debug.Log("Inventory > AddItem에서 FreshSlot() 완료");
    }

    // Start is called before the fㄹirst frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
