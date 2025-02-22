using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Animator marioAnimator;  // Referencia al Animator de Mario
    public GameObject mario;  // Referencia al objeto Mario

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Asegúrate de que el Animator de Mario esté asignado correctamente
            if (marioAnimator != null)
            {
                // Cambia el parámetro "isBig" en el Animator
                marioAnimator.SetBool("isBig", true);  // Activa la animación para Mario grande
            }

         

            Destroy(gameObject); 
        }
    }
}
