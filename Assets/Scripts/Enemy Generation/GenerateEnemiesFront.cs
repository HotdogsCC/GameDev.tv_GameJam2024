using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemiesFront : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    public BoxCollider box;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 25)
        {
            xPos = Random.Range(92, 128);
            zPos = Random.Range(177, 196);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
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
}
