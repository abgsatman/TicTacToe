using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signup : MonoBehaviour
{
    public InputField usernameForm;
    public InputField emailForm;
    public InputField passwordForm;

    public Button signupButtonForm;

    private DBManager DB;
    private AuthManager auth;

    void Start()
    {
        DB = DBManager.Instance;
        auth = AuthManager.Instance;

        signupButtonForm.onClick.AddListener(DoSignup);
    }

    void DoSignup()
    {
        Debug.Log("signup süreci başladı..");

        string username = usernameForm.text;
        string email = emailForm.text;
        string password = passwordForm.text;

        auth.Signup(username, email, password);
    }
}
