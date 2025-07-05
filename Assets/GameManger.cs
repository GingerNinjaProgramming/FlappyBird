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

    public void AddScore(float value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public IEnumerator GameEnd(bool isWin)
    {
        yield return new WaitForSecondsRealtime(5f);
        
        //Load death scene
        if (!isWin)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("Player won?");
        }
    }

    public void TogglePause()
    {
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
    }
}
