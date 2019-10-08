using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawned = 0;
    }

    private const float InitialSpawnRate = 1.6f;
    float spawnRate = InitialSpawnRate;
    private long spawned;
    private float spawnTimer = InitialSpawnRate;

    private bool stopped;

    
    [Range(0,1)] public float tresholdUp = 0.8f;
    [Range(0,1)] public float tresholdDown = 0.8f;
    private int audioBand = 0;
    // Update is called once per frame
    private float timeLimit;
    public float minTimeBetweenWalls = 2f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //AudioPeer.Pause();
            stopped = !stopped;
        }
        if (stopped) { return; }

        timeLimit -= Time.deltaTime;

        if (timeLimit < 0) {
            var graces = new bool[8];
            for(int i = 0; i < 8; i++) {
                graces[i] = AudioPeer.audioBand[i] > tresholdDown && AudioPeer.audioBand[i] < tresholdUp;
            }   
            if (graces.Any(x => x == true)) {
                SpawnWall(graces);
            }
            timeLimit = minTimeBetweenWalls;
        }
       

       
    }

    void SpawnWall(bool[] graces)
    {
        spawned++;
        var wall = Instantiate(wallPrefab, this.transform.position, this.transform.rotation);
        wall.GetComponent<Wall>().SetGraces(graces);
    }

    public void Stop()
    {
        this.stopped = true;
    }
    public void Reset()
    {
        this.stopped = false;
        this.spawnRate = InitialSpawnRate;
        this.spawned = 0;
        this.spawnTimer = InitialSpawnRate;
    }
}
