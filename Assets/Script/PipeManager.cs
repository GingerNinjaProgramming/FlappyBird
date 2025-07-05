using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PipeManager : MonoBehaviour
{
    [SerializeField] GameObject pipePrefab;

    [SerializeField] Vector2[] pipePositions;
    
    //Dicates how much space player has to play with. Smaller more difficult it will be
    [SerializeField,Range(5,20)] int pipeOpeningOffset;
    
    [SerializeField] int maxAdditionalSpeed;
    [SerializeField] int spawnRate;
    
    float spawnTimer;
    int spawnedPipesCount = 0;
    
    //To toggle pipe spawning
    bool isSpawning = true;


    void Awake()
    {
        GameManger.instance.OnPause += PipeManager_OnPause;
        GameManger.instance.OnResume += PipeManager_OnResume;
    }

    void PipeManager_OnResume(object sender, EventArgs e)
    {
        isSpawning = true;
    }

    void PipeManager_OnPause(object sender, EventArgs e)
    {
        isSpawning = false;
    }

    void Update()
    {
        if (!isSpawning) return;
        
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            //Spawn pipe
            SpawnPipe();

            //Reset timer
            spawnTimer = 0;

            spawnedPipesCount++;
            Time.timeScale = Mathf.Clamp(Time.timeScale + 1f, 0f, 1f);
        }
    }

    void SpawnPipe()
    {
        Vector2 spawnPos;

        if (Random.value < 0.2f)
        {
            spawnPos = pipePositions[Random.Range(0, pipePositions.Length)];
        }
        else
        {
            while (true)
            {
                spawnPos = new Vector2(Random.Range(0, 15), Random.Range(0, 15));

                if (spawnPos.x + spawnPos.y > 20 + pipeOpeningOffset)
                {
                    break;
                }
            }
        }

        GameObject topPipe = Instantiate(pipePrefab, this.transform.position, Quaternion.identity);
        GameObject bottomPipe = Instantiate(pipePrefab, this.transform.position, Quaternion.identity);
        
        topPipe.transform.position += new Vector3(0, spawnPos.x, 0);
        bottomPipe.transform.position -= new Vector3(0, spawnPos.y, 0);

        int newPipeSpeed = Mathf.Clamp(maxAdditionalSpeed * (spawnedPipesCount / 100), 1, 10);
        Debug.Log(newPipeSpeed);
        
        topPipe.GetComponent<Pipe>().SetSpeed(newPipeSpeed);
        bottomPipe.GetComponent<Pipe>().SetSpeed(newPipeSpeed);
    }
}
