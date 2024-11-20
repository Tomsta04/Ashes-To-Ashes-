using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
     [SerializeField] float moveSpeed = 6;
    Animator anim;
    Rigidbody2D rb;

    int maxHealth = 100;
    int currentHealth;

    bool dead = false;

    float moveHorizontal, moveVertical;
    Vector2 movement;

    int facingDirection = 1; // 1 = right, -1 = left

    private void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Only for testing
        if (Input.GetKeyDown(KeyCode.Space))
           Hit(10);


       
        if (dead) 
        {   
            movement = Vector2.zero;
            anim.SetFloat("Velocity", 0);
            return;
        }


        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical"); 

        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        anim.SetFloat("Velocity", movement.magnitude);

        if (movement.x != 0)
           facingDirection = movement.x > 0 ? 1 : -1;
        
        transform.localScale = new Vector2(facingDirection, 1);
    }

    private void FixedUpdate()
     {
        rb.velocity = movement * moveSpeed;
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          // Check if we collide with an enemey
     }

     void Hit(int damage)
     {
        anim.SetTrigger("Hit");
        currentHealth -= damage;
        healthText.text = Mathf.Clamp(currentHealth, 0, maxHealth).ToString();
     }

     void Die()
     {
        dead = true;
        // Call GameOver
     }

}
