using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado
    public Vector3 offset;             // Desplazamiento de la cámara respecto al jugador

    private Transform playerTransform; // Referencia al transform del jugador
    private float initialXPosition;    // Posición X inicial de la cámara
    private bool canMoveBackward = true; // Controla si la cámara puede moverse hacia atrás

    void Start()
    {
        // Busca al jugador por su tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Player'. Asegúrate de que el jugador tenga el tag 'Player'.");
        }

        // Guarda la posición X inicial de la cámara
        initialXPosition = transform.position.x;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calcula la posición deseada
            float desiredX = canMoveBackward ? playerTransform.position.x : Mathf.Max(playerTransform.position.x, initialXPosition);
            Vector3 desiredPosition = new Vector3(desiredX, transform.position.y, transform.position.z);

            // Suaviza el movimiento de la cámara hacia la posición deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition + offset, smoothSpeed);

            // Actualiza la posición de la cámara
            transform.position = smoothedPosition;
        }
    }

    public void BlockBackwardMovement()
    {
        canMoveBackward = false;
    }

    public void AllowBackwardMovement()
    {
        canMoveBackward = true;
    }
}