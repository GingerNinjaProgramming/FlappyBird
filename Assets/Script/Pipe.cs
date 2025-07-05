using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    int speed = 1;
    int preStopSpeed;
    bool scoreTrigger = false;

    void Awake()
    {
        GameManger.instance.OnPause += Pipe_OnPause;
        GameManger.instance.OnResume += Pipe_OnResume;
    }

    void Pipe_OnResume(object sender, EventArgs e)
    {
        ToggleStop(false);
    }

    void Pipe_OnPause(object sender, EventArgs e)
    {
        ToggleStop(true);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this,30);
        
        transform.Translate(Vector2.left * (speed * Time.deltaTime));

        if (scoreTrigger) return; 
        
        if ((int)transform.position.x == 0)
        {
            GameManger.instance.AddScore(0.5f);
            scoreTrigger = true;
        }
        
    }
    
    public void SetSpeed(int value)
    {
        speed = value;
    }

    void ToggleStop(bool isStopping)
    {
        if (isStopping)
        {
            preStopSpeed = speed;
        }
        speed = isStopping ? 0 : preStopSpeed;
    }

    
}
