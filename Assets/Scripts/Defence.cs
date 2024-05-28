using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject damagedFX;

    // Update is called once per frame
    public void DamageTaken(int damage)
    {
        health -= damage;
        Instantiate(damagedFX, transform.position, Quaternion.identity);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
