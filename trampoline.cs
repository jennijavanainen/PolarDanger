using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampoline : MonoBehaviour
{
    public Animator anim;
    public GameObject Feet;
    public Rigidbody2D rb;

    private void Start()
    {
        anim.SetBool("isBoing", false);
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(rb.velocity.x, 0);
        rb.velocity = movement;
    }

    private void Update()
    {
        if (rb.velocity.x > 0.11f || rb.velocity.x < -0.11f)
        {
            anim.SetBool("isMoving", true);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D feet = Physics2D.OverlapCircle(Feet.transform.position, 0.3f);

        if (feet)
        {
            anim.SetBool("isBoing", true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D feet = Physics2D.OverlapCircle(Feet.transform.position, 0.3f);

        if (feet)
        {
            anim.SetBool("isBoing", false);
        }
    }

}
