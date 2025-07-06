using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Manages game functions. Access via the singleton (NOTE: THIS SHOULD BE THE ONLY SINGLETON). Would like to learn a way around this time of project structure though
/// </summary>
public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    
    [SerializeField] TextMeshProUGUI scoreText;

    public EventHandler OnPause;
    public EventHandler OnResume;

    float score = 0;
    
    bool isPaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        scoreText.text = score.ToString();
    }

    void GameBegin()
    {
        // Add a three two one need to change pause function to make this work more logically
    }

    public void AddScore(float value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public IEnumerator GameEnd(bool isWin)
    {
        TogglePause(false);
        
        yield return new WaitForSecondsRealtime(5f);
        
        //Load death scene
        if (!isWin)
        {
            SceneManager.LoadScene("Death");
        }
        else
        {
            Debug.LogError("Player won?");
        }
    }

    public void TogglePause(bool isFullPause)
    {
        if (isFullPause)
        {
            Time.timeScale = isPaused ? 1 : 0;
            
            isPaused = FlipBool(isPaused);
            
            return;
        }
        
        if (isPaused)
        {
            //UnPause
            OnResume?.Invoke(this, EventArgs.Empty);
            
        }
        else
        {
            //Pause
            OnPause?.Invoke(this, EventArgs.Empty);
        }

        isPaused = FlipBool(isPaused);
    }

    bool FlipBool(bool value)
    {
        return !value;
    }
}
