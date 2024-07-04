using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    [SerializeField] private Image loadingImage;
    [SerializeField] private Image background;

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

    public void ShowLoading()
    {
        background.color = new Color32(0, 0, 0, 0);
        background.DOFade(0.8f, 0.5f);
        loadingImage.color = new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, 0);
        loadingImage.DOColor(new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, 1), 0.5f);
    }

    public void HideLoading()
    {
        background.DOFade(0f, 0.5f);
        loadingImage.DOColor(new Color(loadingImage.color.r, loadingImage.color.g, loadingImage.color.b, 0), 0.5f);
    }
}
