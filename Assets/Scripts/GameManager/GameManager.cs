using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;

    void Start()
    {
        // Hiển thị dữ liệu từ PlayerInfo
        usernameText.text =  PlayerInfo.Username + "";
        coinsText.text =  PlayerInfo.Coins + "";
        diamondsText.text =  PlayerInfo.Diamonds + "";
    }
}
