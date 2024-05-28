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
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float timeBetweenAttacks = 1;
    private bool attacking = false;

    [Header("Object references")]
    [SerializeField] private GameObject deathVX;
    [SerializeField] private GameObject damageVX;
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
        FindObjectOfType<WaveManager>().currentAmountOfEnemies++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            playerVector2 = new Vector2(transform.position.x, transform.position.z);
            originVector2 = new Vector2(origin.transform.position.x, origin.transform.position.z);
            playerToOriginVector2 = originVector2 - playerVector2;
            direction = new Vector3(playerToOriginVector2.x, 0, playerToOriginVector2.y);
            direction = Vector3.Normalize(direction);
            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        }

        if(transform.position.y < -10)
        {
            Die();
        }
    }

    public void DamageTaken(int damageTaken)
    {
        health += -damageTaken;
        if (health <= 0)
        {
            Die();
        }
        Instantiate(damageVX, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        FindObjectOfType<CurrencyManager>().UpdateMoney(money);
        FindObjectOfType<WaveManager>().currentAmountOfEnemies--;
        Instantiate(deathVX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "defence")
        {
            attacking = true;
            StartCoroutine(Attacking(collision));
        }
    }

    private IEnumerator Attacking(Collision collision)
    {
        GameObject wall = collision.gameObject;
        while (wall != null)
        {
            collision.transform.GetComponent<Defence>().DamageTaken(attackDamage);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        attacking = false;
    }

}
