using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerRankText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerScoreText;
    public int score;
    public string playerName;
    public void SetUp(int rank, string name, int score)
    {
        playerRankText.text = rank.ToString();
        playerNameText.text = name;
        playerScoreText.text = score.ToString();
    }
}
