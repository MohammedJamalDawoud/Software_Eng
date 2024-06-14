using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorBoxManager : MonoBehaviour
{
    public GameObject errorBox;
    [SerializeField] private int messageLengthForChange = 30;
    [SerializeField] private float smallErrorBoxWidth = 600f;
    [SerializeField] private float bigErrorBoxWidth = 1200f;
    public TMP_Text errorText;
    public Color32 successColor;
    public Color32 errorColor;
    public bool isDisplayingError = false;
    private Tween scaleTween;
    public void ShowErrorMessage(string message, bool isSuccess)
    {
        if (isDisplayingError)
        {
            StopAllCoroutines();
        }
        isDisplayingError = true;
        errorBox.transform.localScale = Vector3.zero;
        errorBox.SetActive(true);
        scaleTween = errorBox.transform.DOScale(Vector3.one, 0.5f);
        RectTransform rectTransform = errorBox.GetComponent<RectTransform>();
        errorText.text = message;
        if (message.Length < messageLengthForChange)
        {
            rectTransform.DOSizeDelta(new Vector2(smallErrorBoxWidth, rectTransform.sizeDelta.y), 0.5f);
        }
        else
        {
            rectTransform.DOSizeDelta(new Vector2(bigErrorBoxWidth, rectTransform.sizeDelta.y), 0.5f);
        }
        errorBox.transform.GetChild(0).GetComponent<Image>().DOColor(isSuccess ? successColor : errorColor, 0.5f);
        StartCoroutine(disableErrorMessageRoutine(rectTransform));
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         ShowErrorMessage("This is an error message", false);
    //     }
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         ShowErrorMessage("This is an error message which is big in size ", true);
    //     }

    // }

    private IEnumerator disableErrorMessageRoutine(RectTransform rectTransform)
    {
        // till 3 seconds i want to change the scale like from 1,1,1, to 0.9f,0.9f,0.9f and again back 1,1,1 in a loop using DG tweening.
        yield return new WaitUntil(() => scaleTween.IsPlaying() == false);
        rectTransform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3f);
        isDisplayingError = false;
        errorBox.SetActive(false);
        rectTransform.DOKill();
        isDisplayingError = false;
    }

}
