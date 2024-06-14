using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AuthenticationManager : MonoBehaviour
{
    [Header("Register Screen Refs")]
    public GameObject registerScreenObj;
    public TMP_InputField regUserNameField;
    public TMP_InputField regEmailField;
    public TMP_InputField regPasswordField;
    public TMP_InputField regConfirmPasswordField;
    [Header("Login Screen Refs")]
    public GameObject loginScreenObj;
    public TMP_InputField loginUserNameField;
    public TMP_InputField loginPasswordField;
    [Header("UI Vars")]
    public bool isRegisterScreen = true;
    public Button loginButton;
    public Button registerButton;
    public Button loginRegisterButton;
    [Header("Error Message Refs")]
    public TMP_Text errorMessageText;
    [Header("Password Buttons Refs")]
    public Button showPasswordButtonLogin;
    public Button showPasswordButtonRegister;
    public Sprite showPasswordSprite;
    public Sprite hidePasswordSprite;
    public bool isPasswordShownLogin = false;
    public bool isPasswordShownRegister = false;
    [Header("Save Login Refs")]
    public Button SaveLoginButton;
    public Sprite checkedBoxSprite;
    public Sprite unCheckedBoxSprite;
    public bool isSaveLogin = false;
    private void Awake()
    {
        loginRegisterButton.onClick.RemoveAllListeners();
        loginRegisterButton.onClick.AddListener(OnClickChangeScreenButton);
        loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(OnClickLoginButton);
        registerButton.onClick.RemoveAllListeners();
        registerButton.onClick.AddListener(OnClickRegisterButton);
        showPasswordButtonLogin.onClick.RemoveAllListeners();
        showPasswordButtonLogin.onClick.AddListener(OnClickPasswordButtonLogin);
        showPasswordButtonRegister.onClick.RemoveAllListeners();
        showPasswordButtonRegister.onClick.AddListener(OnClickPasswordButtonRegister);
        SaveLoginButton.onClick.RemoveAllListeners();
        SaveLoginButton.onClick.AddListener(OnClickCheckBox);
        OnClickPasswordButtonLogin();
        OnClickPasswordButtonRegister();

    }
    public void OnClickRegisterButton()
    {
        if (regPasswordField.text != regConfirmPasswordField.text)
        {
            ShowErrorMessage("Passwords do not match");
            return;
        }
        if (regUserNameField.text.Length < 3)
        {
            ShowErrorMessage("Username must be at least 3 characters long");
            return;
        }
        if (regPasswordField.text.Length < 6)
        {
            ShowErrorMessage("Password must be at least 6 characters long");
            return;
        }
        if (regEmailField.text.Length < 6)
        {
            ShowErrorMessage("Email must be at least 6 characters long");
            return;
        }
        Debug.Log("Registering User Successfully...");
        AuthManager.Instance.RegisterFunc(regEmailField.text, regPasswordField.text, regUserNameField.text);
    }
    public void OnClickLoginButton()
    {
        if (loginUserNameField.text.Length < 3)
        {
            ShowErrorMessage("Username must be at least 3 characters long");
            return;
        }
        if (loginPasswordField.text.Length < 6)
        {
            ShowErrorMessage("Password must be at least 6 characters long");
            return;
        }
        Debug.Log("Logging in User Successfully...");
        AuthManager.Instance.LoginFunc(loginUserNameField.text, loginPasswordField.text);
    }
    public void OnClickPasswordButtonLogin()
    {
        isPasswordShownLogin = !isPasswordShownLogin;
        loginPasswordField.contentType = isPasswordShownLogin ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        loginPasswordField.ForceLabelUpdate();
        showPasswordButtonLogin.GetComponent<Image>().sprite = !isPasswordShownLogin ? hidePasswordSprite : showPasswordSprite;
    }

    public void OnClickPasswordButtonRegister()
    {
        isPasswordShownRegister = !isPasswordShownRegister;
        regPasswordField.contentType = isPasswordShownRegister ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        regConfirmPasswordField.contentType = isPasswordShownRegister ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        regPasswordField.ForceLabelUpdate();
        regConfirmPasswordField.ForceLabelUpdate();
        showPasswordButtonRegister.GetComponent<Image>().sprite = !isPasswordShownRegister ? hidePasswordSprite : showPasswordSprite;
    }

    public void OnClickChangeScreenButton()
    {
        isRegisterScreen = !isRegisterScreen;
        registerScreenObj.SetActive(isRegisterScreen);
        loginScreenObj.SetActive(!isRegisterScreen);
        loginRegisterButton.GetComponentInChildren<TextMeshProUGUI>().text = isRegisterScreen ? "Login" : "Register";
    }

    public void OnClickCheckBox()
    {
        isSaveLogin = !isSaveLogin;
        SaveLoginButton.GetComponent<Image>().sprite = isSaveLogin ? checkedBoxSprite : unCheckedBoxSprite;
        PlayerPrefs.SetInt("savelogin", isSaveLogin ? 1 : 0);
    }
    public void ShowErrorMessage(string message)
    {
        errorMessageText.gameObject.SetActive(true);
        errorMessageText.text = message;
        StartCoroutine(HideErrorMessage());
    }

    IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(3f);
        errorMessageText.gameObject.SetActive(false);
    }

    public void ClearLoginFields()
    {
        loginUserNameField.text = "";
        loginPasswordField.text = "";
    }

    public void ClearRegisterFields()
    {
        regUserNameField.text = "";
        regEmailField.text = "";
        regPasswordField.text = "";
        regConfirmPasswordField.text = "";
    }
}
