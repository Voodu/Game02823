using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_vertical: MonoBehaviour
{
    [SerializeField] private float upCap;
    [SerializeField] private float downCap;

    [SerializeField] private float speed = 10f;
    private Collider2D coll;
    private Rigidbody2D rb;
    public Animator anim;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(anim.GetBool("Death")==true)
        {
            rb.velocity = new Vector2(0,0);
        }
        if(transform.position.y > upCap)
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else if(transform.position.y < downCap)
        {
            rb.velocity = new Vector2(0, speed);
        }
    }
}