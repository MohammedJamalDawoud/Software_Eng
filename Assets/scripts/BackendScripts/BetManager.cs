using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager Instance;
    public int betAmount = 1000;
    public int maxBetAmount = 10000;
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

    public int OnIncreBet()
    {
        if (betAmount < maxBetAmount)
        {
            betAmount += 1000;
        }
        return betAmount;
    }

    public int OnDecreBet()
    {
        if (betAmount > minBetAmount)
        {
            betAmount -= 1000;
        }
        return betAmount;
    }
}
