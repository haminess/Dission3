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
        get { return _item; } //������ item ������ �ѱ�
        //set
        //{
        //    _item = value; //item�� ���� �����͸� _item�� ����
        //    if (_item != null)
        //    {
        //        image.sprite = item.itemImage;
        //        image.color = new Color(1, 1, 1, 1); //item�� ������ ȭ�鿡 ǥ��
        //    }
        //    else
        //    {
        //        image.color = new Color(1, 1, 1, 0); //������ ǥ��X
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
            image.color = new Color(1, 1, 1, 1); //item�� ������ ȭ�鿡 ǥ��
            countText.text = _count.ToString(); //item ������ Text�� ǥ��
            countText.color = new Color(0, 0, 0, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0); //������ ǥ��X
            countText.text = ""; //���� ���� ó��
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
