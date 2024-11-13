using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float hoverVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float clickVolume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // 기본 AudioSource 설정
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D 사운드로 설정
    }

    // 마우스가 버튼 위에 올라갔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.volume = hoverVolume;
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // 버튼을 클릭했을 때
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.volume = clickVolume;
            audioSource.PlayOneShot(clickSound);
        }
    }

    // Inspector에서 사운드 변경을 위한 public 메서드
    public void SetHoverSound(AudioClip newSound)
    {
        hoverSound = newSound;
    }

    public void SetClickSound(AudioClip newSound)
    {
        clickSound = newSound;
    }

    public void SetHoverVolume(float volume)
    {
        hoverVolume = Mathf.Clamp01(volume);
    }

    public void SetClickVolume(float volume)
    {
        clickVolume = Mathf.Clamp01(volume);
    }
}