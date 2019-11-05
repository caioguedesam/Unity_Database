using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    Action<string> createItemsCallback;

    void Start()
    {
        createItemsCallback = (jsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userID = Main.Instance.UserInfo.userID;
        StartCoroutine(Main.Instance.Web.GetItemIDs(userID, createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        // Fazendo parsing do vetor em JSON
        SimpleJSON.JSONArray jsonArray = SimpleJSON.JSON.Parse(jsonArrayString) as SimpleJSON.JSONArray;

        // Para todos os items do usuário:
        for(int i = 0; i < jsonArray.Count; i++)
        {
            // Guardando informações dos items
            bool isDone = false;    // O download do item já terminou?
            bool isImageDone = false;
            string itemID = jsonArray[i].AsObject["itemID"];    // ID do item
            byte[] imageData = new byte[0];   // image data
            SimpleJSON.JSONObject itemInfoJSON = new SimpleJSON.JSONObject();

            // Criando callback para conseguir a informação da classe Web
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                // Guarda a informação do dado item em um vetor JSON
                SimpleJSON.JSONArray tempArray = SimpleJSON.JSON.Parse(itemInfo) as SimpleJSON.JSONArray;
                itemInfoJSON = tempArray[0].AsObject;
            };

            Action<byte[]> downloadImageCallback = (imageBytes) =>
            {
                isImageDone = true;
                // Guardando informação da imagem
                imageData = imageBytes;
            };

            StartCoroutine(Main.Instance.Web.GetItem(itemID, getItemInfoCallback));

            // Esperar até o callback ser chamado da web (informação terminou de ser baixada)
            yield return new WaitUntil(() => isDone == true);

            // Criando o item baixado (prefab)
            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            // Conseguindo conteúdo da imagem
            StartCoroutine(Main.Instance.Web.DownloadImage(itemInfoJSON["image"], downloadImageCallback));
            yield return new WaitUntil(() => isImageDone == true);

            // Criando textura e sprite com o conteúdo da imagem
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;
            print(imageData);
            texture.LoadImage(imageData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 8f);
            

            // Preenchendo informação
            item.transform.Find("Name").GetComponent<Text>().text = itemInfoJSON["name"];
            item.transform.Find("Description").GetComponent<Text>().text = itemInfoJSON["description"];
            item.transform.Find("Image").GetComponent<Image>().sprite = sprite;
        }
    }
}
