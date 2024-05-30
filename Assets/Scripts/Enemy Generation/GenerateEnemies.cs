using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int startingMoney;

    [SerializeField] private WaveManager waveManager;

    [SerializeField] private float radius = 30f;
    [SerializeField] Transform origin;
    
    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        StartCoroutine(EnemyDrop());
        FindObjectOfType<CurrencyManager>().UpdateMoney(startingMoney);
    }

    IEnumerator EnemyDrop()
    {
        waveManager.isWaveSpawning = true;
        int i = 0;
        while (i < enemies.Count)
        {


            float angle = Random.Range(0, 2 * Mathf.PI);
            float xComponent = Mathf.Cos(angle) * radius;
            float zComponent = Mathf.Sin(angle) * radius;

            Vector3 position = new Vector3(origin.position.x + xComponent, 60, origin.position.z + zComponent);

            Instantiate(enemies[i], position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            i += 1;
        }
        waveManager.isWaveSpawning = false;
    }

}
