using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSesion : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] public int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;
    
    LifeUI lifeUI;
    
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSesion>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }       
    }

    private void Start()
    {
        lifeUI = FindObjectOfType<LifeUI>();
        lifeUI.UpdateLifeUI(playerLives);        
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            Debug.Log("Take life proceed");
            TakeLife();           
        }
        else
        {
            ResetGameSession();
        }
    }
    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    private void TakeLife()
    {
        Debug.Log("Take life executed");       
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        lifeUI.UpdateLifeUI(playerLives);
    }

    public void IncreseLife()
    {
        Debug.Log("Get a life executed");
        playerLives++;               
        lifeUI.UpdateLifeUI(playerLives);
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
