using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : GameSaveManager
{

    public GameObject main;
    public GameObject options;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneSave.itemDescription);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Credits()
    {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void Return()
    {
        main.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);
    }
}
