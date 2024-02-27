using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;


public class Personagem
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Ataque { get; set; }
    public int Defesa { get; set; }
    public int Agilidade { get; set; }
    public int Health { get; set; }
    public int ArmaId { get; set; }



    public Personagem()
    {

    }

    public Personagem(int id, string nome, int ataque, int defesa, int agilidade, int health, int armaId)
    {
        Id = id;
        Nome = nome;
        Ataque = ataque;
        Defesa = defesa;
        ArmaId = armaId;
        Agilidade = agilidade;
        Health = health;
    }
}
