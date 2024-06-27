using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool mouseHover = false;
    public ColorType color;
    [SerializeField] private Vector3 beforeScale = Vector3.one;
    [SerializeField] private Vector3 afterScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private Vector3 clickedScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float Snapiness = 10f;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private Color32 beforeColor;
    [SerializeField] private Color32 afterColor;
    [SerializeField] private CardsManager myCardsManager;
    public bool isSelected = false;
    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickColorButton);
    }
    private void Update()
    {
        if (GetComponent<Button>() && GetComponent<Button>().interactable == false) // if the button is not interactable then return
        {
            return;
        }
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
            image.color = Color32.Lerp(image.color, afterColor, Time.deltaTime * Snapiness); // change the color of the button to white
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, beforeScale, Time.deltaTime * Snapiness); // if the mouse is not hovering over the button then scale the button to beforeScale
            image.color = Color32.Lerp(image.color, beforeColor, Time.deltaTime * Snapiness); // change the color of the button to transparent
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

    private void OnClickColorButton()
    {
        if (GamePlayManager.Instance.isMyTurn(myCardsManager.id))
        {
            if (StackManager.Instance.CheckTopOfStackisWildorStack())
            {
                GamePlayManager.Instance.SetThisColorSelector(this);
            }
        }

    }
}

public enum ColorType
{
    Red,
    Blue,
    Green,
    Yellow
}
