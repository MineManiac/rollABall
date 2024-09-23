using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public int totalCoins = 8; // Set this to the total number of collectible coins
    private int count;
    public TextMeshProUGUI countText;

    public Vector3 respawnPoint;
    public float fallThreshold = -10f;

    public AudioSource pickupSound; // Reference to AudioSource component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        respawnPoint = transform.position;

        // Get the AudioSource component
        pickupSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // Check if the player is grounded
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void Update()
    {
        // Check if the player falls below the fallThreshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero; // Stop any movement
        transform.position = respawnPoint; // Reset position to the respawn point
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            // Play the pickup sound
            pickupSound.Play();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Check if the player is grounded
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (isGrounded)
        {
            Vector3 jump = new Vector3(0.0f, 400.0f, 0.0f); // Adjust the jump force as needed
            rb.AddForce(jump);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    public int GetCount()
    {
        return count; // Return the current count of picked coins
    }
}
