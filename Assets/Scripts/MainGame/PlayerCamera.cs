using UnityEngine;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Target;             // 따라다닐 타겟 오브젝트
    public float follow_speed = 4.0f;    // 따라가는 속도
    public float x = 0f;
    public float y = 0f;
    public float z = -10.0f;            // 고정시킬 카메라의 z축의 값

    private Transform this_transform;            // 카메라의 좌표
    private Transform Target_transform;         // 타겟의 좌표

    private void Start()
    {
        this_transform = GetComponent<Transform>();
        Target_transform = Target.GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        this_transform.position = Vector2.Lerp(this_transform.position, Target_transform.position + new Vector3(x, y), follow_speed * Time.deltaTime);
        this_transform.Translate(0, 0, z); //카메라를 원래 z축으로 이동
    }
}