using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetUI : MonoBehaviour
{
    public ButtonHover rankedButton;
    public ButtonHover casualButton;
    public bool isRanked = true;

    public TMP_Text betAmountText;
    public Button incrButton;
    public Button decrButton;
    public void Awake()
    {
        rankedButton.GetComponent<Button>().onClick.RemoveAllListeners();
        casualButton.GetComponent<Button>().onClick.RemoveAllListeners();
        rankedButton.GetComponent<Button>().onClick.AddListener(() => OnClickRankedCasualButton(true));
        casualButton.GetComponent<Button>().onClick.AddListener(() => OnClickRankedCasualButton(false));
        rankedButton.GetComponent<Button>().onClick.Invoke();

        incrButton.onClick.RemoveAllListeners();
        decrButton.onClick.RemoveAllListeners();
        incrButton.onClick.AddListener(OnClickIncreaseBet);
        decrButton.onClick.AddListener(OnClickDecreaseBet);

    }

    public void OnClickRankedCasualButton(bool _isRanked)
    {
        isRanked = _isRanked;
        rankedButton.isSelected = isRanked;
        casualButton.isSelected = !isRanked;
        ManageBetUI();
    }

    public void ManageBetUI()
    {
        if (isRanked)
        {
            betAmountText.text = "100";
            incrButton.interactable = true;
            decrButton.interactable = true;
        }
        else
        {
            betAmountText.text = "NONE";
            incrButton.interactable = false;
            decrButton.interactable = false;
        }
    }

    public void OnClickIncreaseBet()
    {
        // call here the method to change the bet...
        betAmountText.text = 100.ToString();
    }

    public void OnClickDecreaseBet()
    {
        // call here the method to change the bet...
        betAmountText.text = 0.ToString();
    }
}
