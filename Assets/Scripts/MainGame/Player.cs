using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // ������Ʈ ����
    Rigidbody2D rigid;
    public SpriteRenderer sprite;
    Animator animator;
    public SoundManager soundMan;
    public AudioSource bgm;
    public AudioSource effect;

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


        // ���ΰ��� �ƴ� �� ����
        if (SceneManager.GetActiveScene().name != "MainGame") return;


        // ����â ����
        settingUI.SetActive(false);

        // �������� ��
        // ĳ���� �ʱ� ��ġ ����
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
        // ������ ����
        if (Movable)
        {
            Move();
        }

        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        // ���ΰ��� �ƴ� �� ����
        if (SceneManager.GetActiveScene().name != "MainManager") return;

        // ���� �� ����â
        if (Input.GetKeyDown(KeyCode.Escape) && MainMan.instance.isStart)
        {
            OnSetting();
        }
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
        //soundMan.SetEffect(0);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, _head, 1 * moveDistance, mask);
        if (!rayHit)
        {
            CurPos += _head * moveDistance;

            // ���ΰ��� �ƴϸ� ����
            if (SceneManager.GetActiveScene().name != "MainGame") return;
            // ����
            if (MainMan.instance.isGame)
                MainMan.instance.Judge(bgm.time, CurPos);
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
}
