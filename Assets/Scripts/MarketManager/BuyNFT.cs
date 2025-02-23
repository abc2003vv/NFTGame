using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class BuyNFT : MonoBehaviour
{
    public TMP_InputField buyerWalletInput;
    public TMP_Text statusText;
    public string apiURL = "http://localhost:3000/buy-nft"; // API từ backend

    public void BuyNFTButton()
    {
        string buyerPublicKey = buyerWalletInput.text; // Lấy địa chỉ ví Phantom từ người chơi
        string nftMintAddress = "NFT_MINT_ADDRESS"; // Thay bằng NFT bạn muốn bán

        if (string.IsNullOrEmpty(buyerPublicKey))
        {
            statusText.text = "Nhập địa chỉ ví Phantom của bạn!";
            return;
        }

        StartCoroutine(BuyNFTRequest(buyerPublicKey, nftMintAddress));
    }

    IEnumerator BuyNFTRequest(string buyerPublicKey, string nftMintAddress)
    {
        string json = $"{{\"buyerPublicKey\": \"{buyerPublicKey}\", \"nftMintAddress\": \"{nftMintAddress}\"}}";
        UnityWebRequest request = new UnityWebRequest(apiURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            statusText.text = "NFT đã được mua thành công!";
            Debug.Log("Mua NFT thành công: " + request.downloadHandler.text);
        }
        else
        {
            statusText.text = "Lỗi khi mua NFT!";
            Debug.LogError("Lỗi: " + request.error);
        }
    }
}
