using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to control 'Character' GameObject
// - Must be attached to Character in Hierarchy
// - Handles 'Character' mechanics
// - Filename must match class name below
public class Character : MonoBehaviour
{

    // Method 1: Keeps a reference Rigidbody2D through script
    // - Not shown in Inspector
    Rigidbody2D rb;

    // Method 2: Keeps a reference Rigidbody2D through script
    // - Shown in Inspector
    public Rigidbody2D rb2;

    // Handles movement speed of Character
    // - Can be adjusted through Inspector while in Play mode
    // - Used to debug movements and test the 'speed' of the Character
    public float speed;

    // Handles jump speed of Character

    // How high Character jumps
    public float jumpForce;

    // Is 'Character' on ground
    // - Are they able to jump
    // - Must be grounded to jump (aka no double jump)
    public bool isGrounded;

    // What is the Ground? 
    // - Player can only jump on GameObjects that are on "Ground" layer  
    public LayerMask isGroundLayer;

    // Tells script where to check if 'Characer' is on ground
    public Transform groundCheck;

    // Size of overlapping circle being checked against ground Colliders
    public float groundCheckRadius;

    // Handles animation states for Character
    // - Idle, Run, Attack...etc.
    Animator anim;

    // Variables to handle projectile firing
    // - Where Projectile will spawn in Scene
    public Transform projectileSpawnPoint;
    // - What projectile prefab to spawn in Scene
    public Projectile projectilePrefab;
    // - How fast Projectile will move in Scene
    public float projectileSpeed;

    // Handles Character flipping
    // - If Character Sprite is looking left, set isFacingLeft = true; in Start()
    // - If Character Sprite is looking right, set isFacingLeft = false;  in Start()
    public bool isFacingLeft;

    // Keeps track of lives
    int _lives;

    // Create a reference to AudioSource to be used when playing sounds for 'Character'
    public AudioSource aSource;

    // Used to store Audio files to played
    public AudioClip jumpSnd;
    public AudioClip shootSnd;
    public AudioClip smb_mariodie;
    

    // Use this for initialization
    void Start()
    {

        // Method 1: Save a reference of Component in script
        // - Component must be added in Inspector
        rb = GetComponent<Rigidbody2D>();

        // Check if Component exists
        if (!rb) // or if(rb == null)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Rigidbody2D not found on " + name);
        }

