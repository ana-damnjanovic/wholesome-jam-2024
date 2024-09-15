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

    public event System.Action TryAgainRequested = delegate { };

    public void ShowUi()
    {
        m_canvas.enabled = true;
        m_tryAgainButton.onClick.AddListener(OnTryAgainClicked);
    }

    private void OnTryAgainClicked()
    {
        TryAgainRequested.Invoke();
        m_canvas.enabled = false;
    }
}
