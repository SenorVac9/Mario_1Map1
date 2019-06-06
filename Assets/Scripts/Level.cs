using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must be added if using UI functions
using UnityEngine.UI;

public class Level : MonoBehaviour {

    // Variable type should match function in 'GameManager'
    public int spawnLocation;
    //public Transform spawnLocation;
    //public Vector3 spawnLocation;
    // public GameObject spawnLocation;
 
    void Start () {

        // Check if spawnLocation was set properly
        if (spawnLocation < 0)
            // Set 'spawnLocation' to the first spawn point in the 'Level'
            spawnLocation = 0;

        // Call 'spawnPlayer()' from GameManager
        GameManager.instance.spawnPlayer(spawnLocation);

        // Set reference to Score Text field in GameManager
        GameManager.instance.scoreText =
            GameObject.Find("Score_Text").GetComponent<Text>();
        
        // Method 1: Set 'score' and 'scoreText' to 0 at Level start by calling set function
        GameManager.instance.score = 0;

        // Method 2: Changes Score
        //GameManager.instance.scoreText.text = "Score: " +
        //   GameManager.instance.score;

    }
}
