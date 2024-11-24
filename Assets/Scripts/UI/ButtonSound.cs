
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
}