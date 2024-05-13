using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour // Menu Manager Class this manages all the menus in the game
{
    public static MenuManager Instance; // This is just an Static Instance singleton of the MenuManager Class
    public List<Menu> menus; // This is just the list Of menus that are required to open or close.
    [SerializeField] private Transform menusParent; // This is where the all the menus are storeed...
    public void Awake() // this is the first method called as soon as the game loads
    {
        Instance = this; // storing the instance of Singleton
        menus = FindObjectsOfType<Menu>(true).ToList(); // getting all the menus that are avaiable in the scene
    }

    public GameObject OpenMenu(string menuName) // this function is called when to open a new Menu
    {
        GameObject selectedMenu = null; // currentSelected Menu is Stored here
        foreach (Menu currentMenu in menus) // we will use for Loop to iterate all the menus..
        {
            if (currentMenu.menuName == menuName) // checking if the target menuName and currentMenu is same
            {
                currentMenu.gameObject.SetActive(true); // setting currentMenu is SetActive to true (visible)
                selectedMenu = currentMenu.gameObject; // storing the currentMenu in selectedMenu
            }
            else
            {
                currentMenu.gameObject.SetActive(false); // setting currentMenu is SetActive to false (invisible)
            }
        }

        return selectedMenu; // returning the selectedMenu
    }

    public void Onclickplaymenu()
    {
        OpenMenu("play");
        Debug.Log("Menü");
    }
}