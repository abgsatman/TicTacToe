using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthManager : Singleton<AuthManager>
{
    public FirebaseAuth auth;

    public DBManager DB;
    
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        DB = DBManager.Instance;
    }

    public void Signup(string username, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                return;
            }
            if(task.IsFaulted)
            {
                return;
            }
            FirebaseUser newUser = task.Result;
            DB.user.UserID = newUser.UserId;
            DB.CreateUser(username);
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                Debug.Log("canceled");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("faulted");
                return;
            }
            FirebaseUser newUser = task.Result;
            DB.user.UserID = newUser.UserId;
            DB.GetUserInformation();
        });
    }

    public void AutoLogin()
    {
        Debug.Log("Auto Login...");
    }
}
