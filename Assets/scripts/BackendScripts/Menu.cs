using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a menu component that can be managed by MenuManager.
/// Each menu should have a unique name for identification.
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Unique identifier for this menu, used by MenuManager to open/close menus.
    /// </summary>
    public string menuName;
}