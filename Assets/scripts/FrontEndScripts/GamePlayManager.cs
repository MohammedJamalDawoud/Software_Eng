using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public List<Sprite> cardSprites;
    public List<int> usedCardIndexes = new List<int>();
    public int turn = -1;
    [SerializeField] private List<CardsManager> players = new List<CardsManager>();
    [SerializeField] private bool[] readyStates = new bool[4];
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
        }
    }


    public bool isMyTurn(int index)
    {
        return index == turn;
    }
}
