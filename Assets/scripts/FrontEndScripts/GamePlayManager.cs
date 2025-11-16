using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Core gameplay manager that handles turn-based gameplay, game rules, win conditions, and score tracking.
/// Manages the game flow, player turns, card validation, and integration with Firebase for score persistence.
/// </summary>
public class GamePlayManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the GamePlayManager.
    /// Provides global access to gameplay functionality.
    /// </summary>
    public static GamePlayManager Instance;
    [Header("Game Settings")]
    /// <summary>
    /// Total time in seconds each player has per turn.
    /// </summary>
    public float totalTime = 15f;
    
    /// <summary>
    /// List of card sprite assets used in the game.
    /// </summary>
    public List<Sprite> cardSprites;
    
    /// <summary>
    /// Tracks which card indices have been used to prevent duplicate cards.
    /// </summary>
    public List<int> usedCardIndexes = new List<int>();
    
    /// <summary>
    /// Available color selector buttons for wild card color selection.
    /// </summary>
    [SerializeField] public List<ColorSelector> colorSelectors = new List<ColorSelector>();
    
    /// <summary>
    /// Current player turn index (0-3). -1 indicates game not started.
    /// </summary>
    public int turn = -1;
    
    /// <summary>
    /// List of all player card managers in the game (typically 4 players).
    /// </summary>
    [SerializeField] private List<CardsManager> players = new List<CardsManager>();
    
    /// <summary>
    /// Tracks ready state for each player during game initialization.
    /// </summary>
    [SerializeField] private bool[] readyStates = new bool[4];
    
    /// <summary>
    /// Panel displayed when the game ends.
    /// </summary>
    [SerializeField] private GameObject gameOverPanel;
    
    /// <summary>
    /// Flag indicating if a player has won the game.
    /// </summary>
    public bool isWin = false;
    
    [SerializeField] private Button backButton;
    /// <summary>
    /// Initializes the singleton instance and sets up UI listeners.
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
        backButton.onClick.AddListener(BackToMenu);
    }
    
    /// <summary>
    /// Returns to the main menu scene.
    /// </summary>
    private void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Week3");
    }
    
    /// <summary>
    /// Retrieves a random card sprite that hasn't been used in the current game cycle.
    /// </summary>
    /// <returns>A random card sprite from the available card sprites.</returns>
    public Sprite GetRandomSprite()
    {
        return cardSprites[GetRandomNumberIndex()];
    }

    /// <summary>
    /// Generates a random card index that hasn't been used yet.
    /// Resets the used cards list when all cards have been used.
    /// </summary>
    /// <returns>A unique random index for a card sprite.</returns>
    private int GetRandomNumberIndex()
    {
        if (usedCardIndexes.Count == cardSprites.Count)
        {
            usedCardIndexes.Clear();
        }
        int randomIndex = Random.Range(0, cardSprites.Count);
        if (usedCardIndexes.Contains(randomIndex))
        {
            return GetRandomNumberIndex();
        }
        usedCardIndexes.Add(randomIndex);
        return randomIndex;
    }

    /// <summary>
    /// Sets the ready state for a player during game initialization.
    /// Starts the game when all players are ready.
    /// </summary>
    /// <param name="index">Player index (0-3).</param>
    /// <param name="state">Ready state of the player.</param>
    public void SetReadyState(int index, bool state)
    {
        readyStates[index] = state;
        if (readyStates[0] && readyStates[1] && readyStates[2] && readyStates[3])
        {
            StartGame();
        }
    }

    /// <summary>
    /// Initializes the game by setting the first turn and starting turn management.
    /// </summary>
    private void StartGame()
    {
        turn = 3;
        ManageTurn();
    }

    /// <summary>
    /// Manages turn progression, activates the current player's timer, and handles bot turns.
    /// Updates card interactivity based on whose turn it is.
    /// </summary>
    public void ManageTurn()
    {
        if (isWin)
        {
            return;
        }
        turn = (turn + 1) % 4;
        foreach (CardsManager cardsManager in players)
        {
            bool isMyTurn = cardsManager.GetComponent<CardsManager>().id == turn;
            foreach (GameObject cardHover in cardsManager.myCards)
            {
                CardHoverScript cardHoverScript = cardHover.GetComponent<CardHoverScript>();
                cardHoverScript.isMyTurn = isMyTurn;
            }
            if (isMyTurn)
            {
                cardsManager.StartTimer(totalTime);
                cardsManager.TryGetComponent(out BotPlayer botPlayer);
                if (botPlayer != null)
                {
                    botPlayer.PlayBot();
                }
            }
            else
            {
                cardsManager.StopTimer();
            }
        }
        SetSelectedColors();
    }

    /// <summary>
    /// Updates the visual state of color selectors based on the top card's color.
    /// </summary>
    private void SetSelectedColors()
    {
        string color = StackManager.Instance.GetTopOfStack().cardColor;
        foreach (ColorSelector colorSelector in colorSelectors)
        {
            if (checkColor(color, colorSelector.color))
            {
                colorSelector.isSelected = true;
            }
            else
            {
                colorSelector.isSelected = false;
            }
        }
    }

    /// <summary>
    /// Sets the selected color for wild cards and progresses the turn.
    /// </summary>
    /// <param name="colorSelector">The color selector button that was clicked.</param>
    public void SetThisColorSelector(ColorSelector colorSelector)
    {
        foreach (ColorSelector selector in colorSelectors)
        {
            selector.isSelected = false;
        }
        colorSelector.isSelected = true;
        Card topCard = StackManager.Instance.GetTopOfStack();
        topCard.cardColor = getColorFromSelector(colorSelector);
        ManageTurn();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowGameOverPanel();
        }
    }
    /// <summary>
    /// Displays the game over panel, calculates player scores, and updates Firebase with the winner's score.
    /// Scoring: 1000 points for empty hand, 500 for 2-5 cards, 100 for 6+ cards.
    /// </summary>
    public async void ShowGameOverPanel()
    {
        isWin = true;
        gameOverPanel.SetActive(true);
        
        int[] playerScores = new int[4];
        int i = 0;
        foreach (CardsManager cardsManager in players)
        {
            if (cardsManager.myCards.Count == 0)
            {
                playerScores[i] = 1000;
            }
            else if (cardsManager.myCards.Count >= 2 && cardsManager.myCards.Count <= 5)
            {
                playerScores[i] = 500;
            }
            else
            {
                playerScores[i] = 100;
            }
            i++;
        }
        PlayerPrefs.SetInt("myscore", playerScores[0]);
        
        // Update Firebase with the player's new score
        await FirebaseDatabase.DefaultInstance.RootReference.Child(PlayerPrefs.GetString("username")).Child("score").GetValueAsync().ContinueWithOnMainThread(async task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                int score = int.Parse(task.Result.Value.ToString());
                await FirebaseDatabase.DefaultInstance.RootReference.Child(PlayerPrefs.GetString("username")).Child("score").SetValueAsync(score + playerScores[0]);
            }
        });

        gameOverPanel.GetComponent<GameOverPanel>().SetUp(playerScores[0], playerScores[1], playerScores[2], playerScores[3]);
    }

    /// <summary>
    /// Converts a ColorType enum to its string representation used in card logic.
    /// </summary>
    /// <param name="colorSelector">The color selector containing the color type.</param>
    /// <returns>Single character string representing the color (R, G, V, Y).</returns>
    private string getColorFromSelector(ColorSelector colorSelector)
    {
        if (colorSelector.color == ColorType.Red)
        {
            return "R";
        }
        if (colorSelector.color == ColorType.Green)
        {
            return "G";
        }
        if (colorSelector.color == ColorType.Blue)
        {
            return "V";
        }
        if (colorSelector.color == ColorType.Yellow)
        {
            return "Y";
        }
        return "";
    }
    
    /// <summary>
    /// Checks if a string color matches a ColorType enum value.
    /// </summary>
    /// <param name="currentColor">String representation of color (R, G, V, Y).</param>
    /// <param name="colorType">ColorType enum to compare against.</param>
    /// <returns>True if colors match, false otherwise.</returns>
    private bool checkColor(string currentColor, ColorType colorType)
    {
        if (currentColor == "R" && colorType == ColorType.Red)
        {
            return true;
        }
        if (currentColor == "G" && colorType == ColorType.Green)
        {
            return true;
        }
        if (currentColor == "V" && colorType == ColorType.Blue)
        {
            return true;
        }
        if (currentColor == "Y" && colorType == ColorType.Yellow)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if it is currently the specified player's turn.
    /// </summary>
    /// <param name="index">Player index to check (0-3).</param>
    /// <returns>True if it is the specified player's turn, false otherwise.</returns>
    public bool isMyTurn(int index)
    {
        return index == turn;
    }
}
