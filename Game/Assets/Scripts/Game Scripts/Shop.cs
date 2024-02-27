using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Shop : MonoBehaviour
{
    public Item id;
    public Item passport;
    public Inventory playerInventory;
    public GameObject idOff;
    public GameObject passOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (playerInventory.numberOfMoney >= 10 && !playerInventory.items.Contains(id))
        {
            idOff.SetActive(false);
        }
        else
        {
            idOff.SetActive(true);
        }

        if (playerInventory.numberOfMoney >= 30 && !playerInventory.items.Contains(passport) && currentScene.name != "Main")
        {
            passOff.SetActive(false);
        }
        else
        {
            passOff.SetActive(true);
        }
    }

    public void BuyId()
    {
        if(playerInventory.numberOfMoney >= 10 && !playerInventory.items.Contains(id))
        {
            playerInventory.numberOfMoney -= 10;
            playerInventory.AddItem(id);
            playerInventory.currentItem = id;
        }
    }
    public void BuyPass()
    {
        if (playerInventory.numberOfMoney >= 30 && !playerInventory.items.Contains(passport))
        {
            playerInventory.numberOfMoney -= 30;
            playerInventory.AddItem(passport);
            playerInventory.currentItem = passport;
        }
    }
}
