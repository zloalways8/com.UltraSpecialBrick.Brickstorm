using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "New Skin", menuName = "Create New Skin")]
public class Skin : ScriptableObject
{
    public enum SkinIDs
    {
        defaultSkin, gray, green, pink, purple, red, red2, red3
    }

    public SkinIDs skinID;

    public Sprite skinSprite;

    public int skinPrice;
}
