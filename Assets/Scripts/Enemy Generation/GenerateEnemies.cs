using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    [SerializeField] private GameObject theEnemy;
    [SerializeField] private int xPos;
    [SerializeField] private int zPos;
    [SerializeField] private int enemyCount;

    [SerializeField] private float radius = 30f;
    [SerializeField] Transform origin;

    [SerializeField] private BoxCollider box;
    
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        int i = 0;
        while (i < enemyCount)
        {
            xPos = Random.Range(167, 190);
            zPos = Random.Range(61, 123);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            i += 1;
        }
    }

    IEnumerator EnemyDropBox()
    {
        while (enemyCount < 50)
        {
            //xPos = box.bounds.extends.x
            //zPos = box.bounds.extends.y
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }

    private void Update()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float xComponent = Mathf.Cos(angle) * radius;
        float zComponent = Mathf.Sin(angle) * radius;

        Vector3 position = new Vector3(origin.position.x + xComponent, 1, origin.position.z + zComponent);

        Instantiate(theEnemy, position, Quaternion.identity);
    }

}
