using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHoverScript : MonoBehaviour
{
    [SerializeField] private bool mouseHover = false; // determines if the mouse is hovering over the button
    [SerializeField] private Vector3 beforeScale = Vector3.one; // the scale of the button before hovering
    [SerializeField] private Vector3 afterScale = new Vector3(1.2f, 1.2f, 1.2f); // the scale of the button after hovering
    [SerializeField] private Vector3 clickedScale = new Vector3(1.1f, 1.1f, 1.1f); // the scale of the button after clicking
    [SerializeField] private float Snapiness = 10f; // the speed of the button scaling (smoothing effect)
    public bool isSelected = false; // determines if the button is selected or not
    public bool isMyTurn = true;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Color enabledColor;
    private void Update()
    {
        if (GetComponent<Button>() && GetComponent<Button>().interactable == false) // if the button is not interactable then return
        {
            return;
        }
        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, isMyTurn ? enabledColor : disabledColor, Time.deltaTime * Snapiness);
        if (mouseHover || isSelected) // if the mouse is hovering over the button or the button is selected then scale the button
        {
            if (Input.GetMouseButton(0) && mouseHover) // if the mouse is clicked and the mouse is hovering over the button then scale the button to clickedScale
            {
                transform.localScale = Vector3.Lerp(transform.localScale, clickedScale, Time.deltaTime * Snapiness);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, afterScale, Time.deltaTime * Snapiness);
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, beforeScale, Time.deltaTime * Snapiness); // if the mouse is not hovering over the button then scale the button to beforeScale
        }
    }
    public void OnPointerEnter(PointerEventData eventData) // this function is called when the mouse is hovering over the button
    {
        mouseHover = true;
    }

    public void OnPointerExit(PointerEventData eventData) // this function is called when the mouse is not hovering over the button
    {
        mouseHover = false;
    }

    public void OnEnable()
    {
        mouseHover = false;
    }
}
