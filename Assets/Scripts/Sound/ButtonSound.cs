
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public enum ButtonSoundType
    {
        Default,    // �⺻ ��ư ����
        Custom,     // Ŀ���� ��ư ����
        Confirm,    // Ȯ��/������ ���� 
        Cancel,     // ���/�ڷΰ���� ����
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