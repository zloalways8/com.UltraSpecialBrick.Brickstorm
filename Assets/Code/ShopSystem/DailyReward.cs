using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public int moneyReward;

    private Button rewardButton;
    private double lastRewardOpen;
    public float hoursToWait;
    private float msToWait;
    [SerializeField] TMP_Text rewardText;

    private void Awake()
    {

        rewardButton = GetComponent<Button>();

        

        

    }

    private void Start()
    {

        double.TryParse(PlayerPrefs.GetString("LastRewardClaimTime"), out lastRewardOpen);

        msToWait = hoursToWait * 3600000;

        if (!IsRewardReady())
        {
            rewardButton.interactable = false;

        }

        
        

    }

    private void Update()
    {
        if (!rewardButton.IsInteractable())
        {
            if (IsRewardReady())
            {
                rewardButton.interactable = true;

                return;
            }
            TimerScript();
        }
    }

    public void TimerScript()
    {
        double diff = ((double)DateTime.Now.Ticks - lastRewardOpen);
        double ms = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (msToWait - (float)ms) / 1000;

        string timeText = "";
        //hours
        timeText += ((int)secondsLeft / 3600).ToString() + ":";
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        //minutes
        timeText += ((int)secondsLeft / 60).ToString("00") + ":";
        //seconds
        timeText += (secondsLeft % 60).ToString("00");

        rewardText.text = timeText;
    }
    public void ClaimReward()
    {
        lastRewardOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastRewardClaimTime", lastRewardOpen.ToString());
        PlayerMoney.Instance.AddMoney(moneyReward);
        rewardButton.interactable = false;
    }

    private bool IsRewardReady()
    {
        double diff = ((double)DateTime.Now.Ticks - lastRewardOpen);
        double ms = diff / TimeSpan.TicksPerMillisecond;


        float secondsLeft = (msToWait - (float)ms) / 1000;

        if (secondsLeft < 0)
        {
            rewardText.text = moneyReward.ToString();
            return true;
        }

        return false;
    }
}
