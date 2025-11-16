using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the discard pile (stack) where played cards are placed.
/// Provides methods to access the top card and check card properties.
/// </summary>
public class StackManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the StackManager.
    /// Provides global access to discard pile functionality.
    /// </summary>
    public static StackManager Instance;
    
    /// <summary>
    /// Count of cards that need to be drawn due to stacking effects (Draw 2, Draw 4).
    /// </summary>
    public int stackCardsCount = 0;
    
    [SerializeField] private Transform stackTransform;
    
    /// <summary>
    /// Initializes the singleton instance.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }
    
    /// <summary>
    /// Retrieves the top card from the discard pile.
    /// </summary>
    /// <returns>The Card component of the top card, or null if the stack is empty.</returns>
    public Card GetTopOfStack()
    {
        if (stackTransform.childCount > 0)
        {
            return stackTransform.GetChild(stackTransform.childCount - 1).GetComponent<Card>();
        }
        return null;
    }

    /// <summary>
    /// Checks if the top card is a wild card (W) or wild draw 4 (4).
    /// These cards require color selection before the next turn can proceed.
    /// </summary>
    /// <returns>True if the top card is a wild card requiring color selection, false otherwise.</returns>
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
