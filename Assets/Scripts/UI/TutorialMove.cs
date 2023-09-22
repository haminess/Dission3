using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : MonoBehaviour
{

    // ������Ʈ ����
    Rigidbody2D rigid;
    public SpriteRenderer sprite;
    Animator animator;
    public Tutorial tutorial;

    // ���ΰ� ��ġ
    public Vector3 CurPos = new Vector3(0, 0, 0);
    public float moveDistance = 1;
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void Move()
    {
        // �̵�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // �¿� ����
            sprite.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            Head(Vector3.left);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // �¿� ����
            sprite.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            Head(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Head(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Head(Vector3.down);
        }
        // ĳ���� ��ǥ �̵�
        transform.localPosition = Vector3.Lerp(transform.localPosition, CurPos, speed);
        if (transform.localPosition == CurPos)
        {
            transform.localPosition = CurPos;
        }
    }

    public void Head(Vector3 _head)
    {
        // �� �̵��Ұ�
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        // �ִϸ��̼�
        animator.SetTrigger("Jump");
        // ȿ����

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, _head, 1 * moveDistance, mask);
        if (!rayHit)
        {
            CurPos += _head * moveDistance;

            tutorial.Judge(tutorial.time, CurPos.x, CurPos.y);
        }
    }
}
