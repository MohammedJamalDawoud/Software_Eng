using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Logical Variables")]
    public Sprite cardSprite;
    public string cardColor;
    public string cardNumber;
    public string cardSpecial;
    public bool isSpecial;
    public CardsManager cardLocationManager;
    private Button cardButton;

    private void Awake()
    {
        cardLocationManager = GetComponentInParent<CardsManager>();
        cardButton = GetComponent<Button>();
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnClickCard);
    }

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

    public void OnClickCard()
    {
        if (!GamePlayManager.Instance.isMyTurn(cardLocationManager.id))
        {
            return;
        }

        if (!IsPlayable())
        {
            // If the card is not playable, simply return
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
