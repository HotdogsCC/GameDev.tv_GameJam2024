using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TurretTriggerDetection : MonoBehaviour
{
    [SerializeField] private Transform turretSwivel;
    [SerializeField] private int damage = 50;
    [SerializeField] private int fireRate = 2;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private GameObject particles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
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

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private void Update()
    {

        Swivel();
    }

    private void Swivel()
    {
        if (enemies.Count > 0)
        {
            particles.SetActive(true);
            if (enemies[0] != null)
            {
                Vector2 turretVector2 = new Vector2(transform.position.x, transform.position.z);
                Vector2 enemyVector2 = new Vector2(enemies[0].transform.position.x, enemies[0].transform.position.z);
                Vector2 playerToOriginVector2 = enemyVector2 - turretVector2;
                float feta = Mathf.Atan2(playerToOriginVector2.x, playerToOriginVector2.y);
                turretSwivel.transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * feta, 0);
            }
            else
            {
                enemies.Remove(enemies[0]);
                Swivel();
            }
        }
        else
        {
            particles.SetActive(false);
        }
    }

    private void DealDamage()
    {
        if (enemies.Count > 0)
        {
            if (enemies[0] != null)
            {
                enemies[0].DamageTaken(damage);
                StartCoroutine(Wait());
            }
            else
            {
                enemies.Remove(enemies[0]);
                DealDamage();
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
