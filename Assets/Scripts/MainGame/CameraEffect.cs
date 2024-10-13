using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

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
        StartCoroutine(OnMove(new Vector3(_pos.x, _pos.y, -8)));
    }

    IEnumerator OnMove(Vector3 _pos)
    {
        float accTime = 0;
        while(true)
        {
            accTime += Time.deltaTime;
            transform.position = Vector2.Lerp(new Vector3(transform.position.x, transform.position.y, -8), _pos, accTime * MoveSpeed);

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

    private IEnumerator ShakeEffect(float shakeamount = 0.5f , float shaketime = 1)
    {
        ShakeTime = shaketime;
        ShakeAmount = shakeamount;
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

    private IEnumerator ZoomEffect(float ZoomFactor = 3.5f, float ZoomTime = 0.5f)
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


    [Header("HJW")]
    public GameObject splash;
    public UnityEngine.Rendering.Volume volume;

    public POSTPROCESS g_Efftype; 
    public float g_intensity; 
    public float g_interptime;
    public Color g_color;

    float g_contrast;
    float g_hue;
    float g_saturation;

    [ContextMenu("PostProcess")]
    public void PostProcess()
    {
        StartCoroutine(PostProcess(g_Efftype, g_intensity, g_interptime));
    }
    /// <summary>
    /// Bloom, Vignette, Chormatic Aberration, Noise 는 0 ~ 1, Lens Distortion -1 ~ 1, Blur 1 ~ 300    
    /// </summary>
    /// <param name="Efftype"></param>
    /// <param name="intensity"></param>
    /// <param name="interptime"></param>
    /// <returns></returns>
    IEnumerator PostProcess(POSTPROCESS Efftype, float intensity, float interptime)
    {
        switch (Efftype)
        {
            case POSTPROCESS.Bloom:
                Bloom bloom;
                if (volume.profile.TryGet(out bloom))
                {
                    while (Mathf.Abs(bloom.intensity.value - intensity) > 0.01f)
                    {
                        bloom.intensity.Interp(bloom.intensity.value, intensity, interptime);
                        yield return null;
                    }
                }
                break;
            case POSTPROCESS.Vignette:
                Vignette vignette;
                if (volume.profile.TryGet(out vignette))
                {
                    while (Mathf.Abs(vignette.intensity.value - intensity) > 0.01f)
                    {
                        vignette.intensity.Interp(vignette.intensity.value, intensity, interptime);
                        yield return null;
                    }
                }
                break;
            case POSTPROCESS.Chromatic_Aberration:
                ChromaticAberration chromaticAberration;
                if (volume.profile.TryGet(out chromaticAberration))
                {
                    while (Mathf.Abs(chromaticAberration.intensity.value - intensity) > 0.01f)
                    {
                        chromaticAberration.intensity.Interp(chromaticAberration.intensity.value, intensity, interptime);
                        yield return null;
                    }
                }
                break;
            case POSTPROCESS.Lens_Distortion:
                LensDistortion lensDistortion;
                if (volume.profile.TryGet(out lensDistortion))
                {
                    while (Mathf.Abs(lensDistortion.intensity.value - intensity) > 0.01f)
                    {
                        lensDistortion.intensity.Interp(lensDistortion.intensity.value, intensity, interptime);
                        yield return null;
                    }
                }
                break;
            case POSTPROCESS.Blur:
                DepthOfField depthOfField;
                if (volume.profile.TryGet(out depthOfField))
                {
                    while (Mathf.Abs(depthOfField.focalLength.value - intensity) > 0.01f)
                    {
                        depthOfField.focalLength.Interp(depthOfField.focalLength.value, intensity, interptime);
                        yield return null;
                    }
                }
                break;
            case POSTPROCESS.Noise:
                FilmGrain filmGrain;
                if (volume.profile.TryGet(out filmGrain))
                {
                    while (Mathf.Abs(filmGrain.intensity.value - intensity) > 0.01f)
                    {
                        filmGrain.intensity.Interp(filmGrain.intensity.value, intensity, interptime);
                        yield return null;
                    }
                }
                break;
        }
    }

    [ContextMenu("Emission")]
    public void Emission()
    {
        StartCoroutine(Emission(g_Efftype, g_intensity, g_interptime, g_color));
    }
    /// <summary>
    /// color 값에 따라 화면이 발광함, 0 ~ 80
    /// </summary>
    /// <param name="Efftype"></param>
    /// <param name="intensity"></param>
    /// <param name="interptime"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    /// 
    IEnumerator Emission(POSTPROCESS Efftype, float intensity, float interptime, Color color)
    {
        Bloom bloom;
        if (volume.profile.TryGet(out bloom))
        {
            while (Mathf.Abs(bloom.intensity.value - intensity) > 0.01f)
            {
                bloom.intensity.Interp(bloom.intensity.value, intensity, interptime);
                bloom.tint.Interp(bloom.tint.value, color, interptime);
                yield return null;
            }
        }
    }

    [ContextMenu("ColorFilter")]
    public void ColorFilter()
    {
        StartCoroutine(ColorFilter(g_Efftype, g_contrast, g_hue, g_saturation, g_interptime));
    }
    /// <summary>
    /// 화면의 색상을 보정할 수 있음, Contrast 대비 -100 ~ 100, Hue 색조 -180 ~ 180, Saturation 채도 -100 ~ 100
    /// </summary>
    /// <param name="Efftype"></param>
    /// <param name="contrast"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="interptime"></param>
    /// <returns></returns>
    IEnumerator ColorFilter(POSTPROCESS Efftype, float contrast, float hue, float saturation, float interptime)
    {
        ColorAdjustments colorAdjustments;
        if (volume.profile.TryGet(out colorAdjustments))
        {
            while (Mathf.Abs(colorAdjustments.contrast.value - contrast) > 0.01f || Mathf.Abs(colorAdjustments.hueShift.value - hue) > 0.01f || Mathf.Abs(colorAdjustments.saturation.value - saturation) > 0.01f)
            {
                colorAdjustments.contrast.Interp(colorAdjustments.contrast.value, contrast, interptime);
                colorAdjustments.hueShift.Interp(colorAdjustments.hueShift.value, hue, interptime);
                colorAdjustments.saturation.Interp(colorAdjustments.saturation.value, saturation, interptime);
                yield return null;
            }
        }
    }


    [ContextMenu("Splash")]
    public void Splash()
    {
        StartCoroutine(Splash(g_color));
    }
    IEnumerator Splash(Color color)
    {
        splash.GetComponent<Image>().color = color;
        var anim = splash.GetComponent<Animation>();
        anim.Play();
        yield return new WaitForSeconds(anim.clip.length);
    }


}