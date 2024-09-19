using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private SceneLoader m_sceneLoader;

    private MainMenuGameState m_mainMenuState;
    private GameplayGameState m_gameplayState;

    private void Awake()
    {
        m_sceneLoader.LoadRequiredScenes();
    }

    private void Start()
    {
        m_mainMenuState = GameObject.FindObjectOfType<MainMenuGameState>();
        m_gameplayState = GameObject.FindObjectOfType<GameplayGameState>();

        m_mainMenuState.PlayGameRequested += OnPlayGameRequested;
        m_mainMenuState.ShowMainMenu();
    }

    private void OnPlayGameRequested()
    {
        m_mainMenuState.PlayGameRequested -= OnPlayGameRequested;
        m_gameplayState.MainMenuRequested += OnMainMenuRequested;
        m_gameplayState.TryAgainRequested += OnTryAgainRequested;
        m_gameplayState.StartNextLevel();
    }

    private void OnTryAgainRequested()
    {
        m_gameplayState.MainMenuRequested -= OnMainMenuRequested;
        m_gameplayState.TryAgainRequested -= OnTryAgainRequested;
        m_sceneLoader.GameplaySceneReloaded += OnGameplaySceneReloaded;
        m_sceneLoader.ReloadGameplayScene();
    }

    private void OnGameplaySceneReloaded()
    {
        m_sceneLoader.GameplaySceneReloaded -= OnGameplaySceneReloaded;
        m_gameplayState = GameObject.FindObjectOfType<GameplayGameState>();
        m_gameplayState.MainMenuRequested += OnMainMenuRequested;
        m_gameplayState.TryAgainRequested += OnTryAgainRequested;
        m_gameplayState.StartNextLevel();
    }

    private void OnMainMenuRequested()
    {
        m_gameplayState.MainMenuRequested -= OnMainMenuRequested;
        m_gameplayState.TryAgainRequested -= OnTryAgainRequested;
        m_mainMenuState.PlayGameRequested += OnPlayGameRequested;
        m_mainMenuState.ShowMainMenu();
    }
}
