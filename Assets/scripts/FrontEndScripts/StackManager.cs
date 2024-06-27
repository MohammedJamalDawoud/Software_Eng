using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private Transform stackTransform;
    public Card GetTopOfStack()
    {
        if (stackTransform.childCount > 0)
        {
            return stackTransform.GetChild(stackTransform.childCount - 1).GetComponent<Card>();
        }
        return null;
    }

    public bool CheckTopOfStackisWildorStack()
    {
        Card topCard = GetTopOfStack();
        if (topCard != null)
        {
            if (topCard.cardSpecial == "W" || topCard.cardSpecial == "4")
            {
                return true;
            }
        }
        return false;
    }
}
