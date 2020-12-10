using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private GameObject player;

    private Player playerScript;

    private bool activated;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Kid");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !activated)
        {
            activated = true;
            playerScript.setCheckpoint();
        }
    }

    
}
