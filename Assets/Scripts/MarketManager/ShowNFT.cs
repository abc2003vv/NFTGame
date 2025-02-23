using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ShowNFT : MonoBehaviour
{
    public string pinataMetadataURL = "https://gateway.pinata.cloud/ipfs/bafkreigkqcud3whkxywktxpkvuzomdhhruxyt45yzzbmdccunipuybijyu";
    public TMP_Text nftNameText;
    public TMP_Text nftPriceText;
    public Renderer nftImageRenderer;

    void Start()
    {
        StartCoroutine(LoadNFTMetadata());
    }

    IEnumerator LoadNFTMetadata()
    {
        UnityWebRequest request = UnityWebRequest.Get(pinataMetadataURL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Lỗi tải metadata: " + request.error);
        }
        else
        {
            JObject metadata = JObject.Parse(request.downloadHandler.text);
            nftNameText.text = metadata["name"].ToString();
            nftPriceText.text = metadata["attributes"][2]["value"].ToString(); // Lấy giá NFT

            string imageUrl = metadata["image"].ToString();
            StartCoroutine(LoadNFTImage(imageUrl));
        }
    }

    IEnumerator LoadNFTImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Lỗi tải ảnh NFT: " + request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            nftImageRenderer.material.mainTexture = texture;
        }
    }
}
