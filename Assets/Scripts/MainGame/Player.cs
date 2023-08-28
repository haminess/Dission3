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
    AudioSource BGM;

    // ������Ʈ ����
    public GameObject settingUI;
    bool isSetOn = false;

    // ���ΰ� ��ġ
    public Vector3 CurPos = new Vector3(0, 0, 0);
    public float moveDistance = 1;

    // ������ ���� ����
    public bool Movable = true;
    public bool Settable = false;
    public float speed = 0.1f;

    public float setTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        // ���ΰ��� �ƴ� �� ����
        if (SceneManager.GetActiveScene().name != "MainGame") return;

        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();

        // ����â ����
        settingUI.SetActive(false);

        // �������� ��
        // ĳ���� �ʱ� ��ġ ����
        switch(MainGame.instance.stageNum)
        {
            case 1:
                CurPos = new Vector3(0, 0, 0);
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

        // ���� �� ����â
        if (Input.GetKeyDown(KeyCode.Escape) && MainGame.instance.isStart)
        {
            OnSetting();
        }

    }

    private void Move()
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

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, _head, 1 * moveDistance, mask);
        if (!rayHit)
        {
            CurPos += _head * moveDistance;

            // ���ΰ��� �ƴϸ� ����
            if (SceneManager.GetActiveScene().name != "MainGame") return;
            // ����
            if (MainGame.instance.isGame)
                MainGame.instance.Judge(BGM.time, CurPos.x, CurPos.y);
        }
    }

    public void OnSetting()
    {
        if (!Settable) return;
        if (!isSetOn)
        {
            // ����â ���� ����
            isSetOn = true;

            // �÷��̾� ����
            Movable = false;

            // ���� ����
            MainGame.instance.Stop();

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
            MainGame.instance.PlayerReposition();

            // �̾��ϱ� ȣ��
            MainGame.instance.Continue();

            // ����â ��Ȱ��ȭ
            settingUI.SetActive(false);
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
