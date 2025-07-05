using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    //Slight dependency. Visual needs a player which makes sense logicaly so this fine :)
    Player player;
    Rigidbody2D playerRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {   
        //Cache player ref
        player = GetComponentInParent<Player>();
        //Uses player ref to get RigidBody
        playerRigidBody = player.GetRigidbody2D();
    }

    // Update is called once per frame
    void Update()
    {
        ManageVisualRotation();
    }

    void ManageVisualRotation()
    {
        if (playerRigidBody.velocity.y == 0) return;
        
        this.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(playerRigidBody.velocity.y,-45,45));
    }
}
