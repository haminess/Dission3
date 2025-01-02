using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    // 컴포넌트 참조
    Rigidbody2D rigid;
    Animator animator;
    public PlayerCamera cam;
    public SpriteRenderer sprite;
    public SoundManager soundMan;
    public AudioSource bgm;
    public AudioSource effect;
    public GameObject[] model;

    // 정보
    public int characterNum = 0;

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
    public MOVE_MODE moveMode = MOVE_MODE.MAP;

    public float setTime = 10;

    // 메뉴
    private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 연결
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        if (MainMan.instance)
        {
            bgm = MainMan.instance.bgm;
            effect = MainMan.instance.effect;
        }

        menu = GameObject.Find("MenuUI");


        // 메인게임 아닐 때 리턴
        if (SceneManager.GetActiveScene().name != "MainGame") return;

        // character change
        if (GameObject.Find("Data"))
        {
            print("데이터 오브젝트 연결");
            characterNum = GameObject.Find("Data").GetComponent<DataManager>().characterNum;
        }

        SetCharacterModel();

        // 설정창 숨김
        settingUI.SetActive(false);

        // 스테이지 별
        // 캐릭터 초기 위치 설정
        switch (MainMan.instance.stageNum)
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
            if (moveMode == MOVE_MODE.MAP)
            {
                GameMove();
            }
            else
            {
                PlayMove();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator?.ResetTrigger("Jump");
                animator?.SetTrigger("Jump");
            }
        }

        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        // 메인게임에서
        if (SceneManager.GetActiveScene().name == "MainManager")
        {
            // 시작 후 설정창
            if (Input.GetKeyDown(KeyCode.Escape) && MainMan.instance.isStart)
            {
                OnSetting();
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 1000.0f);

        // 메뉴 관리
        ControlMenu();
    }

    [ContextMenu("ChangeCharacter")]
    public void SetCharacterModel()
    {
        // select character
        for (int i = 0; i < model.Length; i++)
        {
            model[i].SetActive(false);
        }
        model[characterNum].SetActive(true);
        sprite = model[characterNum].GetComponent<SpriteRenderer>();
        animator = model[characterNum].GetComponent<Animator>();
    }

    void ControlMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }

        if(menu)
        {
            print("메뉴 켜짐");
        }
    }


    public void PlayMove()
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Sit", true);
        }

    }
    public void GameMove()
    {
        Vector3 vHead = Vector3.zero;
        float fSpeed = 3;

        animator?.SetBool("IsMove", false);
        animator?.SetFloat("DirX", 0);
        animator?.SetFloat("DirY", 0);

        // 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 좌우 반전
            animator?.SetBool("IsMove", true);
            animator?.SetFloat("DirX", 1);
            animator?.SetFloat("DirY", 0);
            sprite.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            vHead = Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // 좌우 반전
            animator?.SetBool("IsMove", true);
            animator?.SetFloat("DirX", 1);
            animator?.SetFloat("DirY", 0);
            sprite.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            vHead = Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator?.SetBool("IsMove", true);
            animator?.SetFloat("DirX", 0);
            animator?.SetFloat("DirY", -1);
            vHead = Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator?.SetBool("IsMove", true);
            animator?.SetFloat("DirX", 0);
            animator?.SetFloat("DirY", 1);
            vHead = Vector3.down;
        }


        // 벽 이동불가
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vHead, 0.5f * moveDistance, mask);
        if (!rayHit)
        {
            // 캐릭터 좌표 이동
            transform.Translate(vHead * Time.deltaTime * fSpeed);
        }

    }

    public void Head(Vector3 _head)
    {
        // 벽 이동불가
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        // 애니메이션
        animator?.ResetTrigger("Jump");
        animator?.SetTrigger("Jump");
        // 효과음
        //soundMan.SetEffect(0);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, _head, 1 * moveDistance, mask);
        if (!rayHit)
        {
            CurPos += _head * moveDistance;

            // judge
            if (MainMan.instance && MainMan.instance.isGame)
            {
                MainMan.instance.NewJudge(bgm.time, CurPos);
            }
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


    public void ChangeMode(MOVE_MODE _mode)
    {
        //cam.enabled = false;
        //cam.transform.localPosition = new Vector3(0, 0, cam.transform.localPosition.z);

        if (moveMode != MOVE_MODE.GAME_NORMAL && _mode == MOVE_MODE.GAME_NORMAL)
        {
            //cam.enabled = true;
            CurPos.x = Mathf.Round(transform.position.x);
            CurPos.y = Mathf.Round(transform.position.y);
        }

        if(MainMan.instance.isGame)
        {
            if (_mode == MOVE_MODE.GAME_SLIDE)
            {
                animator.SetBool("Slide", true);
            }
            else
            {
                animator.SetBool("Slide", false);
            }
        }
        moveMode = _mode;
    }
}
