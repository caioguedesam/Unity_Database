using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // User input
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;

    private void Start()
    {
        loginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.Login(usernameInput.text, passwordInput.text));
        });
    }
}
