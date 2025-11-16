using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages menu navigation and state transitions throughout the application.
/// Implements a menu system where only one menu is active at a time.
/// Uses singleton pattern for global access to menu functionality.
/// </summary>
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the MenuManager.
    /// Provides global access to menu navigation functionality.
    /// </summary>
    public static MenuManager Instance;
    
    /// <summary>
    /// List of all available menus in the scene.
    /// </summary>
    public List<Menu> menus;
    
    [SerializeField] private Transform menusParent;
    
    /// <summary>
    /// Initializes the singleton instance and discovers all Menu components in the scene.
    /// </summary>
    public void Awake()
    {
        Instance = this;
        menus = FindObjectsOfType<Menu>(true).ToList();
    }

    /// <summary>
    /// Opens a menu by name and closes all other menus.
    /// Only one menu is active at a time.
    /// </summary>
    /// <param name="menuName">The name of the menu to open (must match Menu.menuName).</param>
    /// <returns>The GameObject of the opened menu, or null if no matching menu was found.</returns>
    public GameObject OpenMenu(string menuName)
    {
        GameObject selectedMenu = null;
        foreach (Menu currentMenu in menus)
        {
            if (currentMenu.menuName == menuName)
            {
                currentMenu.gameObject.SetActive(true);
                selectedMenu = currentMenu.gameObject;
            }
            else
            {
                currentMenu.gameObject.SetActive(false);
            }
        }

        return selectedMenu;
    }
}