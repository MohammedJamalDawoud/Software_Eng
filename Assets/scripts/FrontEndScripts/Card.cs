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
        gameObject.GetComponent<Image>().raycastTarget = false;
        StartCoroutine(delayedSetParent());
        cardLocationManager.myCards.Remove(gameObject);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOSizeDelta(cardLocationManager.stackLocation.GetComponent<RectTransform>().sizeDelta, 0.25f);
        transform.DOMove(cardLocationManager.stackLocation.position, 0.5f);
        transform.DOScale(Vector3.one, 0.5f);
        transform.DOLocalRotate(cardLocationManager.stackLocation.localEulerAngles, 0.25f);
        StartCoroutine(cardLocationManager.SortCards(0.25f));
        GamePlayManager.Instance.ManageTurn();
    }

    IEnumerator delayedSetParent()
    {
        yield return new WaitForSeconds(0.1f);
        transform.SetParent(cardLocationManager.stackLocation);
    }
}
