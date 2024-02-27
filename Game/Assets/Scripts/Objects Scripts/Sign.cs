using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    
    public GameObject interfaceUi;
    public Text dialogText;
    public string dialog;
    public GameObject eButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (interfaceUi.activeInHierarchy)
            {
                interfaceUi.SetActive(false);
            }
            else
            {
                interfaceUi.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            eButton.SetActive(true);
            //context.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            eButton.SetActive(false);
            playerInRange = false;
            interfaceUi.SetActive(false);
        }
    }

}
