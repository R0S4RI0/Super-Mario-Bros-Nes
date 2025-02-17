using UnityEngine;

public class MarioStomp : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, bounceForce);
            }
        }
    }
}
