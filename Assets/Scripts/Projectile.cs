using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Used to tell GameObject (Projectile) how fast to move
    public float speed;

    // Used to tell GameObject (Projectile) how long to live without colliding with anything
    public float lifetime;

	// Use this for initialization
	void Start () {

        // Check if variable is set to something not 0
        if (lifetime <= 0)
        {
            // Set a default value to variable if not set in Inspector
            lifetime = 2.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Lifetime not set on " + name + ". Defaulting to " + lifetime);
        }

        // Take Rigidbody2D component and change its velocity to value passed
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        aSource = GetComponent<AudioSource>();
        // Destroy gameObject after 'lifeTime' seconds
        Destroy(gameObject, lifetime);
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
        Debug.Log("asdfsdfgssgdsfg");
        if (c.gameObject.tag == "Enemy_Turret")
        { 
            PlaySound(smb_enemydie  );
            Debug.Log("PLAYSOUND");
        }
        // Destory GameObject Script is attached to
        //Destroy(gameObject);
    }
}
