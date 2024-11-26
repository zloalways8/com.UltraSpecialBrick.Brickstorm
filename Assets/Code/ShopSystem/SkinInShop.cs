using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinInShop : MonoBehaviour
{
    private bool isSkinUnlocked;
    [SerializeField] private bool isFreeSkin;

    [SerializeField] Skin skin;
    public Skin _skin { get { return skin; } }

    [SerializeField] GameObject priceBG;
    [SerializeField] TMP_Text priceText;



    [SerializeField] Image skinImage;

    private void Awake()
    {

        skinImage.sprite = skin.skinSprite;

        if (isFreeSkin)
        {
            priceBG.gameObject.SetActive(false);
            PlayerPrefs.SetInt(skin.skinID.ToString(), 1);
        }

        UnlockedSkinCheck();
        
        
    }

    private void UnlockedSkinCheck()
    {

        if(PlayerPrefs.GetInt(skin.skinID.ToString()) == 1) 
        {
            isSkinUnlocked = true;
            priceBG.gameObject.SetActive(false);
        }
        else
        {
            priceBG.gameObject.SetActive(true);
            priceText.text = skin.skinPrice.ToString();
        }
    }

    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            SkinManager.Instance.EquipSkin(this);
        }
        else
        {
            if(PlayerMoney.Instance.TryToRemoveMoney(skin.skinPrice))
            {
                PlayerPrefs.SetInt(skin.skinID.ToString(), 1);
                UnlockedSkinCheck();
            }
        }
    }
}
