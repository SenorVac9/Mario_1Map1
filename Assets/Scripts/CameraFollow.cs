using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Thing that the Camera should follow
    Transform target;

    // Empty GameObject in Scene to show where the camera should stop along
    // x access to left of screen and y to bottom of screen
    public Transform cameraBoundMin;

    // Empty GameObject in Scene to show where the camera should stop along
    // x access to right of screen and y to top of screen
    public Transform cameraBoundMax;

    // Using these variables instead of writing the long path
    float xMin, xMax, yMin, yMax;

    // Use this for initialization
    void Start () {

        // Find Target in scene tagged as 'Player'
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        
        // Was the 'Player' not found
        if(!g)
        {
            Debug.Log("Player not found");
            // Stop function from continuing
            return;
        }

        // Was the 'Player' found
        // - Keep a reference to the Transform on the target
        target = g.GetComponent<Transform>();

        // Lowest points stored
        xMin = cameraBoundMin.position.x;
        yMin = cameraBoundMin.position.y;

        // Highest points stored
        xMax = cameraBoundMax.position.x;
        yMax = cameraBoundMax.position.y;
    }
	
	// Update is called once per frame
	void Update () {

        // Only move camera if 'target' exists (was found)
        if (target)
        {
            // Move Camera to player position if the player is within bounds set
            transform.position = new Vector3(
                Mathf.Clamp(target.position.x, xMin, xMax),
                Mathf.Clamp(target.position.y, yMin, yMax),
                transform.position.z);
            // Mathf.Clamp() is used to keep the boundaries
        }
	}
}
