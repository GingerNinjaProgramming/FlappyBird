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
    List<Vector2> pipePositionsList = new List<Vector2>();
    
    //Dicates how much space player has to play with. Smaller more difficult it will be
    [SerializeField] int pipeOpeningOffset;
    
    [SerializeField] int maxAdditionalSpeed;
    [SerializeField] int spawnRate;
    
    float spawnTimer;
    int spawnedPipesCount = 0;
    
    //To toggle pipe spawning
    bool isSpawning = true;
    
    void Start()
    {
        GameManger.instance.OnPause += PipeManager_OnPause;
        GameManger.instance.OnResume += PipeManager_OnResume;
        
        pipePositionsList = GeneratePipePositions();
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
            spawnPos = pipePositionsList[Random.Range(0, pipePositionsList.Count)];
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

    List<Vector2> GeneratePipePositions()
    {
        List<Vector2> pipePositions = new List<Vector2>();
        
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (i + j > 20 + pipeOpeningOffset)
                {
                   pipePositions.Add(new Vector2(i, j));
                }
            }
        }
        return pipePositions;
    }
}
