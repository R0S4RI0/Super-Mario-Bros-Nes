using UnityEngine;
using UnityEngine.SceneManagement;

public class VoidDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reiniciar el nivel si Mario cae al vac�o
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.CompareTag("enemigo"))
        {
            // Destruir enemigos que caigan al vac�o
            Destroy(collision.gameObject);
        }
    }
}
