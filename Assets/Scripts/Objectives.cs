using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour {
    // Used to keep track of number of Collectibles
    public int numberOfCollectibles;

    // Used to keep track of Collectibles left
    public GameObject[] allCollectibles;
    
    // Used to keep a reference to Player GameObject
    public GameObject player;

    // Used to keep a reference to finishLine GameObject
    public GameObject finishLine;
   
    // Use this for initialization
    void Start () {
        
        // Looks through entire Scene for GameObjects tagged as "Collectible"
        // - Returns everything active that is tagged as "Collectible"
        // - Typically found in order they were added to Scene
        // - Should be used sparingly and only in Start() or Awake()
        allCollectibles = GameObject.FindGameObjectsWithTag("Collectible");
        
        // Stores number of GameObjects
        numberOfCollectibles = allCollectibles.Length;

        // Checks if any collectibles were found
        if(numberOfCollectibles <= 0)
        {
            Debug.Log("Add Collectibles to Scene or tage Collectibles as Collectibles");
        }

        // Looks through Scene for a GameObject named "Mario" in Hierarchy
        // - GameObject must be active
        player = GameObject.Find("Character_Mario");

        // Looks through Scene for a GameObject tagged "Player"
        // - GameObject must be active
        player = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindWithTag("Player");

        // Looks through Scene for a GameObject named "Objective" in Hierarchy
        // - GameObject must be active 
        finishLine = GameObject.Find("FinishLine");
    }
	
	// Update is called once per frame
	void Update () {

        // Only run the check if 'player' and 'finishLine' was found
		if(player && finishLine)
        {
            // Check the distance between 'player' and 'finishLine'
            float distanceToFinish = Vector2.Distance(player.transform.position,
                finishLine.transform.position);

            /*distanceToFinish = (player.transform.position -
                finishLine.transform.position).magnitude;
                */

           // Debug.Log(distanceToFinish);
            
        }
    }
}
