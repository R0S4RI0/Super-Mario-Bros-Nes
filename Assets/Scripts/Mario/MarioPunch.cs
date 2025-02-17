using UnityEngine;

public class MarioPunch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Block block = collision.gameObject.GetComponent<Block>();
            if (block != null)
            {
                block.Hit();  // Llama al método de golpeo en la clase Block
            }
        }
    }
}
