using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField emailForm;
    public InputField passwordForm;

    public Button loginButtonForm;

    private DBManager DB;
    private AuthManager auth;
    
    void Start()
    {
        DB = DBManager.Instance;
        auth = AuthManager.Instance;

        loginButtonForm.onClick.AddListener(DoLogin);
    }

    void DoLogin()
    {
        string email = emailForm.text;
        string password = passwordForm.text;

        auth.Login(email, password);
    }
}
