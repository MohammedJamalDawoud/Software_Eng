using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private List<GameOverItem> gameOverItems = new List<GameOverItem>();

    public void SetUp(int playerA, int playerB, int playerC, int playerD)
    {
        List<int> scores = new List<int> { playerA, playerB, playerC, playerD };
        scores.Sort();
        scores.Reverse();
        int i = 0;
        foreach (GameOverItem gameOverItem in gameOverItems) 
        {
            gameOverItem.SetUp(scores[i] == playerA ? GetPlayerName() : $"Bot - {i}", i + 1, scores[i], scores[i] == playerA);
            i++;
            Debug.Log("Player A: " + playerA + " Player B: " + playerB + " Player C: " + playerC + " Player D: " + playerD);
        }
    }


    private string GetPlayerName()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            return PlayerPrefs.GetString("username");
        }
        return "Player - " + Random.Range(1000, 9999);
    }
}
