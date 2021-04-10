using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //the options for inputs
    InputMap inputMap;
    //the horizontal speed at which the object moves
    [SerializeField] private float moveSpeed;
    //the vertical push that causes the object to jump
    [SerializeField] private float jumpStrength;
    //modifies the gravityScale of this object's Rigidbody2D
    [SerializeField] private float gravityScale;

    //whether or not this object is on the ground
    private bool onGround;
    //whether or not this character has performed their double jump
    private bool didDoubleJump;
    //the amount of collisions this object is making with the ground
    private int groundCollisionNum;

    [SerializeField] private bool usingDoubleJump;

    //a reference to this object's Rigidbody2D
    private Rigidbody2D rb;

    private void Awake()
    {
        //initialize the inputMap and check the first frame of a jump
        inputMap = new InputMap();
        inputMap.Gameplay.Jump.performed += ctx => Jump();
    }

    //enable and disable the inputMap at the proper times
    private void OnEnable()
    {
        inputMap.Enable();
    }

    private void OnDisable()
    {
        inputMap.Disable();
    }
    
    void Start()
    {
        //initialize variables
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        onGround = false;
        didDoubleJump = false;
        groundCollisionNum = 0;
    }
    
    void Update()
    {
        //check for moving and whether the player is on the ground
        Move();
        CheckIfOnGround();
    }

    public void Move()
    {
        float moveDirection = inputMap.Gameplay.Move.ReadValue<float>();

        //if you use a positive button, move right
        if (moveDirection > 0.0f)
        {
            transform.position += new Vector3(moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        //if you use a negative button, move left
        else if (moveDirection < 0.0f)
        {
            transform.position += new Vector3(-moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    private void CheckIfOnGround()
    {
        //if you are collding with at least one ground, set that you are onGround and not didDoubleJump
        if (groundCollisionNum > 0)
        {
            onGround = true;
            didDoubleJump = false;
        }
        //otherwise, the object must not be on the ground
        else
        {
            onGround = false;
        }
    }

    private void Jump()
    {
        //if you jump, reset the y velocity and apply an upwards force
        if (onGround)
        {
            float jumping = inputMap.Gameplay.Jump.ReadValue<float>();

            if (jumping >= 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                rb.AddForce(new Vector2(0.0f, jumpStrength));
            }
        }
        //if you jump but aren't on the ground, you can double jump once
        //this option is modular and set in the inspector
        else if (usingDoubleJump && !didDoubleJump)
        {
            float jumping = inputMap.Gameplay.Jump.ReadValue<float>();

            if (jumping >= 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                rb.AddForce(new Vector2(0.0f, jumpStrength));
            }

            didDoubleJump = true;
        }
    }

    //check how many ground objects this object is colliding with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundCollisionNum++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundCollisionNum--;
        }
    }
}
