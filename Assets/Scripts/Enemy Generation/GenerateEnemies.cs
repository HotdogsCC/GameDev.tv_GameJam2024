using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    [SerializeField] private GameObject theEnemy;
    [SerializeField] private int enemyCount;

    [SerializeField] private float radius = 30f;
    [SerializeField] Transform origin;
    
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        int i = 0;
        while (i < enemyCount)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            float xComponent = Mathf.Cos(angle) * radius;
            float zComponent = Mathf.Sin(angle) * radius;

            Vector3 position = new Vector3(origin.position.x + xComponent, 1, origin.position.z + zComponent);

            Instantiate(theEnemy, position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            i += 1;
        }
    }

}
