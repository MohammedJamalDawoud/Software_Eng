using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public float totalTime = 15f;
    public float currentTime;
    public List<Sprite> cardSprites;
    public List<int> usedCardIndexes = new List<int>();
    [SerializeField] public List<ColorSelector> colorSelectors = new List<ColorSelector>();
    public int turn = -1;
    [SerializeField] private List<CardsManager> players = new List<CardsManager>();
    [SerializeField] private bool[] readyStates = new bool[4];
    [SerializeField] private GameObject gameOverPanel;
    public bool isWin = false;
    [SerializeField] private Button backButton;
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
    private void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Week3");
    }
    public Sprite GetRandomSprite()
    {
        return cardSprites[GetRandomNumberIndex()];
    }

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

    public void SetReadyState(int index, bool state)
    {
        readyStates[index] = state;
        if (readyStates[0] && readyStates[1] && readyStates[2] && readyStates[3])
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        turn = 3;
        ManageTurn();
    }

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
    public async void ShowGameOverPanel()
    {
        isWin = true;
        gameOverPanel.SetActive(true);
        // int highest = GetHighestNumberofCards();
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
        await FirebaseDatabase.DefaultInstance.RootReference.Child(PlayerPrefs.GetString("username")).Child("score").SetValueAsync(playerScores[0]);
        gameOverPanel.GetComponent<GameOverPanel>().SetUp(playerScores[0], playerScores[1], playerScores[2], playerScores[3]);
    }

    private int GetHighestNumberofCards()
    {
        int highest = 0;
        foreach (CardsManager cardsManager in players)
        {
            if (cardsManager.myCards.Count > highest)
            {
                highest = cardsManager.myCards.Count;
            }
        }
        return highest;
    }

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
        if (currentColor == "B" && colorType == ColorType.Blue)
        {
            return true;
        }
        if (currentColor == "Y" && colorType == ColorType.Yellow)
        {
            return true;
        }
        return false;
    }

    public bool isMyTurn(int index)
    {
        return index == turn;
    }
}
