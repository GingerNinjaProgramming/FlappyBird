using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //This script has a problem with having no null checks though curruntly they are not nessisary gud to keep in mind
    
    [SerializeField] GameObject target;
    
    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.x != transform.position.x)
        {
                transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
