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
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject enemySwivelPoint;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector2 enemyVector2;
    private Vector2 originVector2;
    private Vector2 playerVector2; 
    private Vector2 enemyToOriginVector2;
    private Vector2 enemyToPlayerVector2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origin = GameObject.FindGameObjectWithTag("origin");
        player = FindObjectOfType<PlayerMovement>();
        FindObjectOfType<WaveManager>().currentAmountOfEnemies++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            enemyVector2 = new Vector2(transform.position.x, transform.position.z);
            originVector2 = new Vector2(origin.transform.position.x, origin.transform.position.z);
            enemyToOriginVector2 = originVector2 - enemyVector2;
            moveDirection = new Vector3(enemyToOriginVector2.x, 0, enemyToOriginVector2.y);
            moveDirection = Vector3.Normalize(moveDirection);
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

            //playerVector2 = new Vector2(player.transform.position.x, player.transform.position.z);
            //enemyToPlayerVector2 = playerVector2 - enemyVector2;
            //float feta = Mathf.Atan2(enemyToPlayerVector2.x, enemyToPlayerVector2.y);
            float feta = Mathf.Atan2(enemyToOriginVector2.x, enemyToOriginVector2.y);
            enemySwivelPoint.transform.eulerAngles = new Vector3(0, feta * Mathf.Rad2Deg, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if(transform.position.y < -10)
        {
            SilentlyDie();
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
        Instantiate(deathVX, transform.position, Quaternion.identity);
        FindObjectOfType<CurrencyManager>().UpdateMoney(money);
        SilentlyDie();
    }

    private void SilentlyDie()
    {
        FindObjectOfType<WaveManager>().currentAmountOfEnemies--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "defence")
        {
            attacking = true;
            StartCoroutine(Attacking(collision));
        }

        if(collision.transform.tag == "origin")
        {
            collision.transform.GetComponent<Tree>().Damaged();
            SilentlyDie();
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
