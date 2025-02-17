using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado
    public Vector3 offset;             // Desplazamiento de la c�mara respecto al jugador

    private Transform playerTransform; // Referencia al transform del jugador
    private float initialXPosition;    // Posici�n X inicial de la c�mara
    private bool canMoveBackward = true; // Controla si la c�mara puede moverse hacia atr�s

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
            Debug.LogError("No se encontr� ning�n objeto con el tag 'Player'. Aseg�rate de que el jugador tenga el tag 'Player'.");
        }

        // Guarda la posici�n X inicial de la c�mara
        initialXPosition = transform.position.x;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calcula la posici�n deseada
            float desiredX = canMoveBackward ? playerTransform.position.x : Mathf.Max(playerTransform.position.x, initialXPosition);
            Vector3 desiredPosition = new Vector3(desiredX, transform.position.y, transform.position.z);

            // Suaviza el movimiento de la c�mara hacia la posici�n deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition + offset, smoothSpeed);

            // Actualiza la posici�n de la c�mara
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