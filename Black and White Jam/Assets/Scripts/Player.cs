using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputMap inputMap;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravityScale;

    private bool onGround;
    private bool didDoubleJump;
    private int groundCollisionNum;

    private Rigidbody2D rb;

    private void Awake()
    {
        inputMap = new InputMap();
        inputMap.Gameplay.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        inputMap.Enable();
    }

    private void OnDisable()
    {
        inputMap.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        onGround = false;
        didDoubleJump = false;
        groundCollisionNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfOnGround();
    }

    public void Move()
    {
        float moveDirection = inputMap.Gameplay.Move.ReadValue<float>();

        if (moveDirection > 0.0f)
        {
            transform.position += new Vector3(moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        else if (moveDirection < 0.0f)
        {
            transform.position += new Vector3(-moveSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    private void CheckIfOnGround()
    {
        if (groundCollisionNum > 0)
        {
            onGround = true;
            didDoubleJump = false;
        }
        else
        {
            onGround = false;
        }
    }

    private void Jump()
    {
        if (onGround)
        {
            float jumping = inputMap.Gameplay.Jump.ReadValue<float>();

            if (jumping >= 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                rb.AddForce(new Vector2(0.0f, jumpStrength));
            }
        }
        else if (!didDoubleJump)
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
