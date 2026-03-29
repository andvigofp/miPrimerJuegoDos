using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemnt : MonoBehaviour
{

    Rigidbody2D rb;
    Transform playerTransform;

    [SerializeField]
    
    float velocityx = 5;

    Animator animator;

    bool isAttacking;
    
    bool isBlocking = false;

      int comboStep = 0;
    float comboTimer = 0.5f;
    float lastClickTime = 0;

    public enum PlayerDir
    {
        Right,
        Left
    }

   void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTransform = transform;
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

         // Si está bloqueando y sigues pulsando C
        if (isBlocking && Keyboard.current.cKey.isPressed)
        {
            animator.SetBool("block", true);
        }
        else
        {
            animator.SetBool("block", false);
            isBlocking = false;
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
        }
        else if (comboStep == 3)
        {
            animator.SetTrigger("attack3");
            comboStep = 0;
        }
    }

    public void OnBlock()
    {
        isBlocking = true;
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
        FlipDir(PlayerDir.Right);
    }

    public void OnMoveLeft()
    {
        rb.linearVelocity = new Vector2(-velocityx, rb.linearVelocity.y);
        FlipDir(PlayerDir.Left);
    }  
    private void FlipDir(PlayerDir direction)
    {
        float dirMultiplier = 1f;

        if (direction == PlayerDir.Left)
            dirMultiplier = -1f;

            playerTransform.localScale = new Vector3(
            Mathf.Abs(playerTransform.localScale.x) * dirMultiplier,
            playerTransform.localScale.y,
            playerTransform.localScale.z
        );
    }
}

