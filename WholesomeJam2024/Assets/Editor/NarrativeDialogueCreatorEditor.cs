using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NarrativeDialogueCreator))]
public class NarrativeDialogueCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NarrativeDialogueCreator dialogue = (NarrativeDialogueCreator)target;

        if (GUILayout.Button("Create Dialogue"))
        {
            dialogue.CreateDialogue();

        }
    }

}
