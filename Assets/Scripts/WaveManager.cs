using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public int currentAmountOfEnemies = 0;
    private int currentWave = 1;
    public bool isWaveSpawning = true;
    public List<GenerateEnemies> waves;
    [SerializeField] private TextMeshProUGUI waveText;

    private void Start()
    {
        waves[currentWave - 1].gameObject.SetActive(true);
        StartCoroutine(DisplayText("Wave " + currentWave.ToString()));
    }

    private void Update()
    {
        //When the wave is over
        if(!isWaveSpawning && currentAmountOfEnemies <= 0)
        {
            StartCoroutine(DisplayText("A wave is coming soon..."));
            currentAmountOfEnemies = 0;
            isWaveSpawning = true;
            currentWave++;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(15);
        currentAmountOfEnemies = 0;
        isWaveSpawning = true;
        waves[currentWave - 1].gameObject.SetActive(true);
        StartCoroutine(DisplayText("Wave " + currentWave.ToString()));
    }

    private IEnumerator DisplayText(string text)
    {
        waveText.text = text;
        yield return new WaitForSeconds(5);
        waveText.text = null;
    }
}
