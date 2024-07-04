using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text playerRank;
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private Image backgroundImg;

    public void SetUp(string name, int rank, int score, bool isMine)
    {
        playerName.text = name;
        playerRank.text = $"Rank {rank}";
        playerScore.text = $"{score} Pts";
        backgroundImg.DOColor(isMine ? Color.red : Color.white, 0.5f);
    }
}
