using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //AudioPeer.Pause();
            stopped = !stopped;
        }
        if (stopped) { return; }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnWall();
            spawnTimer = spawnRate;
        }

        if (spawned > 20)
        {
            spawnRate = Mathf.Clamp(spawnRate * 0.9f, 1f, InitialSpawnRate);
            spawned = 0;
        }
    }

    void SpawnWall()
    {
        spawned++;
        Instantiate(wallPrefab, this.transform.position, Quaternion.identity);
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
