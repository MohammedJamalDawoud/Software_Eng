using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayer : MonoBehaviour
{
    [SerializeField] private CardsManager cardsManager;
    private void Awake()
    {
        cardsManager = GetComponent<CardsManager>();
    }

    public void PlayBot()
    {
        if (isMyBotTurn())
        {
            StartCoroutine(PlayBotTurn());
        }
    }

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

    private bool isMyBotTurn()
    {
        return GamePlayManager.Instance.turn == cardsManager.id;
    }
}
