using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private bool levelEnded = false;

    public float winScore;
    public float scorePerBrick;
    private float currentScore;

    public int minBricksPerWave;
    public int maxBricksPerWave;
    public float brickDistance;
    public int levelMoneyReward;

    public TMP_Text LevelName1, LevelName2;

    private int currentWave = 0;

    private List<Brick> currentBricksArray = new List<Brick>();

    [SerializeField] GameObject defaultBrickPrefab;
    [SerializeField] GameObject highHealthBrickPrefab;

    [SerializeField] TMP_Text scoreDisplayText;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] TMP_Text rewardText;


    [SerializeField] GameObject spawnObject;

    private Transform[] spawnPositions;

    private void Awake()
    {
        LevelName1.text = SceneManager.GetActiveScene().name;
        LevelName2.text = SceneManager.GetActiveScene().name;
        spawnPositions = spawnObject.GetComponentsInChildren<Transform>();
        GenerateBrickWave();
    }

    private void OnGUI()
    {
        progressBar.fillAmount = currentScore / winScore;
        scoreDisplayText.text = currentScore + "/" + winScore;

    }

    private void Update()
    {
        if (!levelEnded)
        {

            if (currentScore == winScore)
            {
                WinLevel();
            }
        }
        
    }

    private void GenerateBrickWave()
    {
        currentWave++;

        for(int i = 0; i < currentBricksArray.Count; i++)
        {
            currentBricksArray[i].gameObject.transform.position = new Vector3(currentBricksArray[i].gameObject.transform.position.x, currentBricksArray[i].gameObject.transform.position.y + brickDistance, -9);
        }

        GameObject[] generatedBricks = new GameObject[UnityEngine.Random.Range(minBricksPerWave, maxBricksPerWave)];

        for (int i = 0; i < generatedBricks.Length; i++)
        {
            if (currentWave < 5)
            {
                generatedBricks[i] = defaultBrickPrefab;
            }
            else
            {
                generatedBricks[i] = highHealthBrickPrefab;
            }

            Brick generatedBrick = generatedBricks[i].GetComponent<Brick>();

            float[] xGeneratedPositions = new float[generatedBricks.Length];

            for(int j = 0; j < generatedBricks.Length; j++)
            {
                xGeneratedPositions[j] = spawnPositions[Random.Range(0, spawnPositions.Length)].position.x;
                
            }

            xGeneratedPositions = xGeneratedPositions.Distinct().ToArray();


            generatedBricks[i].transform.position = new Vector3(xGeneratedPositions[Random.Range(0, xGeneratedPositions.Length)], spawnPositions[0].position.y, -9);





            

            generatedBrick.health = currentWave;


            

            currentBricksArray.Add(GameObject.Instantiate(generatedBrick));

            

        }


    }

    public void OnPlayerLand()
    {
        GenerateBrickWave();
    }


    public void AddScore()
    {
        if(currentScore + scorePerBrick < winScore)
        {
            currentScore += scorePerBrick;
        }
        else
        {
            currentScore = winScore;
        }
    }

    public void LoseLevel()
    {
        levelEnded = true;
        loseMenu.SetActive(true);
        Time.timeScale = 0;

    }

    public void WinLevel()
    {
        levelEnded = true;
        UnlockNextLevel();
        rewardText.text = "+" + levelMoneyReward.ToString();
        winMenu.SetActive(true);
        PlayerMoney.Instance.AddMoney(levelMoneyReward);
        Time.timeScale = 0;

    }

    private void UnlockNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("MaxReachedLevel"))
        {
            PlayerPrefs.SetInt("MaxReachedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

 }
