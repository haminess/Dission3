using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveMode
{
    Default,
    Slide,
    Stop
}

public class LMove : MonoBehaviour
{

    // 컴포넌트 참조
    Rigidbody2D rigid;
    public SpriteRenderer sprite;
    Animator animator;
    public LNoteManager noteManager;

    // 설정
    public MoveMode moveMode = MoveMode.Default;
    public Coroutine slideCo = null;
    public Transform slide_note;

    // 내부값 위치
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

    IEnumerator Slide(Vector3 _head)
    {
        while(moveMode == MoveMode.Slide)
        {
            CurPos += _head * moveDistance * 0.1f;
            yield return null;
        }
    }

    public void StopSlide()
    {
        CurPos = new Vector3(Mathf.Round(CurPos.x), Mathf.Round(CurPos.y), CurPos.z);
    }

    public void Move()
    {
        // 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // 좌우 반전
            sprite.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            Head(Vector3.left);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // 좌우 반전
            sprite.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            Head(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Head(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Head(Vector3.down);
        }

        // 캐릭터 좌표 이동
        if (moveMode == MoveMode.Default)
            //transform.position = Vector3.Lerp(transform.position, CurPos, speed);
            //transform.position = Vector3.Lerp(transform.position, CurPos, speed * 2);
            transform.position = CurPos;

    }

    public void Head(Vector3 _head)
    {
    // 벽 이동불가
    LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        // 애니메이션
        animator.SetTrigger("Jump");
        // 효과음

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, _head, 1 * moveDistance, mask);
        if (!rayHit)
        {
            if(moveMode == MoveMode.Default)
            {
                CurPos += _head * moveDistance;
                noteManager.NewJudge(noteManager.bgm.time, CurPos.x, CurPos.y);
            }

            //// long note
            //if (CurPos == slide_note.position)
            //    moveMode = MoveMode.Slide;
            //else
            //    moveMode = MoveMode.Default;
        }
    }

    public void ChangeMode(MoveMode _mode)
    {
        if(moveMode != MoveMode.Default && _mode == MoveMode.Default)
        {
            CurPos.x = Mathf.Round(transform.position.x); 
            CurPos.y = Mathf.Round(transform.position.y);
        }
        moveMode = _mode;
    }
}
