using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must be added if using UI functions
using UnityEngine.UI;  

public class CanvasManager : MonoBehaviour {

    // References to Buttons in Scene
    public Button buttonStart;
    public Button buttonQuit;

    // Method 1: Used to keep reference to GameManager
    GameManager gm;     // Not needed if using GameManager.instance

    // Use this for initialization
    void Start () {
        
        // Method 1: Finds and keeps reference to GameManager
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Method 1: Check if GameManager was found
        if (gm)
        {
            // Check if Button was connected
            if (buttonStart)
            {
                // Method 1: Uses reference to GameManager
                buttonStart.onClick.AddListener(gm.StartGame);

                // Method 2: No Find. Uses GameManagers.instance
                //buttonStart.onClick.AddListener(GameManager.instance.StartGame);
            }

            // Check if Button was connected
            if (buttonQuit)
            {
                // Method 1: Uses reference to GameManager
                buttonQuit.onClick.AddListener(gm.QuitGame);

                // Method 2: No Find. Uses GameManagers.instance
                //buttonQuit.onClick.AddListener(GameManager.instance.QuitGame);
            }
        }

	}
	
}
