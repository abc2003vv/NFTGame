using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Metadata : MonoBehaviour
{
    public string metadataURL = "https://gateway.pinata.cloud/ipfs/bafkreigkqcud3whkxywktxpkvuzomdhhruxyt45yzzbmdccunipuybijyu";

    void Start()
    {
        StartCoroutine(LoadNFTMetadata());
    }

    IEnumerator LoadNFTMetadata()
    {
        UnityWebRequest request = UnityWebRequest.Get(metadataURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            JObject metadata = JObject.Parse(json);

            string name = metadata["name"].ToString();
            string imageUrl = metadata["image"].ToString();

            Debug.Log("NFT Name: " + name);
            Debug.Log("Image URL: " + imageUrl);

            StartCoroutine(LoadNFTImage(imageUrl));
        }
        else
        {
            Debug.LogError("Failed to load metadata");
        }
    }

    IEnumerator LoadNFTImage(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            GetComponent<Renderer>().material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Failed to load NFT image");
        }
    }
}

