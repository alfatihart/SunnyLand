using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask fallGround;

    private Collider2D collider;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //Translation from Jump to Fall
        if (animator.GetBool("Jumping"))
        {
            if (rigidbody.velocity.y < .1)
            {
                animator.SetBool("Falling", true);
                animator.SetBool("Jumping", false);
            }
        }
        if (collider.IsTouchingLayers(ground) && animator.GetBool("Falling"))
        {
            animator.SetBool("Falling", false);
        }
        else if (collider.IsTouchingLayers(fallGround))
        {
            Debug.Log("Katak mati");
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            // Test to see if we are beyond the leftCap
            if (transform.position.x > leftCap)
            {
                // Make sure sprite is facing right location, and if it is not, then face the right direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                // Test to see if I am on the ground, if so jump
                if (collider.IsTouchingLayers(ground))
                {
                    // Jump
                    rigidbody.velocity = new Vector2(-jumpLength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            // Test to see if we are beyond the rightCap
            if (transform.position.x < rightCap)
            {
                // Make sure sprite is facing right location, and if it is not, then face the right direction
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                // Test to see if I am on the ground, if so jump
                if (collider.IsTouchingLayers(ground))
                {
                    // Jump
                    rigidbody.velocity = new Vector2(jumpLength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
