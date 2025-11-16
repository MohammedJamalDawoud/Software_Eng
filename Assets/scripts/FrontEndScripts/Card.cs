using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents a single UNO card in the game.
/// Handles card logic, validation, playing mechanics, and special card effects.
/// Manages card interactions including clicks, validation, and placement on the discard pile.
/// </summary>
public class Card : MonoBehaviour
{
    [Header("Card Properties")]
    /// <summary>
    /// The sprite image displayed on this card.
    /// </summary>
    public Sprite cardSprite;
    
    /// <summary>
    /// Color of the card (R=Red, G=Green, V=Blue/Violet, Y=Yellow).
    /// </summary>
    public string cardColor;
    
    /// <summary>
    /// Number value on the card (0-9), empty for special cards.
    /// </summary>
    public string cardNumber;
    
    /// <summary>
    /// Special card type (W=Wild, 4=Wild Draw 4, 2=Draw 2, S=Skip, R=Reverse).
    /// </summary>
    public string cardSpecial;
    
    /// <summary>
    /// Indicates if this card has special abilities rather than a number.
    /// </summary>
    public bool isSpecial;
    
    /// <summary>
    /// Reference to the CardsManager that owns this card.
    /// </summary>
    public CardsManager cardLocationManager;
    
    private Button cardButton;

    private void Awake()
    {
        cardLocationManager = GetComponentInParent<CardsManager>();
        cardButton = GetComponent<Button>();
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnClickCard);
    }

    /// <summary>
    /// Initializes card properties by parsing the card sprite name.
    /// Extracts color, number, and special card type from the sprite filename.
    /// </summary>
    /// <param name="_cardSprite">The sprite to use for this card.</param>
    public void SetupCard(Sprite _cardSprite)
    {
        cardSprite = _cardSprite;
        string cardName = cardSprite.name;
        if (cardName[5].ToString() == "W")
        {
            cardSpecial = "W";
            isSpecial = true;
        }
        if (cardName[5].ToString() == "+")
        {
            cardSpecial = "4";
            isSpecial = true;
        }
        if (cardName[7].ToString() == "+")
        {
            cardSpecial = "2";
            cardColor = cardName[5].ToString();
            isSpecial = true;
        }
        if (cardName[7].ToString() == "S")
        {
            cardSpecial = "S";
            cardColor = cardName[5].ToString();
            isSpecial = true;
        }
        if (cardName[7].ToString() == "R")
        {
            cardSpecial = "R";
            cardColor = cardName[5].ToString();
            isSpecial = true;
        }
        if (!isSpecial)
        {
            cardColor = cardName[5].ToString();
            cardNumber = cardName[7].ToString();
        }
    }

    /// <summary>
    /// Handles card click events. Validates if the card can be played and executes the play action.
    /// </summary>
    public void OnClickCard()
    {
        if (!GamePlayManager.Instance.isMyTurn(cardLocationManager.id))
        {
            return;
        }

        if (!IsPlayable())
        {
            Debug.Log("Card is not playable!");
            return;
        }
        if (StackManager.Instance.stackCardsCount != 0)
        {
            cardLocationManager.PickCardsForStack(StackManager.Instance.stackCardsCount);
            StackManager.Instance.stackCardsCount = 0;
            return;
        }
        gameObject.GetComponent<Image>().raycastTarget = false;
        StartCoroutine(delayedSetParent());
        cardLocationManager.myCards.Remove(gameObject);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOSizeDelta(cardLocationManager.stackLocation.GetComponent<RectTransform>().sizeDelta, 0.25f);
        transform.DOMove(cardLocationManager.stackLocation.position, 0.5f);
        transform.DOScale(Vector3.one, 0.5f);
        transform.DOLocalRotate(cardLocationManager.stackLocation.localEulerAngles, 0.25f);
        StartCoroutine(cardLocationManager.SortCards(0.25f));
        if (cardSpecial == "2")
        {
            StackManager.Instance.stackCardsCount += 2;
        }
        if (cardSpecial == "4")
        {
            StackManager.Instance.stackCardsCount += 4;
        }
        if (cardSpecial == "W" || cardSpecial == "4")
        {

        }
        else
        {
            GamePlayManager.Instance.ManageTurn();
        }
        if (cardLocationManager.myCards.Count == 0)
        {
            GamePlayManager.Instance.ShowGameOverPanel();
        }
    }

    /// <summary>
    /// Validates whether this card can be legally played based on UNO rules.
    /// A card is playable if it matches the top card's color, number, or special type.
    /// Wild cards can always be played.
    /// </summary>
    /// <returns>True if the card can be played, false otherwise.</returns>
    private bool IsPlayable()
    {
        Card topCard = StackManager.Instance.GetTopOfStack();

        if (topCard == null)
        {
            return true;
        }
        if (cardSpecial == "W" || cardSpecial == "4")
        {
            return true;
        }
        if (cardColor == topCard.cardColor || cardNumber == topCard.cardNumber)
        {
            return true;
        }
        if (cardSpecial == "2" || cardSpecial == "S" || cardSpecial == "R")
        {
            if (cardSpecial == topCard.cardSpecial && cardColor == topCard.cardColor)
            {
                return true;
            }
        }
        if (topCard.cardSpecial == "4" && cardSpecial == "4")
        {
            return true;
        }

        return false;
    }

    IEnumerator delayedSetParent()
    {
        yield return new WaitForSeconds(0.1f);
        transform.SetParent(cardLocationManager.stackLocation);
    }
}
