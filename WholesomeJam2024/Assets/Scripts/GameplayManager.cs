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
    private float m_delayBetweenHamsterDrops = 0.3f;

    [SerializeField]
    private Transform m_itemStartPosition;
    
    [SerializeField]
    private List<Level> m_levels;

    private int m_levelIndex = 0;

    private List<GameObject> m_spawnedHamsters = new();

    private GameObject m_currentItem;

    private Motorcycle m_motorcycle;
    private Goal m_goal;

    private void Awake()
    {
        m_motorcycle = FindObjectOfType<Motorcycle>();
        m_goal = FindObjectOfType<Goal>();

        StartNextLevel();
    }

    public void StartNextLevel()
    {
        Level currentLevel = m_levels[m_levelIndex];
        GameObject hamster = GameObject.Instantiate(currentLevel.HamsterPrefab, m_hamsterSpawnTransform);
        hamster.GetComponent<GroundCollisionDetector>().GroundCollisionDetected += OnHamsterGroundCollisionDetected;
        hamster.SetActive(false);
        m_spawnedHamsters.Add(hamster);

        m_currentItem = GameObject.Instantiate(currentLevel.ItemPrefab, m_itemStartPosition);
        m_currentItem.SetActive(false);

        m_levelIntroUi.UiClosed += OnIntroUiClosed;
        m_levelIntroUi.ShowUi(currentLevel.HamsterUiSprite, currentLevel.ItemUiSprite, currentLevel.DialogueLine);
    }

    private void OnIntroUiClosed()
    {
        m_levelIntroUi.UiClosed -= OnIntroUiClosed;
        m_currentItem.SetActive(true);

        m_doneBuildingButton.onClick.AddListener(OnDoneButtonClicked);
        m_doneBuildingButton.gameObject.SetActive(true);
    }

    private void OnDoneButtonClicked()
    {
        // only proceed if the player properly attached the item
        if (m_currentItem.tag == "AttachedObject")
        {
            m_doneBuildingButton.onClick.RemoveListener(OnDoneButtonClicked);
            m_doneBuildingButton.gameObject.SetActive(false);
            StartCoroutine(DropHamsters());
        }
    }

    private IEnumerator DropHamsters()
    {
        int numToDrop = m_spawnedHamsters.Count;
        int numDropped = 0;
        while (numDropped < numToDrop)
        {
            yield return new WaitForSeconds(m_delayBetweenHamsterDrops);
            m_spawnedHamsters[numDropped].SetActive(true);
            numDropped++;
        }

        StartCoroutine(WaitAndStartMotorcycle());
    }

    private IEnumerator WaitAndStartMotorcycle()
    {
        yield return new WaitForSeconds(2f);
        m_goal.GoalReached += OnMotorcycleReachedGoal;
        m_motorcycle.StartEngine();
    }

    private void OnMotorcycleReachedGoal()
    {
        m_goal.GoalReached -= OnMotorcycleReachedGoal;
        ResetSpawnedHamsters();
        m_motorcycle.ResetMotorcycle();

        IncrementLevel();
    }

    private void ResetSpawnedHamsters()
    {
        for (int iHamster = 0; iHamster < m_spawnedHamsters.Count; ++iHamster)
        {
            m_spawnedHamsters[iHamster].SetActive(false);
            m_spawnedHamsters[iHamster].transform.position = m_hamsterSpawnTransform.position;
        }
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
        else
        {
            StartNextLevel();
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
