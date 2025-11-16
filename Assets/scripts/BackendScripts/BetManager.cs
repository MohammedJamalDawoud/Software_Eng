using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages betting amounts for game sessions.
/// Provides functionality to increment and decrement bet values within defined limits.
/// Implements singleton pattern for global access.
/// </summary>
public class BetManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the BetManager.
    /// </summary>
    public static BetManager Instance;
    
    /// <summary>
    /// Current bet amount.
    /// </summary>
    public int betAmount = 1000;
    
    /// <summary>
    /// Maximum allowed bet amount.
    /// </summary>
    public int maxBetAmount = 10000;
    
    /// <summary>
    /// Minimum allowed bet amount.
    /// </summary>
    public int minBetAmount = 1000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Increments the bet amount by 1000 if below the maximum limit.
    /// </summary>
    /// <returns>The updated bet amount.</returns>
    public int OnIncreBet()
    {
        if (betAmount < maxBetAmount)
        {
            betAmount += 1000;
        }
        return betAmount;
    }

    /// <summary>
    /// Decrements the bet amount by 1000 if above the minimum limit.
    /// </summary>
    /// <returns>The updated bet amount.</returns>
    public int OnDecreBet()
    {
        if (betAmount > minBetAmount)
        {
            betAmount -= 1000;
        }
        return betAmount;
    }
}
