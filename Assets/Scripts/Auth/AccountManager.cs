using UnityEngine;
using UnityEngine.UI;  // Đảm bảo bạn có sử dụng Unity UI
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class APIManager : MonoBehaviour
{
    // Địa chỉ API backend Node.js
    private string apiBaseUrl = "http://localhost:3000";

    // Các tham chiếu UI
    public TMP_InputField emailInput;  // InputField cho email
    public Button registerButton;  // Button đăng ký
    public Button loginButton;  // Button đăng nhập

    [System.Serializable]
    public class LoginResponse
    {
        public bool success;
        public UserData data; // "data" phải trùng tên key trong JSON
    }

    [System.Serializable]
    public class UserData
    {
        public int id;
        public string gmail;
        public string username;
        public float coins;
    }


    void Start()
    {
        // Gọi Coroutine để thực thi TestAPIConnection
        StartCoroutine(TestAPIConnection());
    }

    IEnumerator TestAPIConnection()
    {
        // Tạo yêu cầu GET
        using (UnityWebRequest request = UnityWebRequest.Get(apiBaseUrl))
        {
            // Gửi yêu cầu và đợi kết quả
            yield return request.SendWebRequest();

            // Kiểm tra nếu yêu cầu thành công
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("API Connection Successful!");
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("API Connection Failed: " + request.error);
            }
        }
    }

    // Khi người dùng nhấn Button Đăng ký, gọi hàm này
    public void OnRegisterButtonClick()
    {
        string email = emailInput.text;
        RegisterUser(email);
    }

    // Khi người dùng nhấn Button Đăng nhập, gọi hàm này
    public void OnLoginButtonClick()
    {
        string email = emailInput.text;
        LoginUser(email);
    }

    // Đăng ký người dùng mới
    public void RegisterUser(string email)
    {
        StartCoroutine(RegisterUserRequest(email));
    }

    // Đăng nhập người dùng
    public void LoginUser(string email)
    {
        StartCoroutine(LoginUserRequest(email));
    }

    // Gửi request POST để đăng ký user
    IEnumerator RegisterUserRequest(string email)
    {
        string json = "{\"gmail\":\"" + email + "\"}";
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(apiBaseUrl + "/users", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User registered: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error registering user: " + request.error);
            }
        }
    }

    // Gửi request POST để đăng nhập user
    IEnumerator LoginUserRequest(string email)
    {
        // Tạo dữ liệu JSON
        string json = "{\"gmail\":\"" + email + "\"}";
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(apiBaseUrl + "/users/login", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // 1. Lấy chuỗi JSON trả về
                string responseText = request.downloadHandler.text;
                Debug.Log("Login response: " + responseText);

                // 2. Parse chuỗi JSON thành đối tượng C#
                LoginResponse loginRes = JsonUtility.FromJson<LoginResponse>(responseText);

                // 3. Kiểm tra success
                if (loginRes != null && loginRes.success)
                {
                    // Lưu username vào PlayerInfo
                    PlayerInfo.Username = loginRes.data.username;
                    Debug.Log("User logged in: " + PlayerInfo.Username);

                    // 4. Chuyển scene
                    SceneManager.LoadScene("MainGame");
                }
                else
                {
                    Debug.LogError("Login failed: " + responseText);
                }
            }
            else
            {
                Debug.LogError("Error logging in: " + request.error);
            }
        }
    }


}
