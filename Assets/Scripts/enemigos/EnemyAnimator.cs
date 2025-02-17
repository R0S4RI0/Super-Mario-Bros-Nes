using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;  // Referencia al Animator del enemigo

    // Estados de los enemigos
    public bool isPatrolling = true;  // Indica si est� patrullando
    public bool isDead = false;  // Indica si el enemigo est� muerto 

    void Start()
    {
        animator = GetComponent<Animator>();  // Obt�n el Animator del enemigo
    }

    void Update()
    {
        // Actualiza las animaciones de acuerdo al estado
        animator.SetBool("isPatrolling", isPatrolling);
        animator.SetBool("isDead", isDead);
    }

    // Llama a esta funci�n cuando el Goomba o Koopa mueran
    public void Die()
    {
        isDead = true;  // Cambia el estado a muerto
        isPatrolling = false;  // Detiene la patrulla
    }

    // Llama a esta funci�n cuando un Koopa se meta en su caparaz�n
    public void EnterShell()
    {
        isPatrolling = false;  // Detiene la patrulla
        isDead = true;  // El Koopa est� "muerto" en su caparaz�n
    }

    // Llama a esta funci�n cuando el Koopa sea pisado
    public void MoveShell(Vector2 direction)
    {
        // Cambiar la animaci�n para mover el caparaz�n
        isDead = false;
        animator.SetBool("isPatrolling", false);
    }
}
