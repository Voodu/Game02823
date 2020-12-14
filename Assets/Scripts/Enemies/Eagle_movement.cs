using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_movement : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float upCap;
    [SerializeField] private float downCap;
    [SerializeField] private bool vertical = true;
    [SerializeField] private float speed = 10f;
    private Collider2D coll;
    private Rigidbody2D rb;
    public Animator anim;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(vertical){
            rb.velocity = new Vector2(0, speed);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(anim.GetBool("Death")==true)
        {
            rb.velocity = new Vector2(0,0);
        }
        if(vertical)
        {
            if(transform.position.y > upCap)
            {
                rb.velocity = new Vector2(0, -speed);
            }
            else if(transform.position.y < downCap)
            {
                rb.velocity = new Vector2(0, speed);
            }
        }
        else{
            if(facingLeft)
            {
                if(transform.position.x > leftCap)
                {
                    if(transform.localScale.x != 1)
                    {
                        transform.localScale = new Vector3(3,3);
                    }
                    rb.velocity = new Vector2(-speed, 0);
                }
                else
                {
                    facingLeft = false;
                }
            }
            else
            {
                if(transform.position.x < rightCap)
                {
                    if(transform.localScale.x != -1)
                    {
                        transform.localScale = new Vector3(-3,3);
                    }
                    rb.velocity = new Vector2(speed, 0);
                }
                else
                {
                    facingLeft = true;
                }
            }
        }
    }
}