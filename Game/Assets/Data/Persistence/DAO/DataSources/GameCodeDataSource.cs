using Assets.Scripts.Persistence.DAO.Implementation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamesCodeDataSource : SQliteDataSource
{
    public PersonagemDAO PersonagemDAO { get; protected set; }
    public ArmaDAO ArmaDAO { get; protected set; }
    public QueryDAO QueryDAO { get; private set; }

    private static GamesCodeDataSource instance;
    /* Este instance vai retornar instancia. Se for Nulo vai pesquisar na Cena se já tem o get criado. 
       Se não tiver, será criado um. */

    public static GamesCodeDataSource Instance
    /* Cada vez que ocorrer um get, tentará retornar o instance (minúsculo) que é privado.
       Este método de Instance( Maiusculo) só será feito no primeiro acesso, pois nos seguintes 
       o instance (minúsculo) já terá uma referência e no teste "if (instance == null)" somente 
       retornará a instância */

    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GamesCodeDataSource>();

                if (instance == null)
                {
                    var gamesCodeDataSourceObject = new GameObject("GamesCodeDataSource");
                    instance = gamesCodeDataSourceObject.AddComponent<GamesCodeDataSource>();
                    DontDestroyOnLoad(gamesCodeDataSourceObject);
                }
            }

            return instance;
        }
    }


    protected new void Awake()
    {
        this.databaseName = "GameAula.db";
        this.copyDatabase = true;

        try
        {
            Debug.Log("this.databaseName :" + this.databaseName);

            base.Awake();

            PersonagemDAO = new PersonagemDAO(this);
            if (PersonagemDAO == null)
            {
                Debug.LogError("PersonagemDAO não foi inicializado corretamente.");
            }
            ArmaDAO = new ArmaDAO(this);
            QueryDAO = new QueryDAO(this);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Database not created! {ex.Message}");
        }
        print("Awake Esperado");
    }
}