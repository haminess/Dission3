using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    // ������Ʈ ����
    Rigidbody2D rigid;
    Animator animator;
    public PlayerCamera cam;
    public SpriteRenderer sprite;
    public SoundManager soundMan;
    public AudioSource bgm;
    public AudioSource effect;
    public GameObject[] model;

    // ����
    public int characterNum = 0;

    // ������Ʈ ����
    public GameObject settingUI;
    bool isSetOn = false;


    // ���ΰ� ��ġ
    public Vector3 CurPos = new Vector3(0, 0, 0);
    public float moveDistance = 1;

    // ������ ���� ����
    public bool Movable = true;
    public bool Settable = false;
    public float waitTime = 0;
    public float speed = 0.1f;
    public MOVE_MODE moveMode = MOVE_MODE.MAP;

    public float setTime = 10;

    // �޴�
    private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ����
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        if (MainMan.instance)
        {
            bgm = MainMan.instance.bgm;
            effect = MainMan.instance.effect;
        }

        menu = GameObject.Find("MenuUI");


        // ���ΰ��� �ƴ� �� ����
        if (SceneManager.GetActiveScene().name != "MainGame") return;

        // character change
        if (GameObject.Find("Data"))
        {
            print("������ ������Ʈ ����");
            characterNum = GameObject.Find("Data").GetComponent<DataManager>().characterNum;
        }

        SetCharacterModel();

        // ����â ����
        settingUI.SetActive(false);

        // �������� ��
        // ĳ���� �ʱ� ��ġ ����
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
        // ������ ����
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

        // ���ΰ��ӿ���
        if (SceneManager.GetActiveScene().name == "MainManager")
        {
            // ���� �� ����â
            if (Input.GetKeyDown(KeyCode.Escape) && MainMan.instance.isStart)
            {
                OnSetting();
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 1000.0f);

        // �޴� ����
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
            print("�޴� ����");
        }
    }


    public void PlayMove()
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

        // �̵�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // �¿� ����
            animator?.SetBool("IsMove", true);
            animator?.SetFloat("DirX", 1);
            animator?.SetFloat("DirY", 0);
            sprite.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            vHead = Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // �¿� ����
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


        // �� �̵��Ұ�
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vHead, 0.5f * moveDistance, mask);
        if (!rayHit)
        {
            // ĳ���� ��ǥ �̵�
            transform.Translate(vHead * Time.deltaTime * fSpeed);
        }

    }

    public void Head(Vector3 _head)
    {
        // �� �̵��Ұ�
        LayerMask mask = LayerMask.GetMask("Wall") | LayerMask.GetMask("Object");

        // �ִϸ��̼�
        animator?.ResetTrigger("Jump");
        animator?.SetTrigger("Jump");
        // ȿ����
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
            // ����â ���� ����
            isSetOn = true;

            // �÷��̾� ����
            Movable = false;

            // ���� ����
            MainMan.instance.Stop();

            // ����â Ȱ��ȭ
            settingUI.SetActive(true);
        }
        else
        {
            // ����â ���� ����
            isSetOn = false;

            // �÷��̾� ������
            Movable = true;

            // ��Ʈ ��ġ�� �̵�
            MainMan.instance.PlayerReposition();

            // �̾��ϱ� ȣ��
            MainMan.instance.Continue();

            // ����â ��Ȱ��ȭ
            settingUI.SetActive(false);

            waitTime = 5;
        }
    }

    public void OffSetting()
    {
        // ����â ���� ����
        isSetOn = false;

        // �÷��̾� ������
        Movable = true;

        // ����â ��Ȱ��ȭ
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
