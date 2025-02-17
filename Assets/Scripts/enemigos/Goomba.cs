using UnityEngine;
using System.Collections;  // Add this to include IEnumerator

public class Goomba : MonoBehaviour, IEnemy  // Implementa la interfaz IEnemy
{
    public float moveSpeed = 2f;
    private Vector2 direction = Vector2.left;

    private Rigidbody2D rb;
    private Animator animator;  // Referencia al Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();  // Obtener el Animator
    }

    void Update()
    {
        if (!animator.GetBool("isDead")) // Si no está muerto, mueve al Goomba
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x)); // Actualiza la velocidad
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animator.GetBool("isDead"))
        {
            if (collision.gameObject.CompareTag("Tuberia") || collision.gameObject.CompareTag("enemigo"))
            {
                direction *= -1;  // Cambia la dirección
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                // Si Mario lo pisa desde arriba
                if (collision.contacts[0].normal.y < -0.5f)
                {
                    Die(); // Mata al Goomba
                    // collision.gameObject.GetComponent<Player>().Bounce();   //  hace que Mario rebote
                }
                else
                {
                    collision.gameObject.GetComponent<Player>().TakeDamage();
                }
            }
        }
    }

    // Implementación del método de la interfaz IEnemy
    public void TakeDamage()
    {
        // Aquí se maneja el daño del Goomba
        Die();
    }

    public void Die()
    {
        animator.SetBool("IsDead", true); // Activa la animación de muerte
        rb.velocity = Vector2.zero; // Detiene el movimiento
        StartCoroutine(DestroyAfterAnimation()); // Destruye el objeto después de un tiempo
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos
        Destroy(gameObject); // Destruye el objeto
    }
}
