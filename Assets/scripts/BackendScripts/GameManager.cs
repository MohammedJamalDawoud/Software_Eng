using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // this is the static instance of the GameManager Class
    [SerializeField] private Animator unoHeadingAnimator;
    [Header("Menu Buttons")]
    [SerializeField] private Button playMenuButton;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button leaderBoardButton;
    [Header("Back")]
    [SerializeField] private Button backFromSettingButton;
    [SerializeField] private Button backFromStartButton;
    [SerializeField] private Button backFromLeaderBoardButton;
    private void Awake()
    {
        if (Instance == null) // if the instance of the GameManager is null then assign this to the instance
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject); // if the instance of the GameManager is not null then destroy this object
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

    public void OnClickLeaderBoardButton()
    {
        MenuManager.Instance.OpenMenu("leaderboard");
    }

    public void OnClickBackFromLeaderBoardButton()
    {
        MenuManager.Instance.OpenMenu("main");
    }

    public void OnClickPlayMenuButton()
    {
        MenuManager.Instance.OpenMenu("start");
    }
    public void OnClickSettingsMenuButton()
    {
        MenuManager.Instance.OpenMenu("setting");
        unoHeadingAnimator.SetBool("minimize", true);
    }
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
    public void OnClickBackFromSettingButton()
    {
        MenuManager.Instance.OpenMenu("main");
        unoHeadingAnimator.SetBool("minimize", false);
    }

    public void OnClickBackFromStartButton()
    {
        MenuManager.Instance.OpenMenu("main");
    }

    public void OnClickPlay()
    {
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
