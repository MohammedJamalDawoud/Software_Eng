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
