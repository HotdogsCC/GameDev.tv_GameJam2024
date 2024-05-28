using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentAmountOfEnemies = 0;
    private int currentWave = 1;
    public bool isWaveSpawning = true;
    public List<GenerateEnemies> waves;

    private void Start()
    {
        waves[currentWave - 1].gameObject.SetActive(true);
    }

    private void Update()
    {
        //When the wave is over
        if(!isWaveSpawning && currentAmountOfEnemies <= 0)
        {
            currentAmountOfEnemies = 0;
            isWaveSpawning = true;
            currentWave++;
            waves[currentWave - 1].gameObject.SetActive(true);
        }
    }
}
