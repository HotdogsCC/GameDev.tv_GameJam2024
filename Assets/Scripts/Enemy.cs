using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int health = 1;
    [SerializeField] private int money = 1;
    [SerializeField] private float moveSpeed = 5;

    [Header("Object references")]
    [SerializeField] private GameObject deathVX;
    [SerializeField] private GameObject origin;
    private Rigidbody rb;
    private Vector3 direction;
    private Vector2 playerVector2;
    private Vector2 originVector2;
    private Vector2 playerToOriginVector2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origin = GameObject.FindGameObjectWithTag("origin");

        playerVector2 = new Vector2(transform.position.x, transform.position.z);
        originVector2 = new Vector2(origin.transform.position.x, origin.transform.position.z);
        playerToOriginVector2 = originVector2 - playerVector2;
        direction = new Vector3(playerToOriginVector2.x, 0, playerToOriginVector2.y);
        direction = Vector3.Normalize(direction);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        //rb.velocity = direction * moveSpeed;
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
        FindObjectOfType<WaveManager>().currentAmountOfEnemies--;
        Instantiate(deathVX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
