using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public int id = -1;
    public static CardsManager Instance;
    [Header("Card Prefab")]
    [SerializeField] private GameObject cardPrefab;
    public Image timerFill;
    [Header("Card Parent")]
    [SerializeField] private Transform cardParent;
    public List<GameObject> myCards;
    public int totalCards = 5;
    [Header("CardProperty Variables")]
    public Transform stackLocation;
    public Transform pickCardsPosition;
    public float[] xBasePosition = new float[] { 120f, 80f, 60f };
    public Vector2 smallCards = new Vector2(150, 225);
    public Vector2 medCards = new Vector2(200, 300);
    public Vector2 bigCards = new Vector2(225, 325);
    public int[] sizeVariationTrigger = new int[] { 10, 15, 20 };
    public float cardRotation;
    public bool isSetuped = false;
    public int score;
    private void Start()
    {
        StartCoroutine(GenerateCards(totalCards));
    }
    public IEnumerator GenerateCards(int totalCards)
    {
        Vector2 targetSize;
        int sizeIndex;
        if (totalCards <= sizeVariationTrigger[0])
        {
            targetSize = bigCards;
            sizeIndex = 0;
        }
        else
        {
            if (totalCards <= sizeVariationTrigger[1])
            {
                targetSize = medCards;
                sizeIndex = 1;
            }
            else
            {
                targetSize = smallCards;
                sizeIndex = 2;
            }
        }
        float startPosi = -((float)Math.Floor((double)(totalCards / 2)) * xBasePosition[sizeIndex]);
        for (int i = 0; i < totalCards; i++)
        {
            GameObject Card = Instantiate(cardPrefab, cardParent);
            Card.transform.localScale = Vector3.one;
            Card cardinfo = Card.GetComponent<Card>();
            // cardinfo.leftAbilityText.text = UnityEngine.Random.Range(10, 100).ToString("N0");
            // cardinfo.rightAbilityText.text = UnityEngine.Random.Range(10, 100).ToString("N0");
            Card.GetComponent<Image>().sprite = GamePlayManager.Instance.GetRandomSprite();
            cardinfo.SetupCard(Card.GetComponent<Image>().sprite);
            Card.transform.position = pickCardsPosition.position;
            Card.transform.DOLocalMove(new Vector3(startPosi, 0, 0), 0.5f);
            RectTransform rectTransform = Card.GetComponentInChildren<RectTransform>();
            rectTransform.DOSizeDelta(targetSize, 0.25f);
            Card.transform.localEulerAngles = Vector3.zero;
            startPosi += xBasePosition[sizeIndex];
            Card.transform.DOLocalRotate(new Vector3(0, 0, cardRotation), 0.25f);
            myCards.Add(Card);
            yield return new WaitForSeconds(0.2f);
        }
        isSetuped = true;
        GamePlayManager.Instance.SetReadyState(id, true);
    }


    public void PickCards()
    {
        if (!GamePlayManager.Instance.isMyTurn(id))
        {
            return;
        }
        GameObject Card = Instantiate(cardPrefab, cardParent);
        Card.transform.localScale = Vector3.one;
        Card.transform.position = pickCardsPosition.position;
        Card.GetComponent<Image>().sprite = GamePlayManager.Instance.GetRandomSprite();
        Card.GetComponent<Card>().SetupCard(Card.GetComponent<Image>().sprite);
        // Card.transform.DOLocalMove(new Vector3(startPosi, 0, 0), 0.5f);
        RectTransform rectTransform = Card.GetComponentInChildren<RectTransform>();
        // rectTransform.DOSizeDelta(targetSize, 0.25f);
        Card.transform.localEulerAngles = Vector3.zero;

        Card.transform.DOLocalRotate(new Vector3(0, 0, cardRotation), 0.25f);
        myCards.Add(Card);
        StartCoroutine(SortCards(0.1f));
        GamePlayManager.Instance.ManageTurn();
    }

    public void PickCardsForStack(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject Card = Instantiate(cardPrefab, cardParent);
            Card.transform.localScale = Vector3.one;
            Card.transform.position = pickCardsPosition.position;
            Card.GetComponent<Image>().sprite = GamePlayManager.Instance.GetRandomSprite();
            Card.GetComponent<Card>().SetupCard(Card.GetComponent<Image>().sprite);
            // Card.transform.DOLocalMove(new Vector3(startPosi, 0, 0), 0.5f);
            RectTransform rectTransform = Card.GetComponentInChildren<RectTransform>();
            // rectTransform.DOSizeDelta(targetSize, 0.25f);
            Card.transform.localEulerAngles = Vector3.zero;

            Card.transform.DOLocalRotate(new Vector3(0, 0, cardRotation), 0.25f);
            myCards.Add(Card);
            StartCoroutine(SortCards(0.1f));
        }
    }

    public IEnumerator SortCards(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector2 targetSize;
        int sizeIndex;
        if (myCards.Count <= sizeVariationTrigger[0])
        {
            targetSize = bigCards;
            sizeIndex = 0;
        }
        else
        {
            if (myCards.Count <= sizeVariationTrigger[1])
            {
                targetSize = medCards;
                sizeIndex = 1;
            }
            else
            {
                targetSize = smallCards;
                sizeIndex = 2;
            }
        }
        float startPosi = -((float)Math.Floor((double)(myCards.Count / 2)) * xBasePosition[sizeIndex]);
        for (int i = 0; i < myCards.Count; i++)
        {
            myCards[i].transform.DOLocalMove(new Vector3(startPosi, 0, 0), 0.5f);
            // myCards[i].transform.localEulerAngles = Vector3.zero;
            myCards[i].transform.DOLocalRotate(new Vector3(0, 0, cardRotation), 0.25f);
            RectTransform rectTransform = myCards[i].GetComponentInChildren<RectTransform>();
            rectTransform.DOSizeDelta(targetSize, 0.25f);
            startPosi += xBasePosition[sizeIndex];
        }
    }

    private Tween timerTween;
    public void StartTimer(float time)
    {
        if (timerTween != null && timerTween.IsActive())
        {
            timerTween.Kill();
        }
        timerFill.fillAmount = 0;
        timerTween = timerFill.DOFillAmount(1, time).OnComplete(() =>
           {
               PickCards();
           });
    }

    public void StopTimer()
    {
        if (timerTween != null && timerTween.IsActive())
        {
            timerTween.Kill();
        }
        timerFill.DOFillAmount(0, 0.25f);
    }

}
