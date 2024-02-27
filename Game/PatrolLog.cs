using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : log
{
    public Transform[] path;            // Pontos de interesse para patrulha
    public int currentPoint;            // Índice do ponto de interesse atual
    public Transform currentGoal;       // Ponto de interesse atual
    public float roundingDistance;      // Distância para considerar que o ponto foi alcançado
    private bool contextRaised = false;

    public override void CheckDistance()
    {
        // Verifica se o jogador está dentro do alcance de perseguição
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                // Move em direção ao jogador
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                anim.SetBool("wakeUp", true);
                anim.SetBool("idle", false);
                
                playerInRange = true;

                if (playerInRange && !contextRaised)
                {
                    context.Raise();
                    contextRaised = true;
                }
            }
        }
        // Se o jogador estiver dentro do alcance de ataque, entra no estado de espera
        else if (Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("idle", true);
            player.SetActive(false);
            gameOver.SetActive(true);
        }
        // Se o jogador estiver fora do alcance de perseguição
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                // Move em direção ao ponto de interesse atual
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                playerInRange = false;

                if (!playerInRange && contextRaised)
                {
                    context.Raise();
                    contextRaised = false;
                }
            }
            else
            {
                ChangeGoal(); // Altera o ponto de interesse quando chegar perto o suficiente
            }
        }
    }

    private void ChangeGoal()
    {
        // Altera o ponto de interesse para o próximo na lista
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
