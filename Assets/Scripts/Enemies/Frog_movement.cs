﻿using UnityEngine;

public class Frog_movement : MonoBehaviour
{
    // [SerializeField] private float leftCap;
    // [SerializeField] private float rightCap;

    // [SerializeField] private float jumpLength = 5f;
    [SerializeField]
    private float jumpHeight = 10f;

    [SerializeField]
    private LayerMask ground;

    private Collider2D  coll;
    private Rigidbody2D rb;
    public  Animator    anim;

    // private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetInteger("State") == 1)
        {
            if (rb.velocity.y < 0)
            {
                anim.SetInteger("State", 2);
            }
        }

        if (coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(0, jumpHeight);
            anim.SetInteger("State", 1);
        }

        if (anim.GetBool("Death"))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    // // Update is called once per frame
    // private void Update()
    // {
    //     if(facingLeft)
    //     {
    //         if(transform.position.x > leftCap)
    //         {
    //             if(transform.localScale.x != 1)
    //             {
    //                 transform.localScale = new Vector3(1,1);
    //             }
    //             if(coll.IsTouchingLayers(ground))
    //             {
    //                 rb.velocity = new Vector2(-jumpLength, jumpHeight);
    //             }
    //         }
    //         else
    //         {
    //             facingLeft = false;
    //         }
    //     }
    //     else
    //     {
    //         if(transform.position.x < rightCap)
    //         {
    //             if(transform.localScale.x != -1)
    //             {
    //                 transform.localScale = new Vector3(-1,1);
    //             }
    //             if(coll.IsTouchingLayers(ground))
    //             {
    //                 rb.velocity = new Vector2(jumpLength, jumpHeight);
    //             }
    //         }
    //         else
    //         {
    //             facingLeft = true;
    //         }
    //     }
    // }
}