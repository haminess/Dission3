using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MinimapPlayer : MonoBehaviour
{
    // ������Ʈ ����
    Rigidbody2D rigid;
    public PlayerCamera cam;

    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ����
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameMove();
    }


    public void GameMove()
    {
        Vector3 vHead = Vector3.zero;

        // �̵�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // �¿� ����
            transform.rotation = new Quaternion(0, 0, 0, 0);
            vHead = Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // �¿� ����
            transform.rotation = new Quaternion(0, 180, 0, 0);
            vHead = Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vHead = Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vHead = Vector3.down;
        }


        // �� �̵��Ұ�
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vHead, 0.5f, mask);
        if (!rayHit)
        {
            // ĳ���� ��ǥ �̵�
            transform.Translate(vHead * Time.deltaTime * speed);
        }

    }
}
