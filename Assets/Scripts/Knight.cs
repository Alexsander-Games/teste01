using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 200f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkGroundRadius = 0.2f;

    bool isOnFloor;
    bool isJumping;
    Rigidbody2D body;
    SpriteRenderer sprite;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        isJumping = (Input.GetButtonDown("Jump") && isOnFloor);
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, checkGroundRadius, whatIsGround);
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(move * speed, body.velocity.y);
        if ((move > 0 && sprite.flipX) || (move < 0 && !sprite.flipX)) {
            FLip();
        }

        if (isJumping) {
            body.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }

        if (body.velocity.y > 0f && !Input.GetButton("Jump")) {
            body.velocity += Vector2.up * -0.8f;
        }
    }

    void FLip() {
        sprite.flipX = !sprite.flipX;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,checkGroundRadius);
    }
}
