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

    public int health;
    public int weight;
    public int speed;
    public int jumpForce;
    public int maxJumps;

    public float move;

    private int jumps;
    private bool hasUpAttacked;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        jumps = maxJumps;
    }

    private void FixedUpdate()
    {
        rb.AddForce(move * speed, 0, 0);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
            jumps = maxJumps;
            hasUpAttacked = true;
        }
    }
}
