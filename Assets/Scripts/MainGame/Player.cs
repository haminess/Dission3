using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 컴포넌트 참조
    Rigidbody2D rigid;
    public SpriteRenderer sprite;
    Animator animator;
    public SoundManager soundMan;
    public AudioSource bgm;
    public AudioSource effect;

    // 오브젝트 참조
    public GameObject settingUI;
    bool isSetOn = false;

    // 내부값 위치
    public Vector3 CurPos = new Vector3(0, 0, 0);
    public float moveDistance = 1;

    // 움직임 제어 관리
    public bool Movable = true;
    public bool Settable = false;
    public float waitTime = 0;
    public float speed = 0.1f;

    public float setTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        if (MainMan.instance)
        {
            bgm = MainMan.instance.bgm;
            effect = MainMan.instance.effect;
        }


        // 메인게임 아닐 때 리턴
        if (SceneManager.GetActiveScene().name != "MainGame") return;


        // 설정창 숨김
        settingUI.SetActive(false);

        // 스테이지 별
        // 캐릭터 초기 위치 설정
        switch(MainMan.instance.stageNum)
        {
            case 1:
                MainMan.instance.PlayerReposition();
                break;
            case 2:
                CurPos = new Vector3(0, 0, 0);
                break;
            case 3:
                CurPos = new Vector3(0, 0, 0);
                break;
            case 4:
                CurPos = new Vector3(0, 0, 0);
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

        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        // 메인게임 아닐 때 리턴
        if (SceneManager.GetActiveScene().name != "MainManager") return;

        // 시작 후 설정창
        if (Input.GetKeyDown(KeyCode.Escape) && MainMan.instance.isStart)
        {
            OnSetting();
        }
    }

    public void Move()
    {
        // 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 좌우 반전
            sprite.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            Head(Vector3.left);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 좌우 반전
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
        // 캐릭터 좌표 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, CurPos, speed);
        if (transform.localPosition == CurPos)
        {
            transform.localPosition = CurPos;
        }
    }

    public void Head(Vector3 _head)
    {
        // 벽 이동불가
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        // 애니메이션
        animator.SetTrigger("Jump");
        // 효과음
        //soundMan.SetEffect(0);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, _head, 1 * moveDistance, mask);
        if (!rayHit)
        {
            CurPos += _head * moveDistance;

            // 메인게임 아니면 리턴
            if (SceneManager.GetActiveScene().name != "MainGame") return;
            // 판정
            if (MainMan.instance.isGame)
                MainMan.instance.Judge(bgm.time, CurPos);
        }
    }

    public void OnSetting()
    {
        if (!Settable || waitTime > 0) return;
        if (!isSetOn)
        {
            // 설정창 상태 설정
            isSetOn = true;

            // 플레이어 멈춤
            Movable = false;

            // 게임 멈춤
            MainMan.instance.Stop();

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
            MainMan.instance.PlayerReposition();

            // 이어하기 호출
            MainMan.instance.Continue();

            // 설정창 비활성화
            settingUI.SetActive(false);

            waitTime = 5;
        }
    }

    public void OffSetting()
    {
        // 설정창 상태 해제
        isSetOn = false;

        // 플레이어 움직임
        Movable = true;

        // 설정창 비활성화
        settingUI.SetActive(false);
    }
}
