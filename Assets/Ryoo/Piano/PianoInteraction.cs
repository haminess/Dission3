using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoInteraction : MonoBehaviour
{
    public GameObject pianoMiniGameUI; // 피아노 미니게임 UI

    private bool isPianoMiniGameActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TogglePianoMiniGame();
        }
    }

    void TogglePianoMiniGame()
    {
        isPianoMiniGameActive = !isPianoMiniGameActive;
        pianoMiniGameUI.SetActive(isPianoMiniGameActive);

        Time.timeScale = isPianoMiniGameActive ? 0f : 1f; // 게임 일시정지 또는 재개
    }
}
