
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public enum ButtonSoundType
    {
        Default,    // 기본 버튼 사운드
        Custom,     // 커스텀 버튼 사운드
        Confirm,    // 확인/결정용 사운드 
        Cancel,     // 취소/뒤로가기용 사운드
    }

    private AudioSource audioSource;
    [SerializeField] private ButtonSoundType buttonType = ButtonSoundType.Default;

    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private Button button;
    private SoundManager soundManager;

    public ButtonSoundType ButtonType { get => buttonType; set => buttonType = value; }
    public AudioClip HoverSound { get => hoverSound; set => hoverSound = value; }
    public AudioClip ClickSound { get => clickSound; set => clickSound = value; }

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
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
        //audioSource.PlayOneShot(hoverSound);
        audioSource.clip = hoverSound;
        audioSource.Play();
    }

    public void PlayClickSound()
    {
        //audioSource.PlayOneShot(clickSound);
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }
}