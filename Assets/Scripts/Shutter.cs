using UnityEngine;
using UnityEngine.InputSystem;

public class Shutter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
     
    [SerializeField]
     private float cadence = 0.1f;
     [SerializeField]
     private float timeToShoot = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed && timeToShoot == 0f)
        {
            Debug.Log("Shoot!");
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            timeToShoot = 1 / cadence;
        }   
    }

    void FixedUpdate()
    {
        timeToShoot = timeToShoot > 0f ? timeToShoot - Time.fixedDeltaTime : 0f;
    }

}
