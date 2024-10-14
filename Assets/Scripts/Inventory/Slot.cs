using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text countText;

    private Item _item;
    private int _count;
    public Item item
    {
        get { return _item; } //슬롯의 item 정보를 넘김
        //set
        //{
        //    _item = value; //item에 들어온 데이터를 _item에 저장
        //    if (_item != null)
        //    {
        //        image.sprite = item.itemImage;
        //        image.color = new Color(1, 1, 1, 1); //item이 있으면 화면에 표시
        //    }
        //    else
        //    {
        //        image.color = new Color(1, 1, 1, 0); //없으면 표시X
        //    }
        //}
    }

    public int count
    {
        get { return _count; }
    }

    public void SetSlot(Item item, int count)
    {
        _item = item;
        _count = count;

        if(_item != null)
        {
            image.sprite = item.itemImage;
            image.color = new Color(1, 1, 1, 1); //item이 있으면 화면에 표시
            countText.text = _count.ToString(); //item 개수를 Text에 표시
            countText.color = new Color(0, 0, 0, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0); //없으면 표시X
            countText.text = ""; //개수 공백 처리
            countText.color = new Color(0, 0, 0, 0);
        }
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