        // Method 1: Save a reference of Component in script
        // - Component must be added in Inspector
        // - Component should be dragged into variable in Script through Inspector
        if (!rb2)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Rigidbody2D not found on " + name);
        }

        // Check if variable is set to something not 0
        if (speed <= 0)
        {
            // Set a default value to variable if not set in Inspector
            speed = 5.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Speed not set on " + name + ". Defaulting to " + speed);
        }

        // Check if variable is set to something not 0
        if (jumpForce <= 0)
        {
            // Set a default value to variable if not set in Inspector
            jumpForce = 5.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("JumpForce not set on " + name + ". Defaulting to " + jumpForce);
        }

        // Check if variable is set to something
        if (!groundCheck)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("GroundCheck not found on " + name);
        }

        // Check if variable is set to something not 0
        if (groundCheckRadius <= 0)
        {
            // Set a default value to variable if not set in Inspector
            groundCheckRadius = 0.2f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("GroundCheckRadius not set on " + name + ". Defaulting to " + groundCheckRadius);
        }

        // Save a reference of Component in script
        // - Component must be added in Inspector
        anim = GetComponent<Animator>();

        // Check if Component exists
        if (!anim)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Animator not found on " + name);
        }

        // Check if variable is set to something
        if (!projectileSpawnPoint)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("ProjectileSpawnPoint not found on " + name);
        }

        // Check if variable is set to something
        if (!projectilePrefab)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("ProjectilePrefab not found on " + name);
        }

        // Check if variable is set to something not 0
        if (projectileSpeed <= 0)
        {
            // Set a default value to variable if not set in Inspector
            projectileSpeed = 7.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("ProjectileSpeed not set on " + name + ". Defaulting to " + projectileSpeed);
        }

        // Assign 3 lives to 'Character' on instantiation
        lives = 3;

        // Check if variable is set to something
        if (!aSource)
        {
            // Add an 'AudioSource' because it is not added
            aSource = gameObject.AddComponent<AudioSource>();

            // Change variables on the 'AudioSource'
            aSource.loop = false;
            aSource.playOnAwake = false;

        }
    }

    // Update is called once per frame
    void Update()
    {

        // Checks if Left (or a) or Right (or d) is pressed
        // - "Horizontal" must exist in Input Manager (Edit-->Project Settings-->Input)
        // - Returns -1(left), 1(right), 0(nothing)
        // - Use GetAxis for value -1-->0-->1 and all decimal places. (Gradual change in values)
        float moveValue = Input.GetAxisRaw("Horizontal");

        // Check if 'groundCheck' GameObject is touching a Collider on Ground Layer
        // - Can change 'groundCheckRadius' to a smaller value for better precision or if 'Character' is smaller or bigger
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, isGroundLayer);

        // Check if "Jump" button was pressed
        // - "Jump" must exist in Input Manager (Edit-->Project Settings-->Input)
        // - Configuration can be changed later
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Jump");

            // Plays 'jumpSnd' AudioClip
            PlaySound(jumpSnd, 1.0f);

            // Plays 'jumpSnd' AudioClip through the 'SoundManager'
            //SoundManager.instance.PlaySingleSound(jumpSnd, 1.0f);

            // Zeros out force before applying a new force
            // - If force is not zeroed out, the force of gravity will have an effect on the jump
            // - Not setting velocity to 0
            //   - Gravity is -9.8 and force up would be 5 causing a force of -4.8 to be applied
            // - Setting velocity to 0
            //   - Gravity is reset to and force up would be 5 causing a force of 5.0 to be applied
            rb.velocity = Vector2.zero;

            // Unit Vector shortcuts that can be used
            // - Vector2.up --> new Vector2(0,1);
            // - Vector2.down --> new Vector2(0,-1);
            // - Vector2.right --> new Vector2(1,0);
            // - Vector2.left --> new Vector2(-1,0);

            // Applies a force in the UP direction
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Check if Left Control was pressed
        if (Input.GetButtonDown("Fire1"))
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("Pew pew");

            // Call function to make pew pew
            fire();
        }
        // Move Character using Rigidbody2D
        // - Uses moveValue from GetAxis to move left or right
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        // Tells Animator to transition to another Clip
        // - Parameter must be created in Animator window under Parameter tab
        anim.SetFloat("speed", Mathf.Abs(moveValue));

        /*if (moveValue > 0 && isFacingLeft)
            flip();
        else if (moveValue < 0 && !isFacingLeft)
            flip();
        */

        // Check if Character should flip and look left and right
        if ((moveValue > 0 && isFacingLeft) || (moveValue < 0 && !isFacingLeft))
            // Call function to flip Character
            flip();

    }

    // Function used to create and fire a Projectile
    void fire()
    {
        // Creates Projectile and add its to the Scene
        // - projectPrefab is the thing to create
        // - projectileSpawnPoint is where and what rotation to use when created
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation);

        //temp.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        //temp.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectileSpeed;
        //temp.GetComponent<Rigidbody2D>().velocity = projectileSpawnPoint.right * projectileSpeed;

        // Apply movement speed to Projectile that is spawned
        // - Lets the projectile handle its own movement
        temp.speed = projectileSpeed;

        // Plays 'shootSnd' AudioClip
        PlaySound(shootSnd, 1.0f);

        // Plays 'shootSnd' AudioClip through the 'SoundManager'
        //SoundManager.instance.PlaySingleSound(shootSnd, 1.0f);
    }

    // Check for Collision with other GameObjects
    // - One or both GameObjects must have a Rigidbody2D attached
    // - Both need colliders attached
    // - One of the GameObjects should be marked as "Is Trigger"
    void OnTriggerEnter2D(Collider2D c)
    {
        // Prints a message to Console (Shortcut: Control+Shift+C)
        // - Prints what is being collided with Character.CS
        Debug.Log(c.gameObject.tag);

        // Check if Character collides with something tagged as "Collectible"
        if (c.gameObject.tag == "Collectible")
        {
            // Destroy GameObject colliding with Character
            Destroy(c.gameObject);
        }
    }
    bool invincible;
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Enemy_Projectile")
        {
            Destroy(c.gameObject);
            if (!invincible)
            {
                _lives -= 1;
                if (lives <= 0)
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("GameOver");
                }


            }
        }
    }
    // Function used to flip direction GameObject (Character) is facing
    void flip()
    {
        // Method 1: Toggle isFacingRight variable
        isFacingLeft = !isFacingLeft;

        // Method 2: Toggle isFacingRight variable
        /*if (isFacingLeft)
            isFacingLeft = false;
        else
            isFacingLeft = true;
        */

        // Make a copy of old scale value
        Vector3 scaleFactor = transform.localScale;

        // Flip scale of 'x' variable
        scaleFactor.x = -scaleFactor.x;

        // Update scale to new flipped value
        transform.localScale = scaleFactor;
    }

    // Called when a SFX needs to be played
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        // Assign 'AudioClip' when function is called
        aSource.clip = clip;

        // Assign 'volume' to 'AudioSource' when function is called
        aSource.volume = volume;

        // Play assigned 'clip' through 'AudioSource'
        aSource.Play();
    }

    public int lives
    {
        //get;
        //set;

        get { return _lives; }
        set
        {
            _lives = value;
            Debug.Log("Lives changed to " + _lives);
        }
    }
}
