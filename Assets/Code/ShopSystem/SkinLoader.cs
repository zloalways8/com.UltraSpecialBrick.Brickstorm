using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    private SpriteRenderer playerSR;

    private void Awake()
    {
        playerSR = GetComponent<SpriteRenderer>();
        playerSR.sprite = SkinManager.equippedSkin;
    }
}
