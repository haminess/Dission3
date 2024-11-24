<<<<<<< Updated upstream
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
=======
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public enum ButtonSoundType
    {
        Default,    // �⺻ ��ư ����
        Confirm,    // Ȯ��/������ ���� 
        Cancel      // ���/�ڷΰ���� ����
    }

    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    [SerializeField] private ButtonSoundType buttonType = ButtonSoundType.Default;

    private static AudioClip confirmHoverSound;
    private static AudioClip cancelHoverSound;
    private static AudioClip confirmClickSound;
    private static AudioClip cancelClickSound;

    private Button button;
    private SoundManager soundManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        soundManager = FindObjectOfType<SoundManager>();

        // Click �̺�Ʈ ���
        button.onClick.AddListener(PlayClickSound);
    }

    private void LoadDefaultSounds()
    {
        // �������� ���� �⺻���� �޶��� ��� �ڵ带 �ۼ��ϼ���.

        // �����ͺ��̽��� ������ �⺻�� ������ �ҷ����� ��� ���.
    }

    public void PlayHoverSound()
    {
        if (soundManager == null) 
            return;

        switch (buttonType)
        {
            case ButtonSoundType.Default:
                soundManager.PlayEffect(hoverSound);
                break;
            case ButtonSoundType.Confirm:
                soundManager.PlayEffect(confirmHoverSound);
                break;
            case ButtonSoundType.Cancel:
                soundManager.PlayEffect(cancelHoverSound);
                break;
        }
    }

    public void PlayClickSound()
    {
        if (soundManager == null) return;

        switch (buttonType)
        {
            case ButtonSoundType.Default:
                soundManager.PlayEffect(clickSound);
                break;
            case ButtonSoundType.Confirm:
                soundManager.PlayEffect(confirmClickSound);
                break;
            case ButtonSoundType.Cancel:
                soundManager.PlayEffect(cancelClickSound);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

>>>>>>> Stashed changes
}