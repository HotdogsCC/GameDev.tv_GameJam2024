using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour
{
    [SerializeField]
    GameObject playerCamera;

    RaycastHit hit;
    if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, bulletRayDistance))
{
    //Something was hit
    if(hit.transform.TryGetComponent<Enemy>(out Enemy enemy))
    {
        //Enemy was hit
        enemy.DamageTaken(gunDamage);
    }
}
}
