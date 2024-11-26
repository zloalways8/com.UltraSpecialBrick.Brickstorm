using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] Sprite equippedSprite;
    [SerializeField] Sprite unequippedSprite;

    public static Sprite equippedSkin { get; private set; }

    [SerializeField] Skin[] allSkins;

    [SerializeField] Transform skinsInShopPanelsParent;
    private List<SkinInShop> skinsInShopPanels = new List<SkinInShop>();

    private Button currentlyEquippedSkinButton;

    public static SkinManager Instance;

    private const string skinPref = "EquippedSkin";

    private void Awake()
    {
        Instance = this;

        foreach(Transform s in skinsInShopPanelsParent)
        {
            if(s.TryGetComponent(out SkinInShop skinInShop))
            {
                skinsInShopPanels.Add(skinInShop);
            }
        }

        EquipPreviousSkin();


        SkinInShop skinEquippedPanel = Array.Find(skinsInShopPanels.ToArray(), dummyFind => dummyFind._skin.skinSprite == equippedSkin);
        currentlyEquippedSkinButton = skinEquippedPanel.GetComponent<Button>();
        currentlyEquippedSkinButton.interactable = false;
        currentlyEquippedSkinButton.GetComponent<Image>().sprite = equippedSprite;
        

        
    }

    private void EquipPreviousSkin()
    {
        string lastSkinUsed = PlayerPrefs.GetString(skinPref, Skin.SkinIDs.defaultSkin.ToString());
        SkinInShop lastSkin = Array.Find(skinsInShopPanels.ToArray(), dummyFind => dummyFind._skin.skinID.ToString() == lastSkinUsed);
        EquipSkin(lastSkin);
    }

    public void EquipSkin(SkinInShop skinInShop)
    {
        equippedSkin = skinInShop._skin.skinSprite;
        PlayerPrefs.SetString(skinPref, skinInShop._skin.skinID.ToString());

        if(currentlyEquippedSkinButton != null)
        {
            currentlyEquippedSkinButton.interactable = true;
            currentlyEquippedSkinButton.GetComponent<Image>().sprite = unequippedSprite;
        }
        

        currentlyEquippedSkinButton = skinInShop.GetComponent<Button>();
        currentlyEquippedSkinButton.interactable = false;
        currentlyEquippedSkinButton.GetComponent<Image>().sprite = equippedSprite;
    }
}
