using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class log : Enemy
{
    public Rigidbody2D myRigidbody;   // Referência ao Rigidbody2D do inimigo
    public Transform target;          // Referência ao objeto que o inimigo está perseguindo (jogador)
    public float chaseRadius;         // Raio de perseguição para iniciar a perseguição ao alvo
    public float attackRadius;        // Raio de ataque para iniciar o ataque ao alvo
    public Transform homePosition;    // Posição de ponto de retorno do inimigo
    public Animator anim;             // Referência ao componente Animator para controle das animações
    private float distanceToPlayer;   // Variável para rastrear a distância até o jogador
    public bool playerInRange;
    public SignalGame context;
    public bool contextRaised = false;
    public GameObject player;
    public GameObject gameOver;

    // Inicialização no início do jogo
    void Start()
    {
        currentState = EnemyState.idle;

        // Obtém referências e configura animações
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("wakeUp", true);
    }

    // Chamado a cada quadro fixo
    void FixedUpdate()
    {
        CheckDistance();
    }

    // Verifica a distância entre o inimigo e o jogador
    public virtual void CheckDistance()
    {
        distanceToPlayer = Vector3.Distance(target.position, transform.position);

        // Se o jogador estiver no alcance de ataque
        if (distanceToPlayer <= attackRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("idle", true);
            player.SetActive(false);
            gameOver.SetActive(true);
        }
        // Se o jogador estiver no alcance de perseguição
        else if (distanceToPlayer <= chaseRadius)
        {
            anim.SetBool("wakeUp", true);
            anim.SetBool("idle", false);
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);

            playerInRange = true;

            if (playerInRange && !contextRaised)
            {
                context.Raise();
                contextRaised = true;
            }
        }
        // Se o jogador estiver fora do alcance de perseguição
        else if (distanceToPlayer > chaseRadius)
        {
            anim.SetBool("wakeUp", true);
            Vector3 temp = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);

            playerInRange = false;

            if (!playerInRange && contextRaised)
            {
                context.Raise();
                contextRaised = false;
            }

            // Se o inimigo chegou ao ponto de retorno
            if (Vector3.Distance(transform.position, homePosition.position) < 0.1f)
            {
                ChangeState(EnemyState.idle);
                anim.SetBool("wakeUp", false);
            }
        }
    }

    // Define as animações de movimento
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    // Atualiza as animações de movimento com base na direção
    public void changeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    // Muda o estado do inimigo
    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
