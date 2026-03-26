using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletInit : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField]
    private float forceH = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      rb.AddForce(transform.right * forceH, ForceMode2D.Impulse);   
    }


}
