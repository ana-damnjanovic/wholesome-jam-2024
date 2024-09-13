using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    private GameObject m_hamsterPrefab;

    [SerializeField]
    private Sprite m_hamsterUiSprite;

    [SerializeField]
    private GameObject m_itemPrefab;

    [SerializeField]
    private Sprite m_itemUiSprite;

    [SerializeField]
    private string m_dialogueLine;

    public GameObject HamsterPrefab => m_hamsterPrefab;

    public Sprite HamsterUiSprite => m_hamsterUiSprite;

    public GameObject ItemPrefab => m_itemPrefab;

    public Sprite ItemUiSprite=> m_itemUiSprite;

    public string DialogueLine => m_dialogueLine;
}
