using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : MonoBehaviour {
    
    // Variables to handle projectile firing
    // - Where Projectile will spawn in Scene
    public Transform projectileSpawnPoint;
    // - What projectile prefab to spawn in Scene
    public Projectile projectilePrefab;
    // - How fast Projectile will move in Scene
    public float projectileSpeed;

    // Handles projectile mechanic (rate of fire)
    public float projectileFireRate;
    float timeSinceLastFire = 0.0f;

    // Handles 'Enemy' health
    public int health;

    // Handles animation states for 'Enemy'
    // - Idle, Run, Attack...etc.
    Animator anim;

    // Use this for initialization
    void Start () {

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

        // Check if variable is set to something not 0
        if (projectileFireRate <= 0)
        {
            // Set a default value to variable if not set in Inspector
            projectileFireRate = 2.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("ProjectileFireRate not set on " + name + ". Defaulting to " + projectileFireRate);
        }

        // Check if variable is set to something not 0
        if (health <= 0)
        {
            // Set a default value to variable if not set in Inspector
            health = 1;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Health not set on " + name + ". Defaulting to " + health);
        }

        // Save a reference of Component in script
        // - Component must be added in Inspector
        anim = GetComponent<Animator>();
       
        // Check if variable is set to something
        if (!anim)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Animator not found on " + name);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time > timeSinceLastFire + projectileFireRate)
        {
            // Calls 'fire' function to fire projectile
            //fire();

            // Uses AnimationEvents to call 'fire' function to fire a projectile
            anim.SetTrigger("Attack");

            // Timestamp of last time the projectile was fired
            timeSinceLastFire = Time.time;
        }
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
        // - Lets projectile handle its own movement
        temp.speed = projectileSpeed;

        // Dynamically tags projectile being fired by 'Enemy'
        // - Tag name must exist or it will give errors
        temp.tag = "Enemy_Projectile";

        // Dynamically changes projectile color after being fired by 'Enemy'
        temp.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
    }

    // Check for collisions with other GameObjects
    // - One or both GameObjects must have a Rigidbody2D attached
    // - Both need colliders attached
    public AudioSource aSource;
    public AudioClip smb_enemydie;
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        // Assign 'AudioClip' when function is called
        aSource.clip = clip;

        // Assign 'volume' to 'AudioSource' when function is called
        aSource.volume = volume;

        // Play assigned 'clip' through 'AudioSource'
        aSource.Play();
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        // Check if 'Enemy' was hit by a 'Projectile'
        if (c.gameObject.tag == "Player_Projectile")
        {
            // Remove one health point
            health--;
           
            // Remove health points based off of Script attached to Projectile
            /*
            Projectile p = c.gameObject.GetComponent<Projectile>();
            
            if(p)
                health -= p.GetDamage();
            */

            // Check if 'Enemy' has health left
            if (health <= 0)
            {
                // Kill 'Enemy;
                // - Play Sound
                // - Trigger Respawn
                // - Play an Animation
                // - Etc...
                PlaySound(smb_enemydie, 1.0f);
                // 'Enemy' is dead, remove from Scene
                Destroy(gameObject);
            }
        }
    }
}
