using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Persistence.DAO.Implementation;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System;

public class GameController : MonoBehaviour
{

    void Start()
    {

        print("Armas Cadastradas");
        var w = GamesCodeDataSource.Instance.ArmaDAO.GetArma(1);
        print("Personagemes Cadastrados");
        var x = GamesCodeDataSource.Instance.PersonagemDAO.GetPersonagem(1);
        print("Armas e respectivos Personagemes que as usam");
        GamesCodeDataSource.Instance.QueryDAO.ArmaINNERPersonagem();
        print("Todas Armas Cadastradas e Personagemes que as usam");
        GamesCodeDataSource.Instance.QueryDAO.ArmaLEFTPersonagem();
    }



    // Update is called once per frame
    void Update()
    {

    }
}