using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Animator marioAnimator;  // Referencia al Animator de Mario
    public GameObject mario;  // Referencia al objeto Mario

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Aseg�rate de que el Animator de Mario est� asignado correctamente
            if (marioAnimator != null)
            {
                // Cambia el par�metro "isBig" en el Animator
                marioAnimator.SetBool("isBig", true);  // Activa la animaci�n para Mario grande
            }

         

            Destroy(gameObject); 
        }
    }
}
