using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;

namespace Photon.Pun.KyleDemo
{
#pragma warning disable 649
    public class KidNetworkingManager : MonoBehaviour
    {

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        public int playerID = -1;
        
        private PhotonView photonView;
        private TextMeshPro nameText;
        
        // Start is called before the first frame update
        void Awake()
        {
            photonView = GetComponent<PhotonView>();
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                Debug.Log("I am me!");
                LocalPlayerInstance = this.gameObject;
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            nameText = transform.GetComponentInChildren<TextMeshPro>();
            nameText.text = photonView.Owner.NickName;
            Debug.Log(photonView.Owner);
            playerID = photonView.ViewID;
            Debug.Log("And my playerID is " + playerID + "!");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
