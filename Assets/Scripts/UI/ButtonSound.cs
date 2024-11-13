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

        // �⺻ AudioSource ����
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D ����� ����
    }

    // ���콺�� ��ư ���� �ö��� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.volume = hoverVolume;
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // ��ư�� Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.volume = clickVolume;
            audioSource.PlayOneShot(clickSound);
        }
    }

    // Inspector���� ���� ������ ���� public �޼���
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