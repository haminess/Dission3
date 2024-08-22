using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffect : MonoBehaviour
{
    Camera camera;

    [Header("Gray")]
    Material CameraMaterial;
    public float GrayScale = 0.0f;
    float AppliedTime = 2.0f;

    [Header("Shake")]
    public float ShakeAmount = 0.5f;
    public float ShakeTime = 0.1f;

    [Header("Zoom")]
    public float ZoomFactor = 0.0f;
    public float ZoomTime = 0.5f;

    [Header("Move")]
    public float MoveSpeed = 0.5f;
    public Vector2 Destination;

    void Start()
    {
        camera = GetComponent<Camera>();
        CameraMaterial = new Material(Shader.Find("Custom/Grayscale"));
    }

    // 후처리 효과. src 이미지(현재 화면)를 dest 이미지로 교체
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        CameraMaterial.SetFloat("_Grayscale", GrayScale);
        Graphics.Blit(src, dest, CameraMaterial);
    }

    #region Move
    [ContextMenu("Move")]
    public void Move()
    {
        Move(Destination);
    }
    public void Move(Vector2 _pos)
    {
        StartCoroutine(OnMove(new Vector3(_pos.x, _pos.y, transform.position.z)));
    }

    IEnumerator OnMove(Vector3 _pos)
    {
        float accTime = 0;
        while(true)
        {
            accTime += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, _pos, accTime * MoveSpeed);

            if (Vector2.Distance(_pos, transform.position) < 0.001f)
                break;

            yield return null;
        }

        transform.position = _pos;
    }


    #endregion

    #region Gray Effect
    [ContextMenu("OnGray")]
    public void OnGrayEffect()
    {
        StartCoroutine(GrayEffect(true));
    }
    [ContextMenu("OffGray")]
    public void OffGrayEffect()
    {
        StartCoroutine(GrayEffect(false));
    }

    private IEnumerator GrayEffect(bool _On)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < AppliedTime)
        {
            elapsedTime += Time.deltaTime;

            if (_On)
                GrayScale = elapsedTime / AppliedTime;
            else
                GrayScale = (AppliedTime - elapsedTime) / AppliedTime;

            yield return null;
        }

        if (_On)
            GrayScale = 1;
        else
            GrayScale = 0;
    }
    #endregion

    #region Shake Effect
    [ContextMenu("Shake")]
    public void OnShakeEffect()
    {
        StartCoroutine(ShakeEffect());
    }

    private IEnumerator ShakeEffect()
    {
        Vector3 oldPos = transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < ShakeTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Random.insideUnitSphere * ShakeAmount + oldPos;

            yield return null;
        }

        transform.position = oldPos;
    }
    #endregion

    #region Zoom Effect
    [ContextMenu("Zoom")]
    public void OnZoomEffect()
    {
        StartCoroutine(ZoomEffect());
    }

    private IEnumerator ZoomEffect()
    {
        float oldSize = camera.orthographicSize;
        float Size = camera.orthographicSize + ZoomFactor;

        float elapsedTime = 0.0f;

        while (elapsedTime < ZoomTime)
        {
            elapsedTime += Time.deltaTime;

            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, Size, Time.deltaTime / ZoomTime);

            yield return null;
        }

        elapsedTime = 0.0f;
        float comeTime = 0.05f;
        while (elapsedTime < comeTime)
        {
            elapsedTime += Time.deltaTime;

            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, oldSize, Time.deltaTime / comeTime);

            yield return null;
        }
        camera.orthographicSize = oldSize;
    }
    #endregion
}