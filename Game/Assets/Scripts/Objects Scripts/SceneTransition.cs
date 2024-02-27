using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Sign
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public Inventory playerInventory;
    public Item idContent;
    public Item passContent;
    public bool isBus, isAirport;
    public bool cutScene;


    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isBus && !isAirport) 
            {
                playerStorage.initialValue = playerPosition;
                StartCoroutine(FadeCo());
            }
            if (isBus && !playerInventory.items.Contains(idContent))
            {
                interfaceUi.SetActive(true);
                StartCoroutine(HideMessageAfterDelay(1.5f));
            }
            if (isBus && playerInventory.items.Contains(idContent))
            {
                playerStorage.initialValue = playerPosition;
                StartCoroutine(FadeCo());
            }
            if (isAirport && !playerInventory.items.Contains(passContent))
            {
                interfaceUi.SetActive(true);
                StartCoroutine(HideMessageAfterDelay(1.5f));
            }
            if (isAirport && playerInventory.items.Contains(passContent))
            {
                playerStorage.initialValue = playerPosition;
                StartCoroutine(FadeCo());
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (cutScene)
            {
                playerStorage.initialValue = playerPosition;
                StartCoroutine(FadeCo());
            }
            else
            {
                playerInRange = true;
                eButton.SetActive(true);
                //SceneManager.LoadScene(sceneToLoad);
            }

        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = false;
            eButton.SetActive(false);
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void End()
    {
        StartCoroutine(FadeCo());
    }

    public IEnumerator FadeCo()
    {
        if(fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Torna o ícone invisível após o atraso
        interfaceUi.SetActive(false);
    }

}
