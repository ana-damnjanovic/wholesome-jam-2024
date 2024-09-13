using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroUi : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Image m_hamsterImage;

    [SerializeField]
    private Image m_itemImage;

    [SerializeField]
    private TextMeshProUGUI m_dialogueText;

    [SerializeField]
    private float m_uiTimer = 3f;

    public event System.Action UiClosed = delegate { };

    public void ShowUi(Sprite hamsterSprite, Sprite itemSprite, string text)
    {
        m_hamsterImage.sprite = hamsterSprite;
        m_itemImage.sprite = itemSprite;
        m_dialogueText.text = text;
        m_canvas.enabled = true;

        StartCoroutine(WaitAndHideUi(m_uiTimer));
    }

    public void HideUi()
    {
        m_canvas.enabled = false;
    }

    private IEnumerator WaitAndHideUi(float time)
    {
        yield return new WaitForSeconds(time);
        HideUi();
        UiClosed.Invoke();
    }
}
