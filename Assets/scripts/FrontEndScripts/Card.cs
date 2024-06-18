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
    public CardsManager cardLocationManager;
    private Button cardButton;
    // public TMP_Text leftAbilityText;
    // public TMP_Text rightAbilityText;
    private void Awake()
    {
        cardLocationManager = GetComponentInParent<CardsManager>();
        cardButton = GetComponent<Button>();
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnClickCard);
    }

    public void OnClickCard()
    {
        // if (GameManager.Instance.isMyTurn(cardLocationManager.id) == false)
        // {

        //     return;
        // }
        gameObject.GetComponent<Image>().raycastTarget = false;
        StartCoroutine(delayedSetParent());
        cardLocationManager.myCards.Remove(gameObject);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOSizeDelta(cardLocationManager.stackLocation.GetComponent<RectTransform>().sizeDelta, 0.25f);
        transform.DOMove(cardLocationManager.stackLocation.position, 0.5f);
        transform.DOScale(Vector3.one, 0.5f);
        transform.DOLocalRotate(cardLocationManager.stackLocation.localEulerAngles, 0.25f);
        StartCoroutine(cardLocationManager.SortCards(0.25f));
        // GameManager.Instance.ManageTurn();
        // string leftAbility = leftAbilityText.text;
        // string rightAbility = rightAbilityText.text;
        // GameManager.Instance.LastCardSetup(leftAbility, rightAbility)
    }
    IEnumerator delayedSetParent()
    {
        yield return new WaitForSeconds(0.1f);
        transform.SetParent(cardLocationManager.stackLocation);
    }

}
