using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Importar el SceneManager

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;  // Fuerza de salto
    public float walkSpeed = 5f;   // Velocidad de caminar
    public float runSpeed = 10f;   // Velocidad de correr
    public LayerMask groundLayer;  // Capa del suelo
    public Color rayColor = Color.red;  // Color del rayo
    public float raycastLength = 0.6f;  // Longitud del rayo
    private float rayMargin = 0.1f;  // Margen para ajustar los rayos hacia adentro

    private Rigidbody2D rb;
    private float moveInput;
    private bool isRunning = false;  // Indica si Mario est� corriendo
    private bool isGrounded = false;

    private Animator animator;  // Referencia al Animator
    private Collider2D col;  // Declara la variable para el Collider2D

    // Variable para controlar el estado de crecimiento (si Mario es grande o peque�o)
    private bool isBig = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtener el Rigidbody2D
        col = GetComponent<Collider2D>();  // Obtener el collider para las dimensiones
        animator = GetComponent<Animator>();  // Obtener el Animator
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); // Obtiene el input horizontal

        CheckJump();

        // Actualiza los par�metros del Animator
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));  // Velocidad de movimiento
        animator.SetBool("isJumping", !isGrounded);  // Si est� saltando
        animator.SetBool("isRunning", isRunning);  // Si est� corriendo
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Mueve al jugador
    void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector2 force = new Vector2(moveInput * currentSpeed * 2f, 0);

        rb.AddForce(force, ForceMode2D.Force); // Aplica la fuerza de manera progresiva

        // Limita la velocidad m�xima en X para evitar aceleraciones infinitas
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -currentSpeed, currentSpeed), rb.velocity.y);

        // Cambia la direcci�n de Mario
        if (moveInput < 0)  // Si se mueve hacia la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInput > 0)  // Si se mueve hacia la derecha
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // Detecta si est� corriendo
        isRunning = Input.GetKey(KeyCode.UpArrow);

        // si la velocidad es menor a 0.1f Mario se para 
        if (Mathf.Abs(rb.velocity.x) < 0.1f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Verifica si el jugador est� en el suelo
    public bool IsGrounded()
    {
        Vector2 leftRayOrigin = new Vector2(transform.position.x - col.bounds.extents.x + rayMargin, transform.position.y);
        Vector2 rightRayOrigin = new Vector2(transform.position.x + col.bounds.extents.x - rayMargin, transform.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, raycastLength, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, raycastLength, groundLayer);

        return leftHit.collider != null || rightHit.collider != null;
    }

    // Controla el salto
    void CheckJump()
    {
        isGrounded = IsGrounded();  // Verifica si est� tocando el suelo

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))  // Si est� en el suelo y presionamos espacio
        {
            Jump();
        }
    }

    // Realiza el salto
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Establece la velocidad vertical del salto
    }

    // M�todo para recibir da�o cuando el jugador es tocado por un enemigo
    public void TakeDamage()
    {
        // Si Mario es grande, reduce su tama�o y lo hace peque�o
        if (isBig)
        {
            Shrink();
        }
        else
        {
            // Aplica una peque�a fuerza hacia arriba al morir
            rb.velocity = new Vector2(rb.velocity.x, 5f); // Ajusta el valor seg�n lo que necesites

            // Cambiar la capa de "Player" a una capa que no interact�e con los objetos del juego
            gameObject.layer = LayerMask.NameToLayer("Death");  // Aseg�rate de que la capa "Death" exista en el Editor de Unity

            // Desactivar el Collider2D para que Mario no colisione con el mundo mientras cae
            col.enabled = false;

            animator.SetTrigger("Die"); // Activa la animaci�n de muerte
            StartCoroutine(Respawn());  // Espera para reiniciar el nivel
        }
    }

    // M�todo para hacer crecer a Mario (cuando recoge power-ups)
    public void Grow()
    {
        isBig = true;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // Aumenta el tama�o de Mario
    }

    // M�todo para hacer que Mario se haga m�s peque�o (cuando pierde una vida)
    public void Shrink()
    {
        isBig = false;
        transform.localScale = new Vector3(1f, 1f, 1f); // Restaura el tama�o original de Mario
    }

    // M�todo que resetear al jugador despu�s de morir
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);  // Espera 2 segundos para la animaci�n de muerte

        // Restaura la capa y habilita el collider
        gameObject.layer = LayerMask.NameToLayer("Player");  // Restaura la capa de "Player"
        col.enabled = true;  // Habilita el collider para que Mario interact�e con el mundo nuevamente

        RestartLevel();  // Reinicia el nivel
    }

    // M�todo para reiniciar el nivel
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Recarga la escena actual
    }

    private void OnDrawGizmos()
    {
        if (col == null)
        {
            col = GetComponent<Collider2D>();  // Si col es null, lo inicializamos aqu�
        }

        if (col != null)
        {
            Gizmos.color = rayColor;
            Gizmos.DrawRay(new Vector2(transform.position.x - col.bounds.extents.x + rayMargin, transform.position.y), Vector2.down * raycastLength);
            Gizmos.DrawRay(new Vector2(transform.position.x + col.bounds.extents.x - rayMargin, transform.position.y), Vector2.down * raycastLength);
        }
    }
}
