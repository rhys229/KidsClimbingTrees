using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class KidSpawnerDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("hello world!");
        if (FindObjectOfType<PhotonView>() == null)
        {
            Debug.Log("There aren't any players, debug mode ACTIVATE");
            GameObject kid = (GameObject) Instantiate(Resources.Load("Kid1"));
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().target = kid.GetComponent<Controller2D>();
            RopeScript r = GameObject.Find("Grid").transform.Find("Ropes").GetComponent<RopeScript>();
            r.player = kid;
            r.playerScript = kid.GetComponent<Player>();
            kid.GetComponent<Player>().forceMovement = true;
        }
    }
}
