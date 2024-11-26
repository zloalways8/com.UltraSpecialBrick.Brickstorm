using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{

    public int levelId;

    private Button levelButton;
    [SerializeField] Image lockIcon;
    [SerializeField] TMP_Text levelNumberText;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        levelButton = GetComponent<Button>();
        levelNumberText.text = levelId.ToString();

        if (levelId <= unlockedLevel)
        {
            levelButton.interactable = true;
            lockIcon.gameObject.SetActive(false);
            levelNumberText.gameObject.SetActive(true);
            
        }
        else
        {
            levelButton.interactable = false;
            lockIcon.gameObject.SetActive(true);
            levelNumberText.gameObject.SetActive(false);
        }
        
        
    }

    public void OpenLevel()
    {
        Time.timeScale = 1;
        string levelName = "Level " + levelId.ToString();
        SceneManager.LoadScene(levelName);
    }

    public void OpenLevelEndless()
    {
        Time.timeScale = 1;
        string levelName = "EndlessLevel";
        SceneManager.LoadScene(levelName);
    }




}
