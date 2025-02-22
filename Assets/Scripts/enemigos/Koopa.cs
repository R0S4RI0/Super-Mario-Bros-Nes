using UnityEngine;

public class Koopa : MonoBehaviour, IEnemy  // Implementa la interfaz IEnemy
{
    public float moveSpeed = 2f;
    public float shellSpeed = 8f;
    private Vector2 direction = Vector2.left;

    private Rigidbody2D rb;
    private bool isShell = false; // Indica si el Koopa está en modo caparazón
    private bool isMovingShell = false; // Indica si el caparazón se está moviendo
    private EnemyAnimator enemyAnimator;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<EnemyAnimator>();  // Obtén la referencia al script de animaciones
    }

    void Update()
    {
        if (!isShell) // Si no está en el caparazón, se mueve normalmente
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
        else if (isMovingShell) // Si está en caparazón y se mueve
        {
            rb.velocity = new Vector2(shellSpeed * direction.x, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShell)
        {
            if (collision.gameObject.CompareTag("Tuberia") || collision.gameObject.CompareTag("enemigo"))
            {
                direction *= -1; // Cambia la dirección
                FlipSprite(); // Gira el sprite
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                // Si Mario lo pisa desde arriba
                if (collision.contacts[0].normal.y < -0.5f)
                {
                    EnterShell();  // Koopa entra en el caparazón
                    // Mueve el caparazón automáticamente
                    isMovingShell = true;
                    direction = (collision.transform.position.x < transform.position.x) ? Vector2.right : Vector2.left;
                    FlipSprite(); // Asegura que el sprite mire en la dirección correcta
                }
                else
                {
                    collision.gameObject.GetComponent<Player>().TakeDamage(); // Si no es desde arriba, Mario recibe daño
                }
            }
        }
        else if (isMovingShell)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().TakeDamage();  // Mario recibe daño si choca con el caparazón
            }
            else if (collision.gameObject.CompareTag("enemigo"))
            {
                Destroy(collision.gameObject); // Koopa mata a otro enemigo si lo golpea
            }
            else
            {
                direction *= -1; // Cambia la dirección al chocar con una pared
                FlipSprite(); // Gira el sprite
            }
        }
    }

    // Implementación del método de la interfaz IEnemy
    public void TakeDamage()
    {
        // Aquí se maneja el daño de Koopa
        Die();
    }

    public void Die()
    {
        enemyAnimator.Die(); // Llama al método Die en EnemyAnimator
        Destroy(gameObject); // Destruye el objeto Koopa
    }

    void EnterShell()
    {
        isShell = true;  // Establece el estado de Koopa como dentro del caparazón
        enemyAnimator.EnterShell();  // Cambia la animación a caparazón
    }

    void FlipSprite()
    {
        // Gira el sprite en el eje Y
        Vector3 scale = transform.localScale;
        scale.x = (direction.x > 0) ? -1 : 1;
        transform.localScale = scale;
    }
}
