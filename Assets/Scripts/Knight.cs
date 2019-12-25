using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 200f;
    public LayerMask whatIsGround;
    public float checkGroundRadius = 0.2f;
    public int extraJumps = 1;

    int remaningJumps;
    bool isOnFloor;
    bool isJumping;
    Rigidbody2D body;
    SpriteRenderer sprite;
    Animator anim;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        remaningJumps = extraJumps;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") && remaningJumps > 0)
        {
            isJumping = true;
            remaningJumps--;
        }

        if (isOnFloor)
        {
            remaningJumps = extraJumps;
        }

        PlayerAnimation();

        isOnFloor = body.IsTouchingLayers(whatIsGround);
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(move * speed, body.velocity.y);
        if ((move > 0 && sprite.flipX) || (move < 0 && !sprite.flipX))
        {
            FLip();
        }

        if (isJumping)
        {
            body.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }

        if (body.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            body.velocity += Vector2.up * -0.8f;
        }

        if (Input.GetButtonDown("Fire1")) {
            anim.SetTrigger("meleeAtk");
        }
    }

    void FLip()
    {
        sprite.flipX = !sprite.flipX;
    }

    void PlayerAnimation() {
        anim.SetFloat("VelX",Mathf.Abs(body.velocity.x));
        anim.SetFloat("VelY", Mathf.Abs(body.velocity.y)); 
    }
}
