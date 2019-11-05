using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    public string userID { get; private set; }
    public string userName;
    public string userPassword;


    public void SetCredentials(string username, string password)
    {
        userName = username;
        userPassword = password;
    }

    public void SetID(string id)
    {
        userID = id;
    }

    public void UpdateUserInformation()
    {
        Main.Instance.UserProfile.transform.Find("Username").GetComponent<Text>().text = userName;
    }
}
