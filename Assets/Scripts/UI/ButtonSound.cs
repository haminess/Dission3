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
=======
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public enum ButtonSoundType
    {
        Default,    // 기본 버튼 사운드
        Confirm,    // 확인/결정용 사운드 
        Cancel      // 취소/뒤로가기용 사운드
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

        // Click 이벤트 등록
        button.onClick.AddListener(PlayClickSound);
    }

    private void LoadDefaultSounds()
    {
        // 설정마다 사운드 기본값이 달라질 경우 코드를 작성하세요.

        // 데이터베이스에 설정된 기본값 정보로 불러오는 방식 사용.
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