using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;


    [SerializeField] TMP_Text moneyDisplayText;
    private int playerMoney;
    private const string prefMoney = "PlayerMoney";

    private void Awake()
    {
        Instance = this;

        playerMoney = PlayerPrefs.GetInt(prefMoney);
    }

    public bool TryToRemoveMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            PlayerPrefs.SetInt(prefMoney, playerMoney);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        PlayerPrefs.SetInt(prefMoney, playerMoney);
    }

    private void OnGUI()
    {
        moneyDisplayText.text = playerMoney.ToString();
    }
}
