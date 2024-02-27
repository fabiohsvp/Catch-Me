using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject moneyIconPrefab; // Arraste o Prefab do ícone de dinheiro aqui na interface Unity

    private GameObject moneyIcon; // Referência ao ícone de dinheiro instanciado

    private void Start()
    {
        // Chame esta função para gerar o ícone de dinheiro aleatoriamente em um NPC.
        GenerateMoneyIcon();
    }

    // Método para gerar o ícone de dinheiro
    public void GenerateMoneyIcon()
    {
        // Certifique-se de que o ícone de dinheiro não tenha sido gerado anteriormente
        if (moneyIcon == null)
        {
            // Instancia o ícone de dinheiro como um objeto filho do NPC
            moneyIcon = Instantiate(moneyIconPrefab, transform);
            moneyIcon.transform.localPosition = Vector3.up * 2; // Ajuste a posição conforme necessário
        }
    }

    // Método para remover o ícone de dinheiro
    public void RemoveMoneyIcon()
    {
        if (moneyIcon != null)
        {
            Destroy(moneyIcon);
        }
    }

    // Lógica de interação com o NPC
    public void Interact()
    {
        // Implemente sua lógica de interação aqui
    }
}

