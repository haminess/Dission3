using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PianoManager : MonoBehaviour
{
    public AudioSource[] pianoKeys; // 12개의 음을 포함한 배열 (도, 도#, 레, 레#, 미, 파, 파#, 솔, 솔#, 라, 라#, 시)
    public Button[] pianoButtons; // UI 버튼 배열
    public int currentOctave = 4; // 초기 옥타브
    public int minOctave = 1;
    public int maxOctave = 7;

    void Update()
    {
        PlayPianoKeys();
    }

    void PlayPianoKeys()
    {
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
        // 해당하는 피아노 키의 소리를 재생하고 UI 버튼을 눌린 것처럼 보이도록 설정
        pianoButtons[keyIndex].onClick.Invoke();
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
        }
    }

    public void DecreaseOctave()
    {
        if (currentOctave > minOctave)
        {
            currentOctave--;
        }
    }
}
