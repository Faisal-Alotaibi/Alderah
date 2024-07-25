using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    public Transform bulletPrefab;
    public Transform bulletSpawnPoint;
    public int maxBullets = 10;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private int currentBullets;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // currentBullets = maxBullets;
    }

    void Update()
    {
        Move();
        Jump();
       // Shoot();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * speed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && currentBullets > 0)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            currentBullets--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void Reload(int bulletAmount)
    {
        currentBullets = Mathf.Min(currentBullets + bulletAmount, maxBullets);
    }
}
