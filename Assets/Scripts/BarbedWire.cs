using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbedWire : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private int fireRate = 2;
    [SerializeField] private int damage = 5;
    [SerializeField] private int health = 20;

    // Start is called before the first frame update
    void Start()
    {
        DealDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemies.Add(other.GetComponent<Enemy>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemies.Remove(other.GetComponent<Enemy>());
        }
    }

    private void DealDamage()
    {
        if (enemies.Count > 0)
        {
            health--;
            foreach (Enemy enemy in enemies)
            {
                enemy.DamageTaken(damage);
                StartCoroutine(Wait());
            }
        }
        else
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1 / (float)fireRate);
        DealDamage();
    }
}
