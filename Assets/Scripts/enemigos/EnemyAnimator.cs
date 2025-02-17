using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;  // Referencia al Animator del enemigo

    // Estados de los enemigos
    public bool isPatrolling = true;  // Indica si está patrullando
    public bool isDead = false;  // Indica si el enemigo está muerto 

    void Start()
    {
        animator = GetComponent<Animator>();  // Obtén el Animator del enemigo
    }

    void Update()
    {
        // Actualiza las animaciones de acuerdo al estado
        animator.SetBool("isPatrolling", isPatrolling);
        animator.SetBool("isDead", isDead);
    }

    // Llama a esta función cuando el Goomba o Koopa mueran
    public void Die()
    {
        isDead = true;  // Cambia el estado a muerto
        isPatrolling = false;  // Detiene la patrulla
    }

    // Llama a esta función cuando un Koopa se meta en su caparazón
    public void EnterShell()
    {
        isPatrolling = false;  // Detiene la patrulla
        isDead = true;  // El Koopa está "muerto" en su caparazón
    }

    // Llama a esta función cuando el Koopa sea pisado
    public void MoveShell(Vector2 direction)
    {
        // Cambiar la animación para mover el caparazón
        isDead = false;
        animator.SetBool("isPatrolling", false);
    }
}
