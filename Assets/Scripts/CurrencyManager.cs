using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Balance")]
    [SerializeField] private int money = 0;

    [Header("Costs")]
    [SerializeField] public int wallCost = 1;

    [Header("TMPro")]
    [SerializeField] private TextMeshProUGUI moneyTextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMoney(int moneyChange)
    {
        money += moneyChange;
        moneyTextDisplay.text = "$" + money.ToString();
    }

    public int GetCurrentMoneyBalance()
    {
        return money;
    }
}
