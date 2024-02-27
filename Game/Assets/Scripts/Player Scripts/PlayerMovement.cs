using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk, attack, interact, stagger, idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;       // Estado atual do jogador
    public int speed;                    // Velocidade de movimento do jogador
    private Rigidbody2D myRigidbody;       // Referência ao Rigidbody2D do jogador
    private Vector3 change;                // Armazena o vetor de movimento do jogador
    private Animator animator;             // Referência ao componente Animator do jogador
    private PickUp pickUp;                 // Referência ao componente PickUp do jogador (não mostrado)
    public FloatValue currentHealth;       // Valor de vida atual do jogador (não mostrado)
    public SignalGame playerHealthSignal;  // Sinal para atualizar a UI da vida do jogador (não mostrado)
    public VectorValue startingPosition;   // Posição inicial do jogador (não mostrado)
    public Inventory playerInventory;       // Inventário do jogador (não mostrado)
    public SpriteRenderer receivedItemSprite; // Referência ao SpriteRenderer para itens recebidos (não mostrado)
    public SignalGame playerHit;
    //public SignalGame context;
    public Personagem personagem;

    // Start é chamado antes da primeira atualização do quadro
    void Start()
    {
        Debug.Log("Trying to get Personagem from PersonagemDAO");
        personagem = GamesCodeDataSource.Instance.PersonagemDAO.GetPersonagem(1);
        if (personagem != null)
        {
            speed = personagem.Agilidade;
            // Restante do seu código...
        }
        else
        {
            Debug.LogError("Personagem não foi inicializado corretamente em PlayerMovement.");
        }
        currentState = PlayerState.walk;        // Define o estado inicial como "andar"
        animator = GetComponent<Animator>();    // Obtém o componente Animator
        myRigidbody = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D
        animator.SetFloat("moveX", 0);         // Define a animação de movimento inicial
        animator.SetFloat("moveY", -1);        // Define a animação de movimento inicial
        pickUp = gameObject.GetComponent<PickUp>(); // Obtém o componente PickUp (não mostrado)
        pickUp.Direction = new Vector2(0, -1);  // Define a direção inicial do PickUp
        transform.position = startingPosition.initialValue; // Define a posição inicial
        //print(speed);
    }

    public void Update()
    {
        // Verifica se o botão de ataque foi pressionado e o jogador não está em um estado de ataque ou atordoamento
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo()); // Inicia a rotina de ataque
        }
    }

    // A atualização é chamada uma vez por quadro
    void FixedUpdate()
    {
        // Verifica se o jogador está em um estado de interação, se sim, retorna
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero; // Reinicia o vetor de movimento
        change.x = Input.GetAxisRaw("Horizontal"); // Obtém a entrada do eixo horizontal
        change.y = Input.GetAxisRaw("Vertical");   // Obtém a entrada do eixo vertical
        if (change.sqrMagnitude > .1f)
        {
            pickUp.Direction = change.normalized; // Define a direção do PickUp se houver movimento
        }

        // Se o jogador estiver nos estados de "andar" ou "parado", atualiza a animação e move o personagem
        if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    // Ataque
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true); // Ativa a animação de ataque
        currentState = PlayerState.attack;   // Define o estado como "ataque"
        yield return null;
        animator.SetBool("attacking", false); // Desativa a animação de ataque
        yield return new WaitForSeconds(0.5f); // Aguarda um curto período
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk; // Volta para o estado de "andar" se não estiver em interação
        }
    }

    // Função para levantar um item
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true); // Ativa a animação de receber item
                currentState = PlayerState.interact;    // Define o estado como "interação"
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite; // Define o sprite do item recebido
            }
            else
            {
                animator.SetBool("receive item", false); // Desativa a animação de receber item
                currentState = PlayerState.idle;         // Retorna ao estado "parado"
                receivedItemSprite.sprite = null;        // Limpa o sprite do item recebido
                playerInventory.currentItem = null;       // Limpa o item atual no inventário
            }
        }
    }

    // Atualiza a animação e move o personagem
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false); // Define a animação de movimento como "parado"
        }
    }

    // Move o personagem usando o Rigidbody2D
    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }

    // Função para atordoar o jogador (geralmente chamada por inimigos)
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;       // Reduz a vida do jogador
        playerHealthSignal.Raise();                 // Atualiza a UI de vida
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));     // Inicia a rotina de atordoamento
        }
        else
        {
            this.gameObject.SetActive(false);       // Desativa o jogador se a vida for zero ou menos
        }
    }

    // Atordoamento
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime); // Aguarda o tempo de atordoamento
            myRigidbody.velocity = Vector2.zero;        // Define a velocidade do Rigidbody como zero
            currentState = PlayerState.idle;           // Retorna ao estado "parado"
            myRigidbody.velocity = Vector2.zero;        // Garante que a velocidade seja zero novamente
        }
    }
}
