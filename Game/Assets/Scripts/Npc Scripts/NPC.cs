using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject moneyIconPrefab; // Arraste o Prefab do �cone de dinheiro aqui na interface Unity

    private GameObject moneyIcon; // Refer�ncia ao �cone de dinheiro instanciado

    private void Start()
    {
        // Chame esta fun��o para gerar o �cone de dinheiro aleatoriamente em um NPC.
        GenerateMoneyIcon();
    }

    // M�todo para gerar o �cone de dinheiro
    public void GenerateMoneyIcon()
    {
        // Certifique-se de que o �cone de dinheiro n�o tenha sido gerado anteriormente
        if (moneyIcon == null)
        {
            // Instancia o �cone de dinheiro como um objeto filho do NPC
            moneyIcon = Instantiate(moneyIconPrefab, transform);
            moneyIcon.transform.localPosition = Vector3.up * 2; // Ajuste a posi��o conforme necess�rio
        }
    }

    // M�todo para remover o �cone de dinheiro
    public void RemoveMoneyIcon()
    {
        if (moneyIcon != null)
        {
            Destroy(moneyIcon);
        }
    }

    // L�gica de intera��o com o NPC
    public void Interact()
    {
        // Implemente sua l�gica de intera��o aqui
    }
}

