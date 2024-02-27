using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma
{

    public int Id { get; set; }
    public string Nome { get; set; }
    public int Ataque { get; set; }
    public double Preco { get; set; }

    public Arma()
    {

    }

    public Arma(int id, string nome, int ataque, double preco)
    {
        Id = id;
        Nome = nome;
        Ataque = ataque;
        Preco = preco;
    }

}