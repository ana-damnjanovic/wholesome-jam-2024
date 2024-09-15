using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public NarrativeDialogue narrativeDialogue;
    public TextMeshProUGUI uiText;

    [ContextMenu("UpdateText")]
    public void UpdateText()
    {
        uiText.text = narrativeDialogue.dialogue;
    }

}
