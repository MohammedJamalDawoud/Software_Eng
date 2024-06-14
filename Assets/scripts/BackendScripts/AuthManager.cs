using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;
    public FirebaseAuth auth;
    public DatabaseReference reference;
    public UI_AuthenticationManager uIFieldManager;
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

    public async void RegisterFunc(string email, string password, string userName)
    {
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
                // uIFieldManager.ChangeScreen();
                Debug.Log("User Registered");
                uIFieldManager.ClearRegisterFields();
                uIFieldManager.OnClickChangeScreenButton();
            }
        });
    }

    public async void LoginFunc(string email, string password)
    {
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
            }
        });
    }

    public void OnClickSignOut()
    {
        auth.SignOut();
        MenuManager.Instance.OpenMenu("auth");
        // clear all the PlayerPrefs...
        PlayerPrefs.DeleteAll();
    }
}
