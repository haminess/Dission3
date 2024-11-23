using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PianoManager : MonoBehaviour
{
    public AudioSource[] pianoKeys; // 2옥타브~6옥타브의 '도' 음원 5개
    public Button[] pianoButtons; // UI 버튼 배열
    public Button octaveUpButton; // 옥타브 증가 버튼
    public Button octaveDownButton; // 옥타브 감소 버튼
    public Text octaveDisplayText; // 옥타브 표시용 UI 텍스트

    public int currentOctave = 4; // 초기 옥타브 (기본값 4)
    public int minOctave = 2; // 최소 옥타브
    public int maxOctave = 6; // 최대 옥타브

    private float[] pitchRatios = {
        1.0f,                // 도
        1.05946f,            // 도#
        1.12246f,            // 레
        1.18921f,            // 레#
        1.25992f,            // 미
        1.33484f,            // 파
        1.41421f,            // 파#
        1.49831f,            // 솔
        1.5874f,             // 솔#
        1.68179f,            // 라
        1.7818f,             // 라#
        1.88775f,            // 시
        2.0f                 // 위쪽 도
    };

    void Start()
    {
        // 옥타브 조절 버튼 이벤트 연결
        octaveUpButton.onClick.AddListener(IncreaseOctave);
        octaveDownButton.onClick.AddListener(DecreaseOctave);

        // 초기 옥타브 표시 업데이트
        UpdateOctaveDisplay();

        // 각 피아노 버튼에 클릭 이벤트 등록
        for (int i = 0; i < pianoButtons.Length; i++)
        {
            int index = i; // 지역 변수로 캡처하여 이벤트에 전달
            EventTrigger trigger = pianoButtons[i].gameObject.AddComponent<EventTrigger>();

            // PointerDown 이벤트 등록
            EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            pointerDownEntry.callback.AddListener((data) => { OnKeyPress(index); });
            trigger.triggers.Add(pointerDownEntry);

            // PointerUp 이벤트 등록
            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            pointerUpEntry.callback.AddListener((data) => { OnKeyRelease(index); });
            trigger.triggers.Add(pointerUpEntry);
        }
    }

    void Update()
    {
        PlayPianoKeys();
    }

    void PlayPianoKeys()
    {
        // 키보드 입력 처리
        if (Input.GetKeyDown(KeyCode.A)) { OnKeyPress(0); }
        if (Input.GetKeyDown(KeyCode.W)) { OnKeyPress(1); }
        if (Input.GetKeyDown(KeyCode.S)) { OnKeyPress(2); }
        if (Input.GetKeyDown(KeyCode.E)) { OnKeyPress(3); }
        if (Input.GetKeyDown(KeyCode.D)) { OnKeyPress(4); }
        if (Input.GetKeyDown(KeyCode.F)) { OnKeyPress(5); }
        if (Input.GetKeyDown(KeyCode.T)) { OnKeyPress(6); }
        if (Input.GetKeyDown(KeyCode.G)) { OnKeyPress(7); }
        if (Input.GetKeyDown(KeyCode.Y)) { OnKeyPress(8); }
        if (Input.GetKeyDown(KeyCode.H)) { OnKeyPress(9); }
        if (Input.GetKeyDown(KeyCode.U)) { OnKeyPress(10); }
        if (Input.GetKeyDown(KeyCode.J)) { OnKeyPress(11); }
        if (Input.GetKeyDown(KeyCode.K)) { OnKeyPress(12); }

        if (Input.GetKeyUp(KeyCode.A)) { OnKeyRelease(0); }
        if (Input.GetKeyUp(KeyCode.W)) { OnKeyRelease(1); }
        if (Input.GetKeyUp(KeyCode.S)) { OnKeyRelease(2); }
        if (Input.GetKeyUp(KeyCode.E)) { OnKeyRelease(3); }
        if (Input.GetKeyUp(KeyCode.D)) { OnKeyRelease(4); }
        if (Input.GetKeyUp(KeyCode.F)) { OnKeyRelease(5); }
        if (Input.GetKeyUp(KeyCode.T)) { OnKeyRelease(6); }
        if (Input.GetKeyUp(KeyCode.G)) { OnKeyRelease(7); }
        if (Input.GetKeyUp(KeyCode.Y)) { OnKeyRelease(8); }
        if (Input.GetKeyUp(KeyCode.H)) { OnKeyRelease(9); }
        if (Input.GetKeyUp(KeyCode.U)) { OnKeyRelease(10); }
        if (Input.GetKeyUp(KeyCode.J)) { OnKeyRelease(11); }
        if (Input.GetKeyUp(KeyCode.K)) { OnKeyRelease(12); }
    }

    public void OnKeyPress(int keyIndex)
    {
        if (keyIndex < 0 || keyIndex >= pitchRatios.Length) return;

        // 현재 옥타브의 '도' 음원을 기준으로 다른 음 생성
        int pianoKeyIndex = currentOctave - 2; // 2옥타브를 0번 인덱스로
        if (pianoKeyIndex < 0 || pianoKeyIndex >= pianoKeys.Length) return;

        pianoKeys[pianoKeyIndex].pitch = pitchRatios[keyIndex]; // 피치 적용
        pianoKeys[pianoKeyIndex].Play();

        // 버튼 시각 효과
        pianoButtons[keyIndex].image.color = Color.gray; // 눌린 색상
    }

    public void OnKeyRelease(int keyIndex)
    {
        // 버튼 시각 효과 복구
        pianoButtons[keyIndex].image.color = Color.white; // 원래 색상 복구
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
