using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private LevelIntroUi m_levelIntroUi;

    [SerializeField]
    private Button m_doneBuildingButton;

    [SerializeField]
    private Transform m_hamsterSpawnTransform;

    [SerializeField]
    private Transform m_itemStartPosition;
    
    [SerializeField]
    private List<Level> m_levels;

    private int m_levelIndex = 0;

    private List<GameObject> m_spawnedHamsters = new();

    private GameObject m_currentHamster;
    private GameObject m_currentItem;

    private void Awake()
    {
        StartNextLevel();
    }

    public void StartNextLevel()
    {
        Level currentLevel = m_levels[m_levelIndex];
        m_currentHamster = GameObject.Instantiate(currentLevel.HamsterPrefab, m_hamsterSpawnTransform);
        m_currentHamster.GetComponent<GroundCollisionDetector>().GroundCollisionDetected += OnHamsterGroundCollisionDetected;
        m_currentHamster.SetActive(false);
        m_spawnedHamsters.Add(m_currentHamster);

        m_currentItem = GameObject.Instantiate(currentLevel.ItemPrefab, m_itemStartPosition);
        m_currentItem.SetActive(false);

        m_levelIntroUi.UiClosed += OnIntroUiClosed;
        m_levelIntroUi.ShowUi(currentLevel.HamsterUiSprite, currentLevel.ItemUiSprite, currentLevel.DialogueLine);
    }

    private void OnIntroUiClosed()
    {
        m_levelIntroUi.UiClosed -= OnIntroUiClosed;
        m_currentItem.SetActive(true);

        m_doneBuildingButton.gameObject.SetActive(true);
        m_doneBuildingButton.onClick.AddListener(OnDoneButtonClicked);
    }

    private void OnDoneButtonClicked()
    {
        // only proceed if the player properly attached the item
        if (m_currentItem.tag == "AttachedObject")
        {
            m_doneBuildingButton.onClick.RemoveListener(OnDoneButtonClicked);
            m_doneBuildingButton.gameObject.SetActive(false);
            m_currentHamster.SetActive(true);
            StartCoroutine(WaitAndStartMotorcycle());
        }
    }

    private IEnumerator WaitAndStartMotorcycle()
    {
        yield return new WaitForSeconds(2f);
        Motorcycle motorcycle = FindObjectOfType<Motorcycle>();
        motorcycle.StartEngine();
    }

    private void OnHamsterGroundCollisionDetected(GroundCollisionDetector detector)
    {
        detector.GroundCollisionDetected -= OnHamsterGroundCollisionDetected;
        Debug.Log("game over!!!");
        // show the game over menu
    }

    public void IncrementLevel()
    {
        m_levelIndex++;
        if (m_levelIndex >= m_levels.Count)
        {
            // player beat all levels
            Debug.Log("WIN!!!");
        }
    }

    private void CleanUp()
    {
        for (int i = 0; i < m_spawnedHamsters.Count; ++i)
        {
            Destroy(m_spawnedHamsters[i]);
        }
        m_spawnedHamsters.Clear();
    }
}
