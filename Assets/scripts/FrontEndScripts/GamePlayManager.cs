using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public List<Sprite> cardSprites;
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
    public Sprite GetRandomSprite()
    {
        return cardSprites[Random.Range(0, cardSprites.Count)];
    }
}
