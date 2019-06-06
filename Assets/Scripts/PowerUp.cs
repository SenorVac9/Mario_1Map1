using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float powerUpTimer;
    public float jumpStrength;

    Character cc;

    // Use this for initialization
    void Start () {
		if(powerUpTimer <= 0)
        {
            // Set a default value to variable if not set in Inspector
            powerUpTimer = 2.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("JumpForce not set on " + name + ". Defaulting to " + powerUpTimer);
        }

        if (jumpStrength <= 0)
        {
            // Set a default value to variable if not set in Inspector
            jumpStrength = 20.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("JumpForce not set on " + name + ". Defaulting to " + jumpStrength);
        }

        if(!GetComponent<BoxCollider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>();

            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            cc = c.GetComponent<Character>();
            if (cc)
            {
                cc.jumpForce += jumpStrength;

                StartCoroutine("stopPowerUp", cc);
            }

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator stopPowerUp(Character c)
    {
        yield return new WaitForSeconds(powerUpTimer);

        c.jumpForce -= jumpStrength;

        Destroy(gameObject);
    }
}
