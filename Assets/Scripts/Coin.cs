using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 100;  // Puntos que otorga la moneda

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Si el jugador entra en contacto con la moneda
        {
            // Suma los puntos al puntaje
            ScoreManager.Instance.AddScore(points);

            // Desactiva la moneda
            Destroy(gameObject);
        }
    }
}
