using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls AI bot player behavior and decision-making during gameplay.
/// Implements card selection logic and turn timing to simulate realistic player behavior.
/// </summary>
public class BotPlayer : MonoBehaviour
{
    [SerializeField] private CardsManager cardsManager;
    
    /// <summary>
    /// Initializes reference to the bot's card manager.
    /// </summary>
    private void Awake()
    {
        cardsManager = GetComponent<CardsManager>();
    }

    /// <summary>
    /// Initiates the bot's turn if it is currently the bot's turn to play.
    /// </summary>
    public void PlayBot()
    {
        if (isMyBotTurn())
        {
            StartCoroutine(PlayBotTurn());
        }
    }

    /// <summary>
    /// Executes the bot's turn with a randomized delay to simulate thinking time.
    /// Selects and plays a valid card, or draws a card if no valid card is available.
    /// Handles color selection for wild cards.
    /// </summary>
    private IEnumerator PlayBotTurn()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Card topCard = StackManager.Instance.GetTopOfStack();
        Card cardToPlay = getValidCardToPlay(topCard);
        if (cardToPlay != null)
        {
            cardToPlay.OnClickCard();
            if (cardToPlay.cardSpecial == "W" || cardToPlay.cardSpecial == "S")
            {
                yield return new WaitForSeconds(Random.Range(1f, 2f));
                GamePlayManager.Instance.SetThisColorSelector(GamePlayManager.Instance.colorSelectors[Random.Range(0, 4)]);
            }
        }
        else
        {
            cardsManager.PickCards();
        }
    }

    /// <summary>
    /// Finds a valid card in the bot's hand that can be played based on UNO rules.
    /// </summary>
    /// <param name="topCard">The top card on the discard pile to match against.</param>
    /// <returns>A playable card from the bot's hand, or null if no valid card exists.</returns>
    private Card getValidCardToPlay(Card topCard)
    {
        foreach (GameObject cardobj in cardsManager.myCards)
        {
            Card card = cardobj.GetComponent<Card>();
            if (card.cardColor == topCard.cardColor || card.cardNumber == topCard.cardNumber || (card.cardSpecial == topCard.cardSpecial && card.cardSpecial.Length != 0))
            {
                return card;
            }
        }
        return null;
    }

    /// <summary>
    /// Checks if it is currently this bot's turn to play.
    /// </summary>
    /// <returns>True if it is this bot's turn, false otherwise.</returns>
    private bool isMyBotTurn()
    {
        return GamePlayManager.Instance.turn == cardsManager.id;
    }
}
