using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PianoManager : MonoBehaviour
{
    public AudioSource[] pianoKeys; // 12개의 음을 포함한 배열 (도, 도#, 레, 레#, 미, 파, 파#, 솔, 솔#, 라, 라#, 시)
    public Button[] pianoButtons; // UI 버튼 배열
    public Button octaveUpButton; // 옥타브 증가 버튼
    public Button octaveDownButton; // 옥타브 감소 버튼
    public Text octaveDisplayText; // 옥타브 표시용 UI 텍스트

    public int currentOctave = 4; // 초기 옥타브 (기본값 4)
    public int minOctave = 2; // 최소 옥타브
    public int maxOctave = 6; // 최대 옥타브

    void Start()
    {
        // 옥타브 조절 버튼 이벤트 연결
        octaveUpButton.onClick.AddListener(IncreaseOctave);
        octaveDownButton.onClick.AddListener(DecreaseOctave);

        // 초기 옥타브 표시 업데이트
        UpdateOctaveDisplay();
    }

    void Update()
    {
        PlayPianoKeys();
    }

    void PlayPianoKeys()
    {
        // 각 키를 눌렀을 때 현재 옥타브에 맞춰 소리를 재생
        if (Input.GetKeyDown(KeyCode.A)) { TriggerKey(0); }  // 도(C)
        if (Input.GetKeyDown(KeyCode.W)) { TriggerKey(1); }  // 도#(C#)
        if (Input.GetKeyDown(KeyCode.S)) { TriggerKey(2); }  // 레(D)
        if (Input.GetKeyDown(KeyCode.E)) { TriggerKey(3); }  // 레#(D#)
        if (Input.GetKeyDown(KeyCode.D)) { TriggerKey(4); }  // 미(E)
        if (Input.GetKeyDown(KeyCode.F)) { TriggerKey(5); }  // 파(F)
        if (Input.GetKeyDown(KeyCode.T)) { TriggerKey(6); }  // 파#(F#)
        if (Input.GetKeyDown(KeyCode.G)) { TriggerKey(7); }  // 솔(G)
        if (Input.GetKeyDown(KeyCode.Y)) { TriggerKey(8); }  // 솔#(G#)
        if (Input.GetKeyDown(KeyCode.H)) { TriggerKey(9); }  // 라(A)
        if (Input.GetKeyDown(KeyCode.U)) { TriggerKey(10); } // 라#(A#)
        if (Input.GetKeyDown(KeyCode.J)) { TriggerKey(11); } // 시(B)
        if (Input.GetKeyDown(KeyCode.K)) { TriggerKey(12); } // 도(C)

        // 키가 떼어질 때 시각적 상태를 원래대로 복구
        if (Input.GetKeyUp(KeyCode.A)) { ReleaseKey(0); }
        if (Input.GetKeyUp(KeyCode.W)) { ReleaseKey(1); }
        if (Input.GetKeyUp(KeyCode.S)) { ReleaseKey(2); }
        if (Input.GetKeyUp(KeyCode.E)) { ReleaseKey(3); }
        if (Input.GetKeyUp(KeyCode.D)) { ReleaseKey(4); }
        if (Input.GetKeyUp(KeyCode.F)) { ReleaseKey(5); }
        if (Input.GetKeyUp(KeyCode.T)) { ReleaseKey(6); }
        if (Input.GetKeyUp(KeyCode.G)) { ReleaseKey(7); }
        if (Input.GetKeyUp(KeyCode.Y)) { ReleaseKey(8); }
        if (Input.GetKeyUp(KeyCode.H)) { ReleaseKey(9); }
        if (Input.GetKeyUp(KeyCode.U)) { ReleaseKey(10); }
        if (Input.GetKeyUp(KeyCode.J)) { ReleaseKey(11); }
        if (Input.GetKeyUp(KeyCode.K)) { ReleaseKey(12); }
    }

    void TriggerKey(int keyIndex)
    {
        // 현재 옥타브에 맞춰 피아노 키의 소리를 조정하고 재생
        pianoKeys[keyIndex].pitch = Mathf.Pow(2f, currentOctave - 4); // 주파수 조정
        pianoKeys[keyIndex].Play();
        PressButton(pianoButtons[keyIndex]);
    }

    void PressButton(Button button)
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
    }

    void ReleaseKey(int keyIndex)
    {
        ReleaseButton(pianoButtons[keyIndex]);
    }

    void ReleaseButton(Button button)
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }

    public void IncreaseOctave()
    {
        if (currentOctave < maxOctave)
        {
            currentOctave++;
            UpdateOctaveDisplay();
        }
    }

    public void DecreaseOctave()
    {
        if (currentOctave > minOctave)
        {
            currentOctave--;
            UpdateOctaveDisplay();
        }
    }

    void UpdateOctaveDisplay()
    {
        // 현재 옥타브를 4옥타브 기준으로 표시 (0은 기본값, -1, +1 등으로 표시)
        int displayValue = currentOctave - 4;
        octaveDisplayText.text = displayValue.ToString();
    }
}
