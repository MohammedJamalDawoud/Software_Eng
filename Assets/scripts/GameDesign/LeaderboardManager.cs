using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public DatabaseReference reference;
    [SerializeField] private GameObject leaderboardItemPrefab;
    [SerializeField] private Transform content;
    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private void OnEnable()
    {
        SetUpLeaderboard();
    }
    public async void SetUpLeaderboard()
    {
        await reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;
                foreach (Transform child in content)
                {
                    Destroy(child.gameObject);
                }
                List<LeaderboardItem> leaderboardItems = new List<LeaderboardItem>();
                foreach (DataSnapshot child in snapshot.Children)
                {
                    LeaderboardItem leaderboardItem = Instantiate(leaderboardItemPrefab, content).GetComponent<LeaderboardItem>();
                    leaderboardItem.gameObject.SetActive(false);
                    leaderboardItems.Add(leaderboardItem);
                    leaderboardItem.playerName = child.Key.ToString();
                    leaderboardItem.score = int.Parse(child.Child("score").Value.ToString());
                    Debug.Log("Name : " + child.Key.ToString());
                    Debug.Log("Score : " + child.Child("score").Value.ToString());
                }
                // sort the leaderboard items list with score..
                leaderboardItems.Sort((x, y) => y.score.CompareTo(x.score));
                // leaderboardItems.Reverse();
                foreach (LeaderboardItem leaderboardItem1 in leaderboardItems)
                {
                    leaderboardItem1.SetUp(leaderboardItems.IndexOf(leaderboardItem1) + 1, leaderboardItem1.playerName, leaderboardItem1.score);
                    leaderboardItem1.gameObject.SetActive(true);
                    leaderboardItem1.transform.SetAsLastSibling();
                }
            }
        });
    }
}
