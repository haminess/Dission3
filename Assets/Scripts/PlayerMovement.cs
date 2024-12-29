using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 플레이어의 이동 속도 설정

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // 좌우 이동
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // 상하 이동

        transform.Translate(moveX, moveY, 0); // 플레이어 위치 변환
    }
}
