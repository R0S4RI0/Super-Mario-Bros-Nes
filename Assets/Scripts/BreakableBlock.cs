using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float breakForce = 10f; 

    // Direcciones para cada parte del bloque
    private Vector2[] forceDirections = new Vector2[]
    {
        new Vector2(-1, 1),   // Arriba-izquierda
        new Vector2(1, 1),    // Arriba-derecha
        new Vector2(-1, -1),  // Abajo-izquierda
        new Vector2(1, -1)    // Abajo-derecha
    };

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player") && collision.transform.localScale.x > 1)
        {
           
            BreakBlock();
        }
    }

    private void BreakBlock()
    {
      
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

        
            child.gameObject.SetActive(true);

            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
              
                rb.bodyType = RigidbodyType2D.Dynamic;

                
                Vector2 forceDirection = forceDirections[i];

               
                rb.AddForce(forceDirection * breakForce, ForceMode2D.Impulse);
            }
        }

        
        GetComponent<Collider2D>().enabled = false;

        
        GetComponent<SpriteRenderer>().enabled = false;
    }
}