using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    [Header("Build Mode Variables")]
    [SerializeField] float buildRayDistance = 10f;
    
    [Header("Attack Mode Variables")]
    [SerializeField] float bulletRayDistance = 100f;
    [SerializeField] int gunDamage = 1;
    [SerializeField] float firingRate = 1;

    [Header("Materials")]
    [SerializeField] private Material canPlaceMat;
    [SerializeField] private Material cantPlaceMat;
    [SerializeField] private Material towerMat;

    [Header("Object References")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject gunGO;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private Transform muzzleFlashLocation;

    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI modeText;

    private bool inBuildMode = true;
    private bool lookingAtSomwthingICanSpawn = false;
    private bool canShoot = true;
    private GameObject currentSelection;

    // Start is called before the first frame update
    void Start()
    {
        if (inBuildMode)
        {
            modeText.text = "Build Mode";

            gunGO.SetActive(false);
            currentSelection = Instantiate(wallPrefab, new Vector3(0, -500, 0), Quaternion.identity);
        }
        else
        {
            modeText.text = "Attack Mode";

            gunGO.SetActive(true);
            Destroy(currentSelection);
            currentSelection = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuildMode)
        {
            BuildMode();
        }
        else
        {
            AttackMode();
        } 

        //Switches Mode, currently right click
        if (Input.GetMouseButtonDown(1))
        {
            SwitchModes();
        }
    }

    private void BuildMode()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, buildRayDistance))
        {
            lookingAtSomwthingICanSpawn = true;
            currentSelection.GetComponent<MeshRenderer>().material = canPlaceMat;
            currentSelection.transform.position = hit.point;
            currentSelection.transform.eulerAngles = new Vector3(0, playerCamera.transform.eulerAngles.y - 90, 0);
        }
        else
        {
            lookingAtSomwthingICanSpawn = false;
            currentSelection.GetComponent<MeshRenderer>().material = cantPlaceMat;
        }

        //Left Click Input
        if (Input.GetMouseButtonDown(0))
        {
            //Checks player can spawn in the thing
            if (lookingAtSomwthingICanSpawn)
            {
                //Checks player has enough money
                if (currencyManager.GetCurrentMoneyBalance() >= currencyManager.wallCost)
                {
                    currencyManager.UpdateMoney(-currencyManager.wallCost);
                    currentSelection.GetComponent<MeshRenderer>().material = towerMat;
                    currentSelection = Instantiate(wallPrefab);
                }
            }
        }
    }

    private void AttackMode()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            //Gun go boom
            Instantiate(muzzleFlashPrefab, muzzleFlashLocation.position, muzzleFlashLocation.rotation);
            canShoot = false;
            StartCoroutine(WaitAndThen(firingRate, "gunWait"));
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
    }

    private void SwitchModes()
    {
        inBuildMode = !inBuildMode;

        if (inBuildMode)
        {
            modeText.text = "Build Mode";

            gunGO.SetActive(false);
            currentSelection = Instantiate(wallPrefab);
        }
        else
        {
            modeText.text = "Attack Mode";

            gunGO.SetActive(true);
            Destroy(currentSelection);
            currentSelection = null;
        }
    }

   private IEnumerator WaitAndThen(float time, string thing)
    {
        yield return new WaitForSeconds(time);

        switch (thing)
        {
            case "gunWait":
                canShoot = true;
                break;

            default:
                Debug.Log("there is an issue with waitandthen");
                break;
        }
    }
}
