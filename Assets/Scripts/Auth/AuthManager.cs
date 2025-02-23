using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    private string registerUrl = "http://localhost:3000/register"; // URL API đăng ký

    public void Register(string email, string walletAddress)
    {
        StartCoroutine(RegisterUser(email, walletAddress));
    }

    IEnumerator RegisterUser(string email, string walletAddress)
    {
        string json = "{\"email\":\"" + email + "\", \"walletAddress\":\"" + walletAddress + "\"}";
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(registerUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Registration success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Registration failed: " + request.error);
            }
        }
    }
}
