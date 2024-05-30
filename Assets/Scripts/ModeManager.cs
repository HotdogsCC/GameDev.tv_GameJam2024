using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    [Header("Build Mode Variables")]
    [SerializeField] float buildRayDistance = 10f;
    
    [Header("Attack Mode Variables")]
    [SerializeField] float bulletRayDistance = 100f;
    [SerializeField] public int gunDamage = 1;
    [SerializeField] public float firingRate = 1;

    [Header("Prefabs")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject barbedWirePrefab;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;

    [Header("Materials")]
    [SerializeField] private Material canPlaceMat;
    [SerializeField] private Material cantPlaceMat;
    [SerializeField] private Material towerMat;
    [SerializeField] private Material barbedWireMat;
    [SerializeField] private Material turretMat1;
    [SerializeField] private Material turretMat2;

    [Header("Object References")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private GameObject gunGO;
    [SerializeField] private GameObject defenceInventory;
    [SerializeField] private Transform muzzleFlashLocation;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI modeText;
    [SerializeField] private Image uiSlot1;
    [SerializeField] private Image uiSlot2;
    [SerializeField] private Image uiSlot3;

    private int slotSelected = 1;
    private bool inBuildMode = true;
    private bool lookingAtSomwthingICanSpawn = false;
    private bool canShoot = true;
    private GameObject currentSelection;

    // Start is called before the first frame update
    void Start()
    {
        ModeReset();
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
        //UI Selection
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slotSelected = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slotSelected = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slotSelected = 3;
        }

        //Resets stuff
        uiSlot1.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        uiSlot2.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        uiSlot3.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Destroy(currentSelection);
        currentSelection = null;

        //Sets correct solour for selected slot
        switch (slotSelected)
        {
            case 1:
                uiSlot1.color = new Color(1, 1, 1, 0.5f);
                currentSelection = Instantiate(wallPrefab, new Vector3(0, -500, 0), Quaternion.identity);
                break;

            case 2:
                uiSlot2.color = new Color(1, 1, 1, 0.5f);
                currentSelection = Instantiate(barbedWirePrefab, new Vector3(0, -500, 0), Quaternion.identity);
                break;

            case 3:
                uiSlot3.color = new Color(1, 1, 1, 0.5f);
                currentSelection = Instantiate(turretPrefab, new Vector3(0, -500, 0), Quaternion.identity);
                break;


            default:
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, buildRayDistance, 255))
        {
            lookingAtSomwthingICanSpawn = true;
            if(currentSelection.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            {
                mesh.material = canPlaceMat;
            }
            else
            {
                foreach (MeshRenderer item in currentSelection.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material = canPlaceMat;
                }
            }
            currentSelection.transform.position = hit.point;
            currentSelection.transform.eulerAngles = new Vector3(0, playerCamera.transform.eulerAngles.y - 90, 0);
        }
        else
        {
            lookingAtSomwthingICanSpawn = false;
            if (currentSelection.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            {
                mesh.material = cantPlaceMat;
            }
            else
            {
                foreach (MeshRenderer item in currentSelection.GetComponentsInChildren<MeshRenderer>())
                {
                    item.material = cantPlaceMat;
                }
            }
        }

        //Makes selection red if not enough money
        switch (slotSelected)
        {
            case 1:
                if (currencyManager.GetCurrentMoneyBalance() < currencyManager.wallCost)
                {
                    if (currentSelection.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
                    {
                        mesh.material = cantPlaceMat;
                    }
                    else
                    {
                        foreach (MeshRenderer item in currentSelection.GetComponentsInChildren<MeshRenderer>())
                        {
                            item.material = cantPlaceMat;
                        }
                    }
                }
                break;

            case 2:
                if (currencyManager.GetCurrentMoneyBalance() < currencyManager.barbedWireCost)
                {
                    if (currentSelection.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
                    {
                        mesh.material = cantPlaceMat;
                    }
                    else
                    {
                        foreach (MeshRenderer item in currentSelection.GetComponentsInChildren<MeshRenderer>())
                        {
                            item.material = cantPlaceMat;
                        }
                    }
                }
                break;

            case 3:
                if (currencyManager.GetCurrentMoneyBalance() < currencyManager.turretCost)
                {
                    if (currentSelection.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
                    {
                        mesh.material = cantPlaceMat;
                    }
                    else
                    {
                        foreach (MeshRenderer item in currentSelection.GetComponentsInChildren<MeshRenderer>())
                        {
                            item.material = cantPlaceMat;
                        }
                    }
                }
                break;


            default:
                break;
        }

        //Left Click Input
        if (Input.GetMouseButtonDown(0))
        {
            //Checks player can spawn in the thing
            if (lookingAtSomwthingICanSpawn)
            {
                //Checks player has enough money, and then spawns
                switch (slotSelected)
                {
                    case 1:
                        if (currencyManager.GetCurrentMoneyBalance() >= currencyManager.wallCost)
                        {
                            currencyManager.UpdateMoney(-currencyManager.wallCost);
                            currentSelection.GetComponent<MeshRenderer>().material = towerMat;
                            currentSelection.GetComponent<BoxCollider>().enabled = true;
                            currentSelection = Instantiate(wallPrefab);
                        }

                        break;

                    case 2:
                        if (currencyManager.GetCurrentMoneyBalance() >= currencyManager.barbedWireCost)
                        {
                            currencyManager.UpdateMoney(-currencyManager.barbedWireCost);
                            currentSelection.GetComponent<MeshRenderer>().material = barbedWireMat;
                            currentSelection.GetComponent<BoxCollider>().enabled = true;
                            currentSelection = Instantiate(barbedWirePrefab);
                        }

                        break;

                    case 3:
                        if (currencyManager.GetCurrentMoneyBalance() >= currencyManager.turretCost)
                        {
                            currencyManager.UpdateMoney(-currencyManager.turretCost);
                            int i = 0;
                            foreach (MeshRenderer item in currentSelection.GetComponentsInChildren<MeshRenderer>())
                            {
                                if(i == 0)
                                {
                                    item.material = turretMat1;
                                }
                                else
                                {
                                    item.material = turretMat2;
                                }

                                
                                i++;
                            }
                            currentSelection.GetComponent<CapsuleCollider>().enabled = true;
                            currentSelection.GetComponentInChildren<SphereCollider>().enabled = true;
                            currentSelection = Instantiate(turretPrefab);
                        }
                        
                        break;

                    default:
                        break;
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
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, bulletRayDistance, 255))
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

        ModeReset();
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

    private void ModeReset()
    {
        if (inBuildMode)
        {
            modeText.text = "Build Mode";

            gunGO.SetActive(false);
            defenceInventory.SetActive(true);
        }
        else
        {
            modeText.text = "Attack Mode";

            gunGO.SetActive(true);
            defenceInventory.SetActive(false);
            Destroy(currentSelection);
            currentSelection = null;
        }
    }
}
