using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Central manager for main menu navigation and scene transitions.
/// Implements singleton pattern to ensure only one instance exists throughout the application lifecycle.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the GameManager.
    /// Provides global access to menu navigation and game initialization functionality.
    /// </summary>
    public static GameManager Instance;
    [SerializeField] private Animator unoHeadingAnimator;
    
    [Header("Menu Buttons")]
    [SerializeField] private Button playMenuButton;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button leaderBoardButton;
    
    [Header("Navigation Buttons")]
    [SerializeField] private Button backFromSettingButton;
    [SerializeField] private Button backFromStartButton;
    [SerializeField] private Button backFromLeaderBoardButton;
    
    /// <summary>
    /// Initializes the singleton instance and sets up button event listeners.
    /// Ensures only one GameManager exists in the scene.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        playMenuButton.onClick.RemoveAllListeners();
        settingsMenuButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        leaderBoardButton.onClick.RemoveAllListeners();
        backFromSettingButton.onClick.RemoveAllListeners();
        backFromStartButton.onClick.RemoveAllListeners();
        backFromLeaderBoardButton.onClick.RemoveAllListeners();

        playMenuButton.onClick.AddListener(OnClickPlayMenuButton);
        settingsMenuButton.onClick.AddListener(OnClickSettingsMenuButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
        backFromSettingButton.onClick.AddListener(OnClickBackFromSettingButton);
        backFromStartButton.onClick.AddListener(OnClickBackFromStartButton);
        backFromLeaderBoardButton.onClick.AddListener(OnClickBackFromLeaderBoardButton);
        leaderBoardButton.onClick.AddListener(OnClickLeaderBoardButton);

    }

    /// <summary>
    /// Opens the leaderboard menu when the leaderboard button is clicked.
    /// </summary>
    public void OnClickLeaderBoardButton()
    {
        MenuManager.Instance.OpenMenu("leaderboard");
    }

    /// <summary>
    /// Returns to the main menu from the leaderboard screen.
    /// </summary>
    public void OnClickBackFromLeaderBoardButton()
    {
        MenuManager.Instance.OpenMenu("main");
    }

    /// <summary>
    /// Opens the start/pre-game menu when the play button is clicked.
    /// </summary>
    public void OnClickPlayMenuButton()
    {
        MenuManager.Instance.OpenMenu("start");
    }
    
    /// <summary>
    /// Opens the settings menu and minimizes the UNO heading animation.
    /// </summary>
    public void OnClickSettingsMenuButton()
    {
        MenuManager.Instance.OpenMenu("setting");
        unoHeadingAnimator.SetBool("minimize", true);
    }
    
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
    
    /// <summary>
    /// Returns to the main menu from the settings screen and restores the heading animation.
    /// </summary>
    public void OnClickBackFromSettingButton()
    {
        MenuManager.Instance.OpenMenu("main");
        unoHeadingAnimator.SetBool("minimize", false);
    }

    /// <summary>
    /// Returns to the main menu from the start screen.
    /// </summary>
    public void OnClickBackFromStartButton()
    {
        MenuManager.Instance.OpenMenu("main");
    }

    /// <summary>
    /// Handles the play button click and starts the game.
    /// </summary>
    public void OnClickPlay()
    {
        StartGame();
    }

    /// <summary>
    /// Loads the main game scene to start a new game session.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
