using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject plane;

    public Text scoreText;
    public Text deathsText;
    public Text multiplierText;

    int deaths;
    int score;
    int multiplier = 1;

    private void Awake()
    {
        Instance = this;
        plane = GameObject.FindGameObjectWithTag("Ground");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    float showingHitTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        showingHitTimer -= Time.deltaTime;
        if (showingHitTimer <= 0)
        {
            plane.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
    }

    public void ShowHit()
    {
        showingHitTimer = 0.2f;
        plane.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void ShowHitReversion()
    {
        showingHitTimer = 0.2f;
        plane.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    internal void PlayerDeath()
    {
        deaths++;
        deathsText.text = $"Deaths: {deaths}";
    }
    internal void AddScore()
    {
        score += 10 * multiplier;
        scoreText.text = $"Score: {score}";
    }

    internal void SetMultiplier()
    {
        multiplier *= 2;
        multiplier = Mathf.Clamp(multiplier, 1, 8);
        multiplierText.text = $"Multiplier: x{multiplier}";
    }

    internal void ResetMultiplier()
    {
        multiplier = 1;
        multiplierText.text = $"Multiplier: x{multiplier}";
    }
}
