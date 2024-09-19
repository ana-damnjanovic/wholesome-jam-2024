using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGameState : MonoBehaviour
{
    public event System.Action PlayGameRequested = delegate { };

    [SerializeField]
    private Canvas m_canvas;


    [SerializeField]
    private Button m_playGameButton;

    public void ShowMainMenu() {
        m_playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        m_canvas.enabled = true;
    }


    private void OnPlayGameButtonClicked()
    {
        m_playGameButton.onClick.RemoveListener(OnPlayGameButtonClicked);
        PlayGameRequested.Invoke();
        m_canvas.enabled = false;
    }
}
