using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

public class Web : MonoBehaviour
{

    private void Start()
    {
        
    }

    // GetUsers: faz uma conexão com o servidor e guarda/imprime os dados de todos os usuários
    IEnumerator GetUsers()
    {
        // Conectando ao servidor
        using(UnityWebRequest www = UnityWebRequest.Get("http://localhost/archtutorial2/GetUsers.php"))
        {
            // Esperando conexão
            yield return www.SendWebRequest();

            // Se a conexão retorna erro, mostrar erro
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            // Caso contrário, guardar resultados
            else
            {
                // Imprimir dados recebidos do servidor
                Debug.Log(www.downloadHandler.text);
                // Guardar dados
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    // Login: função de login padrão. Faz uma conexão com o servidor, manda nome de usuário e senha
    // via POST e realiza a função de Login com a base de dados.
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        // Mandando informações para .php
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        // Conectando com o servidor
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/archtutorial2/Login.php", form))
        {
            // Esperando conexão
            yield return www.SendWebRequest();

            // Tratando erros
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            // Conexão bem sucedida
            else
            {
                // Guarda os dados do usuário atual em UserInfo
                Debug.Log(www.downloadHandler.text);
                Main.Instance.UserInfo.SetCredentials(username, password);
                Main.Instance.UserInfo.SetID(www.downloadHandler.text);

                // Se houveram erros:
                if(www.downloadHandler.text.Contains("Wrong credentials") || www.downloadHandler.text.Contains("User not found"))
                {
                    Debug.Log("Error. TryAgain.");
                }
                // Se Login bem sucedido, ativa o dado usuário
                else
                {
                    Main.Instance.UserProfile.SetActive(true);
                    Main.Instance.Login.gameObject.SetActive(false);
                    Main.Instance.UserInfo.UpdateUserInformation();
                }
                
            }
        }
    }

    // RegisterUser: Faz uma conexão com o servidor para registrar um novo usuário no banco.
    IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        // Submitting login info to PHP file
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        // Connecting to server
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/archtutorial2/RegisterUser.php", form))
        {
            // Waiting for connection
            yield return www.SendWebRequest();

            // Error handling
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            // Connection successful
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // GetItemIDs: Faz uma conexão ------------
    public IEnumerator GetItemIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        // Submitting login info to PHP file
        form.AddField("userID", userID);

        // Conectando ao servidor
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/archtutorial2/GetItemIDs.php", form))
        {
            // Esperando conexão
            yield return www.SendWebRequest();

            // Se a conexão retorna erro, mostrar erro
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            // Caso contrário, guardar resultados
            else
            {
                string jsonArray = www.downloadHandler.text;
                Debug.Log(jsonArray);
                // Chamar callback para guardar resultados (json)
                callback(jsonArray);
            }
        }
    }

    // GetItem: Faz uma conexão ------------
    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        // Submitting login info to PHP file
        form.AddField("itemID", itemID);

        // Conectando ao servidor
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/archtutorial2/GetItem.php", form))
        {
            // Esperando conexão
            yield return www.SendWebRequest();

            // Se a conexão retorna erro, mostrar erro
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            // Caso contrário, guardar resultados
            else
            {
                string jsonArray = www.downloadHandler.text;
                Debug.Log(jsonArray);
                // Chamar callback para guardar resultados (json)
                callback(jsonArray);
            }
        }
    }

    public IEnumerator DownloadImage(string imageURL, System.Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("imageURL", imageURL);

        // Conectando ao servidor
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/archtutorial2/DownloadImage.php", form))
        {
            // Esperando conexão
            yield return www.SendWebRequest();

            // Se a conexão retorna erro, mostrar erro
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            // Caso contrário, guardar resultados
            else
            {
                // Guardar dados da imagem em results
                byte[] results = www.downloadHandler.data;
                callback(results);
            }
        }
    }
}
