using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{

    Rigidbody2D rb;
    Transform playerTransform;

    [SerializeField]
    
    float velocityx = 5;

    Animator animator;
    

    public enum PlayerDir
{
    Right,
    Left
}

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.linearVelocityX));
    }

   public void OvenMoveRight()
    {
        rb.linearVelocity = new Vector2(velocityx, rb.linearVelocityY);
        FlipDir(PlayerDir.Right);
        Debug.Log("Moviendo Right");
    }

    public void OvenMoveLeft()
    {
        rb.linearVelocity = new Vector2(-5, rb.linearVelocityY);
        FlipDir(PlayerDir.Left);
        Debug.Log("Moviendo Letft");
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

