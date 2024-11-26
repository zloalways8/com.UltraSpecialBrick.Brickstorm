using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour
{
    public int health;

    private LevelController levelController;

    private SpriteRenderer spriteRenderer;
    [SerializeField] TMP_Text healthText;

    private void Awake()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(health <= 1)
            {
                try
                {
                    levelController.AddScore();
                }
                catch
                {
                    Debug.Log("Endless");
                }
                gameObject.SetActive(false);
            }
            else
            {
                health--;
            }

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("DeathZone"))
        {
            try
            {
                levelController.LoseLevel();
            }
            catch
            {
                EndlessLevelController endless = GameObject.Find("LevelController").GetComponent<EndlessLevelController>();
                endless.LoseLevel();
            }
        }
    }



    private void OnGUI()
    {
        healthText.text = "" + health;
    }
}
