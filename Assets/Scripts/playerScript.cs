using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask terrainLayerMask;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spr;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    float Xaxis;

    bool canIdleAnim = true;
    bool canRunAnim = true;

    void Update()
    {
        Xaxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (Xaxis > 0) spr.flipX = false;
        else if (Xaxis < 0) spr.flipX = true;

        if(rb.velocity.y < -2) //Caida
        {
            anim.SetTrigger("fall");
            canIdleAnim = true;
            canRunAnim = true;
        }
        else if(rb.velocity.y > 2) //Salto
        {
            anim.SetTrigger("jump");
            canIdleAnim = true;
            canRunAnim = true;
        }
        else
        {
            if (Xaxis == 0) IdleAnim();
            else RunAnim();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * Xaxis * speed);
    }

    bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, 1.5f, terrainLayerMask);
        if (ray.collider != null && ray.collider.CompareTag("Terrain"))
        {
            return true;
        }
        else return false;
    }

    void IdleAnim()
    {
        if (canIdleAnim)
        {
            canIdleAnim = false;
            anim.SetTrigger("idle");
            canRunAnim = true;
        }
    }

    void RunAnim()
    {
        if (canRunAnim)
        {
            canRunAnim = false;
            anim.SetTrigger("run");
            canIdleAnim = true;
        }
    }
}
