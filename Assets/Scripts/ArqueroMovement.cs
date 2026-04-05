using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArqueroMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Transform arqueroTransform;

    [SerializeField]
    
    float velocityx = 5;

    Animator animator;

    bool isAttacking;
    

    int comboStep = 0;
    float comboTimer = 0.5f;
    float lastClickTime = 0;

    [SerializeField]
    float jumpForce = 8f;

    bool isGrounded = true;

     public enum ArqueroDir
    {
        Right,
        Left
    }

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        arqueroTransform = transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.linearVelocityX));

       if (!Keyboard.current.aKey.isPressed &&
        !Keyboard.current.dKey.isPressed &&
        !Keyboard.current.leftArrowKey.isPressed &&
        !Keyboard.current.rightArrowKey.isPressed)
        {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (isAttacking)
        return;
    }

     public void OnJump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

     //Presionar X para atacar, si vuelves a presionar, salta al siguiente ataque
    public void OnAttack()
    {
        if (Time.time - lastClickTime > comboTimer)
        {
            comboStep = 0;
        }

        comboStep++;
        lastClickTime = Time.time;

        if (comboStep == 1)
        {
            animator.SetTrigger("attack1");
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("attack2");
            comboStep = 0;
        }
    }

     public void OnDeath()
    {
        animator.SetTrigger("death");
    }

    public void OnHurt()
    {
        animator.SetTrigger("hurt");
    }


    public void OnMoveRight()
    {
        rb.linearVelocity = new Vector2(velocityx, rb.linearVelocity.y);
        FlipDir(ArqueroDir.Right);
    }

    public void OnMoveLeft()
    {
        rb.linearVelocity = new Vector2(-velocityx, rb.linearVelocity.y);
        FlipDir(ArqueroDir.Left);
    }
    


    private void FlipDir(ArqueroDir direction)
    {
        float dirMultiplier = 1f;

        if (direction == ArqueroDir.Left)
            dirMultiplier = -1f;

            arqueroTransform.localScale = new Vector3(
            Mathf.Abs(arqueroTransform.localScale.x) * dirMultiplier,
            arqueroTransform.localScale.y,
            arqueroTransform.localScale.z
        );
    }

}
