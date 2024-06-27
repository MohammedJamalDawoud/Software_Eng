using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public float totalTime = 15f;
    public float currentTime;
    public List<Sprite> cardSprites;
    public List<int> usedCardIndexes = new List<int>();
    [SerializeField] private List<ColorSelector> colorSelectors = new List<ColorSelector>();
    public int turn = -1;
    [SerializeField] private List<CardsManager> players = new List<CardsManager>();
    [SerializeField] private bool[] readyStates = new bool[4];
    [SerializeField] private GameObject gameOverPanel;
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
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
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
