using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MinimapPlayer : MonoBehaviour
{
    // 컴포넌트 참조
    Rigidbody2D rigid;
    public PlayerCamera cam;

    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 연결
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

        // 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 좌우 반전
            transform.rotation = new Quaternion(0, 0, 0, 0);
            vHead = Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // 좌우 반전
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


        // 벽 이동불가
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vHead, 0.5f, mask);
        if (!rayHit)
        {
            // 캐릭터 좌표 이동
            transform.Translate(vHead * Time.deltaTime * speed);
        }

    }
}
