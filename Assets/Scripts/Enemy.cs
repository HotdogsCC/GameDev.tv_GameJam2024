using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int health = 1;
    [SerializeField] private int money = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageTaken(int damageTaken)
    {
        health += -damageTaken;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<CurrencyManager>().UpdateMoney(money);
        Destroy(gameObject);
    }
}
