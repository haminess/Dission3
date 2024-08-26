using UnityEngine;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Target;             // ����ٴ� Ÿ�� ������Ʈ
    public float follow_speed = 4.0f;    // ���󰡴� �ӵ�
    public float x = 0f;
    public float y = 0f;
    public float z = -10.0f;            // ������ų ī�޶��� z���� ��

    private Transform this_transform;            // ī�޶��� ��ǥ
    private Transform Target_transform;         // Ÿ���� ��ǥ

    private void Start()
    {
        this_transform = GetComponent<Transform>();
        Target_transform = Target.GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        this_transform.position = Vector2.Lerp(this_transform.position, Target_transform.position + new Vector3(x, y), follow_speed * Time.deltaTime);
        this_transform.Translate(0, 0, z); //ī�޶� ���� z������ �̵�
    }
}