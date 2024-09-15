using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NarrativeDialogueCreator : MonoBehaviour
{
    public string sceneName = "";
    public int objectID = 1;
    public GameObject objectToSpawn;

    public void CreateDialogue()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Error: Please assign an object to be spawned.");
            return;
        }
        if (sceneName == string.Empty)
        {
            Debug.LogError("Error: Please enter a scene name for your dialogue");
            return;
        }

        GameObject newObject = Instantiate(objectToSpawn, gameObject.transform);
        newObject.name = sceneName + "Dialogue_" + objectID;

        objectID++;
    }
}
