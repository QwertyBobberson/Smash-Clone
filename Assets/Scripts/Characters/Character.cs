using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public Player player;
    public Rigidbody rb;

    public float health;
    public int speed;
    public int jumpForce;
    public int maxJumps;
    public float acceleration;

    public float move;

    private int jumps;
    private bool hasUpAttacked;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        health = 0;

        jumps = maxJumps;
    }

    private void FixedUpdate()
    {
        rb.AddForce(move * speed, 0, 0);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);

        rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, move * speed, acceleration), rb.velocity.y, 0);

        if(rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    public void SetMove(float input)
    {
        move = input;
    }

    public void Jump()
    {
        if(jumps > 0)
        {
            rb.AddForce(0, jumpForce, 0);
            jumps--;
        }
    }

    public virtual void UpStandard()
    {
    }

    public virtual void SideStandard()
    {
    }

    public virtual void DownStandard()
    {
    }

    public virtual void NeutralStandard()
    {
    }

    public virtual void UpSpecial()
    {
    }

    public virtual void SideSpecial()
    {
    }

    public virtual void DownSpecial()
    {
    }

    public virtual void NeutralSpecial()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
            jumps = maxJumps;
            hasUpAttacked = false;
        }
    }
}
