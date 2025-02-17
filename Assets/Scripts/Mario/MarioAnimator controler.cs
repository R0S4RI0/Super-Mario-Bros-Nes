using UnityEngine;

public class MarioAnimator : MonoBehaviour
{
    private Animator animator;
    private Player marioMove;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        marioMove = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isJumping", rb.velocity.y > 0.1f);
    }
}
