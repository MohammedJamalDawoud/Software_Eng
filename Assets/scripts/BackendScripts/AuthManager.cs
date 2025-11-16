using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

/// <summary>
/// Manages user authentication using Firebase Authentication.
/// Handles user registration, login, logout, and persistent session management.
/// Integrates with Firebase Realtime Database for user profile data.
/// </summary>
public class AuthManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the AuthManager.
    /// Provides global access to authentication functionality.
    /// </summary>
    public static AuthManager Instance;
    
    /// <summary>
    /// Firebase Authentication instance for user management.
    /// </summary>
    public FirebaseAuth auth;
    
    /// <summary>
    /// Firebase Realtime Database reference for user data storage.
    /// </summary>
    public DatabaseReference reference;
    
    /// <summary>
    /// UI manager that handles authentication interface interactions.
    /// </summary>
    public UI_AuthenticationManager uIFieldManager;
    /// <summary>
    /// Initializes Firebase services and sets up the singleton instance.
    /// </summary>
    public void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Checks for saved login credentials and attempts automatic login if available.
    /// </summary>
    private void Start()
    {
        if (PlayerPrefs.HasKey("savelogin"))
        {
            int saveLogin = PlayerPrefs.GetInt("savelogin");
            if (saveLogin == 1)
            {
                string email = PlayerPrefs.GetString("email");
                string password = PlayerPrefs.GetString("password");
                uIFieldManager.loginUserNameField.text = email;
                uIFieldManager.loginPasswordField.text = password;
                uIFieldManager.loginButton.onClick.Invoke();
                uIFieldManager.isSaveLogin = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("savelogin", 0);
        }
    }

    /// <summary>
    /// Registers a new user with Firebase Authentication and creates their profile in the database.
    /// </summary>
    /// <param name="email">User's email address.</param>
    /// <param name="password">User's password.</param>
    /// <param name="userName">Desired username for display purposes.</param>
    public async void RegisterFunc(string email, string password, string userName)
    {
        LoadingManager.Instance.ShowLoading();
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log(task.Exception);
                uIFieldManager.ShowErrorMessage(task.Exception.ToString());
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
                uIFieldManager.ShowErrorMessage(task.Exception.ToString());
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                reference.Child(userName);
                reference.Child(userName).Child("Email").SetValueAsync(email);
                reference.Child(userName).Child("uid").SetValueAsync(newUser.UserId);
                reference.Child(userName).Child("score").SetValueAsync(0);
                // uIFieldManager.ChangeScreen();
                PlayerPrefs.SetString("username", userName);
                Debug.Log("User Registered");
                uIFieldManager.ClearRegisterFields();
                uIFieldManager.OnClickChangeScreenButton();
                LoadingManager.Instance.HideLoading();
            }
        });
    }

    /// <summary>
    /// Authenticates an existing user with Firebase using email and password.
    /// Optionally saves credentials for automatic login on next session.
    /// </summary>
    /// <param name="email">User's email address.</param>
    /// <param name="password">User's password.</param>
    public async void LoginFunc(string email, string password)
    {
        LoadingManager.Instance.ShowLoading();
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log(task.Exception);
                uIFieldManager.ShowErrorMessage(task.Exception.ToString());
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
                uIFieldManager.ShowErrorMessage(task.Exception.ToString());
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                Debug.Log("User Logged In");
                if (uIFieldManager.isSaveLogin)
                {
                    PlayerPrefs.SetString("email", email);
                    PlayerPrefs.SetString("password", password);
                }
                uIFieldManager.ClearLoginFields();
                MenuManager.Instance.OpenMenu("main");
                LoadingManager.Instance.HideLoading();
            }
        });
    }

    /// <summary>
    /// Signs out the current user and clears all stored preferences.
    /// Returns to the authentication menu.
    /// </summary>
    public void OnClickSignOut()
    {
        auth.SignOut();
        MenuManager.Instance.OpenMenu("auth");
        PlayerPrefs.DeleteAll();
    }
}
