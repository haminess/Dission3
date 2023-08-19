using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 컴포넌트 참조
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator animator;
    AudioSource BGM;

    // 오브젝트 참조
    public GameObject settingUI;
    bool isSetOn = false;

    // 내부값 위치
    public Vector3 CurPos = new Vector3(0, 0, 0);

    // 움직임 제어 관리
    public bool Movable = true;
    public float speed = 0.1f;

    public float setTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();

        settingUI.SetActive(false);

        // 스테이지 별
        // 캐릭터 초기 위치 설정
        switch(MainGame.instance.stageNum)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 움직임 제어
        if (Movable)
        {
            Move();
        }
        // 시작 후 설정창
        if (Input.GetKeyDown(KeyCode.Escape) && MainGame.instance.isStart)
        {
            OnSetting();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space" + BGM.time);
        }

    }

    private void Move()
    {
        // 이동
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 좌우 반전
            sprite.flipX = false;

            // 점프 애니메이션
            animator.SetTrigger("Jump");

            // 벽 오브젝트 감지
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.left, 1, mask);
            // 벽 없으면
            if (!rayHit)
            {
                // 갈 곳 설정
                CurPos += Vector3.left;

                // 판정함수 호출
                MainGame.instance.Judge(BGM.time, CurPos.x, CurPos.y);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 좌우 반전
            sprite.flipX = true;

            animator.SetTrigger("Jump");

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right, 1, mask);
            if (!rayHit)
            {
                CurPos += Vector3.right;
                MainGame.instance.Judge(BGM.time, CurPos.x, CurPos.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Jump");

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.up, 1, mask);
            if (!rayHit)
            {
                CurPos += Vector3.up;
                MainGame.instance.Judge(BGM.time, CurPos.x, CurPos.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetTrigger("Jump");

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, mask);
            if (!rayHit)
            {
                CurPos += Vector3.down;
                MainGame.instance.Judge(BGM.time, CurPos.x, CurPos.y);
            }
        }

        // 캐릭터 좌표 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, CurPos, speed);
        if (transform.localPosition == CurPos)
        {
            transform.localPosition = CurPos;
        }
    }

    public void OnSetting()
    {
        if (!isSetOn)
        {
            // 설정창 상태 설정
            isSetOn = true;

            // 플레이어 멈춤
            Movable = false;

            // 게임 멈춤
            MainGame.instance.Stop();

            // 설정창 활성화
            settingUI.SetActive(true);
        }
        else
        {
            // 설정창 상태 해제
            isSetOn = false;

            // 플레이어 움직임
            Movable = true;

            // 노트 위치로 이동
            MainGame.instance.PlayerReposition();

            // 이어하기 호출
            MainGame.instance.Continue();

            // 설정창 비활성화
            settingUI.SetActive(false);
        }
    }
}
