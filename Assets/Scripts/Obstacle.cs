using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {
    
    // Holds starting health of 'Obstacle'
    public int health;

    // Used to change size of HealthBar size
    public RectTransform healthBar;
    float healthScale;
    
	// Use this for initialization
	void Start () {

        // Used to change size of HealthBar size
        if (health<=0)
        {
            // Assign a default value of 5 to 'health'
            health = 5;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Health not set on " + name + ". Defaulting to " + health);
        }

        // Check if variable is set to something
        if (!healthBar)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("HealthBar not found on " + name);
        }

        // Resize 'healthBar' based off 'health' value
        healthScale = healthBar.sizeDelta.x / health;
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        // Check if 'Obstacle' was hit by a 'Player_Projectile'
        if (c.gameObject.tag == "Player_Projectile")
        {
            // Remove one health point
            health--;

            //health -= c.gameObject.GetComponent<Projectile>().GetDamage();

            // Resize 'healthBar' based off 'health' value
            healthBar.sizeDelta = new Vector2(health * healthScale,
                healthBar.sizeDelta.y);
            
            // Check if 'Obstacle' is dead
            if (health <= 0)
            {
                // Play Sound
                // Create Partle Effect
                // Trigger a Respawn
                // Play an Animation
                // Etc...

                // 'Enemy' is dead, delete from Scene
                Destroy(gameObject);
            }
        }
    }
}
