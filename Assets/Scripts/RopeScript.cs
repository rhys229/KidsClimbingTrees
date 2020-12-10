using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public GameObject player;

    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        //begin Kyle Code
        Player[] kids = FindObjectsOfType<Player>();
        foreach (Player kid in kids)
        {
            if (kid.photonView.IsMine)
            {
                player = kid.gameObject;
                playerScript = player.GetComponent<Player>();
                return;
            }
        }
        Debug.LogErrorFormat("RopeScript: Wuh oh, no local player instance to reference. This is bad :(");
        //end Kyle Code
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("on rope");
            playerScript.onRope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("off rope");
            playerScript.onRope = false;
        }
    }
}
