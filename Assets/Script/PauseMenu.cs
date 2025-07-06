using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            GameManger.instance.TogglePause(true);
        }
    }

    void ToggleMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ContinueButton()
    {
        GameManger.instance.TogglePause(true);
    }
}
