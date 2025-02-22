using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 5f;  // Rango de detecci�n de Mario
    public float moveSpeed = 2f;      // Velocidad de movimiento del enemigo
    private Transform mario;          // Referencia a Mario
    private bool isMoving = false;    // Si el enemigo est� movi�ndose hacia Mario

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        mario = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar si Mario est� dentro del rango de visi�n
        if (Vector2.Distance(transform.position, mario.position) <= detectionRange)
        {
            isMoving = true;  // Comienza a moverse hacia Mario
        }
        else
        {
            isMoving = false;  // Deja de moverse si Mario est� fuera de rango
        }

        if (isMoving)
        {
            // Mueve el enemigo hacia Mario
            direction = (mario.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero; // Det�n el movimiento si Mario no est� cerca
        }
    }
}
