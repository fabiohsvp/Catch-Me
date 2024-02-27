using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreasureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public SignalGame raiseItem;
    //public GameObject dialogBox;
    //public Text dialogText;
    private Animator anim;
    public GameObject moneyIconPrefab;
    public GameObject moneyIconPlayer;
    private GameObject moneyIcon;
    public NPCManager npcManager;
    private float minRespawnTime = 5.0f;
    private float maxRespawnTime = 15.0f;
    private float individualRespawnTime;
    public int maxMoneyToSteal = 3; // Quantidade m�xima de dinheiro que o jogador pode roubar
    private int currentMoneyStolen = 0; // Acompanha a quantidade de dinheiro roubada atual
    private bool isStealingEnabled = true; // Vari�vel para controlar se o jogador pode continuar roubando
    public SignalGame powerupSignal;
    public GameObject eButton;

    // Start is called before the first frame update
    void Start()
    {
        //powerupSignal.Raise();
        anim = GetComponent<Animator>();
        if (isStealingEnabled)
        {
            individualRespawnTime = Random.Range(minRespawnTime, maxRespawnTime);
            InvokeRepeating("GenerateMoneyIcon", individualRespawnTime, individualRespawnTime);
        }
        else
        {
            RemoveMoneyIcon();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        powerupSignal.Raise();
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if(!isOpen)
            {
                //Open the chest
                OpenChest();
            }
            else
            {
                //Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        if (isStealingEnabled && currentMoneyStolen < maxMoneyToSteal)
        {
            moneyIconPlayer.SetActive(true);
            StartCoroutine(HideIconAfterDelay(1.5f));
            RemoveMoneyIcon();
            // Dialog window on
            //dialogBox.SetActive(true);
            // dialog text = contents text
            //dialogText.text = contents.itemDescription;
            // add contents to the inventory
            playerInventory.AddItem(contents);
            playerInventory.currentItem = contents;
            // Raise the signal to the player to animate
            //raiseItem.Raise();
            // raise the context clue
            //context.Raise();
            // set the chest to opened
            
            //anim.SetBool("opened", true);
            
            if (currentMoneyStolen >= maxMoneyToSteal)
            {
                isStealingEnabled = false;
            }
        }

    }

    IEnumerator HideIconAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Torna o �cone invis�vel ap�s o atraso
        moneyIconPlayer.SetActive(false);
    }

    public void ChestAlreadyOpen()
    {
        // Dialog off
        //dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        //raiseItem.Raise();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = true;
            if (moneyIcon != null)
            {
                eButton.SetActive(true);
                //context.Raise();
                
            }

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = false;
            if (moneyIcon != null)
            {
                eButton.SetActive(false);
                //context.Raise();
            }

        }
    }
    

    // M�todo para gerar o �cone de dinheiro
    public void GenerateMoneyIcon()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // Certifique-se de que o �cone de dinheiro n�o tenha sido gerado anteriormente
        if (moneyIcon == null && isStealingEnabled)
        {
            if(playerInventory.numberOfMoney <= 10 && currentScene.name == "Main")
            {
                // Instancia o �cone de dinheiro como um objeto filho do NPC
                moneyIcon = Instantiate(moneyIconPrefab, transform);
                moneyIcon.transform.localPosition = Vector3.up * 2; // Ajuste a posi��o conforme necess�rio
                isOpen = false;
            }
            if (playerInventory.numberOfMoney <= 30 && currentScene.name == "Airport")
            {
                // Instancia o �cone de dinheiro como um objeto filho do NPC
                moneyIcon = Instantiate(moneyIconPrefab, transform);
                moneyIcon.transform.localPosition = Vector3.up * 2; // Ajuste a posi��o conforme necess�rio
                isOpen = false;
            }

        }
    }

    // M�todo para remover o �cone de dinheiro
    public void RemoveMoneyIcon()
    {
        if (moneyIcon != null)
        {
            eButton.SetActive(false);
            isOpen = true;
            currentMoneyStolen += 1;
            //Debug.Log("Removing money icon." + moneyIcon.name);
            Destroy(moneyIcon);
            //Debug.Log("Money icon removed.");
        }
        else
        {
            Debug.Log("Money icon is null.");
        }
    }
}

