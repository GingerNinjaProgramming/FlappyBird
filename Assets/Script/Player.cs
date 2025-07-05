using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int force;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ManageMovement();
        }
    }

    /// <summary>
    /// Manages all functionality to do with movement (its not much... :0 )
    /// </summary>
    void ManageMovement()
    {
        rb.velocity = Vector2.zero;
        
        rb.AddForce(Vector2.up * force,ForceMode2D.Impulse);
        
    }
    
    //On Collision = Bonk(Collision with detection)
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerDie();
    }
    
    //On Trigger = Phantom Pain(Detection without actual collision)
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDie();
    }

    /// <summary>
    /// To be ran in any circumstance the player dies to run end of game code
    /// </summary>
    void PlayerDie()
    {
        force = 0;
        
        GameManger.instance.TogglePause();
        
        //Runs coroutine to stop instant scene switching 
        StartCoroutine(GameManger.instance.GameEnd(false));
    }
}
