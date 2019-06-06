using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Automatically adds Components when Script is added to GameObject
[RequireComponent(typeof(Rigidbody2D))]     // Rigidbody2D is added if not already added
[RequireComponent(typeof(BoxCollider2D))]   // BoxCollider2D is added if not already added
public class Enemy_Walker : MonoBehaviour {

    // Reference Rigidbody2D through script
    // - Not shown in Inspector
    Rigidbody2D rb;

    // Handles movement speed of 'Enemy'
    // - Can be adjusted through Inspector while in Play mode
    // - Used to debug movements and test the 'speed' of the 'Enemy'
    public float speed;

    // Handles Character flipping
    // - If Character Sprite is looking left, set isFacingLeft = true; in Start()
    // - If Character Sprite is looking right, set isFacingLeft = false;  in Start()
    public bool isFacingLeft;

    // Use this for initialization
    void Start () {

        // Reference Rigidbody through script
        rb = GetComponent<Rigidbody2D>();

        // Checks if Component exists
        if (!rb)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Check if variable is set to something not 0
        if (speed <= 0)
        {
            // Set a default value to variable if not set in Inspector
            speed = 7.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Speed not set on " + name + ". Defaulting to " + speed);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Check if Enemy is facing left
        if (isFacingLeft)
            // Move Enemy Left
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
            // Move Enemy Right
            rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // Check for collisions with other GameObjects
    // - One or both GameObjects must have a Rigidbody2D attached
    // - Both need colliders attached
    void OnCollisionEnter2D(Collision2D c)
    {
        // Check if 'Enemy' hits something else
        if (c.gameObject.tag == "Enemy")
            // Flip 'Enemy'
            flip();
    }

    // Check for collisions with other GameObjects
    // - One or both GameObjects must have a Rigidbody2D attached
    // - Both need colliders attached
    // - Must have 'Is Trigger' checked on the Collider2D
    void OnTriggerEnter2D(Collider2D c)
    {
        // Check if 'Enemy' hit a barrier
        if (c.gameObject.tag == "Enemy_Barrier")
            // Flip Enemy
            flip();
    }

    // Function used to flip direction GameObject (Character) is facing
    void flip()
    {
        // Toggle isFacingLeft variable
        isFacingLeft = !isFacingLeft;

        // Make a copy of old scale value
        Vector3 scaleFactor = transform.localScale;

        // Flip scale of 'x' variable
        scaleFactor.x = -scaleFactor.x;

        // Update scale to new flipped value
        transform.localScale = scaleFactor;
    }
}
