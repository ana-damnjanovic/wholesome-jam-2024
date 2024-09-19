using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Button m_tryAgainButton;

    [SerializeField]
    private Button m_mainMenuButton;

    public event System.Action TryAgainRequested = delegate { };
    public event System.Action MainMenuRequested = delegate { };

    public void ShowUi()
    {
        m_canvas.enabled = true;
        m_tryAgainButton.onClick.AddListener(OnTryAgainClicked);
        m_mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    private void OnTryAgainClicked()
    {
        m_tryAgainButton.onClick.RemoveListener(OnTryAgainClicked);
        m_mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
        TryAgainRequested.Invoke();
        m_canvas.enabled = false;
    }

    private void OnMainMenuClicked()
    {
        m_tryAgainButton.onClick.RemoveListener(OnTryAgainClicked);
        m_mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
        MainMenuRequested.Invoke();
        m_canvas.enabled = false;
    }
}
