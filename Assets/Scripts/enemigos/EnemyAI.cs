using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 5f;  // Rango de detección de Mario
    public float moveSpeed = 2f;      // Velocidad de movimiento del enemigo
    private Transform mario;          // Referencia a Mario
    private bool isMoving = false;    // Si el enemigo está moviéndose hacia Mario

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        mario = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar si Mario está dentro del rango de visión
        if (Vector2.Distance(transform.position, mario.position) <= detectionRange)
        {
            isMoving = true;  // Comienza a moverse hacia Mario
        }
        else
        {
            isMoving = false;  // Deja de moverse si Mario está fuera de rango
        }

        if (isMoving)
        {
            // Mueve el enemigo hacia Mario
            direction = (mario.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero; // Detén el movimiento si Mario no está cerca
        }
    }
}
