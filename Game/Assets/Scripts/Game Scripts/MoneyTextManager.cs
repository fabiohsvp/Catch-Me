using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI moneyDisplay;

    public void UpdateCoinCount()
    {
        moneyDisplay.text = "" + playerInventory.numberOfMoney;
    }
}
