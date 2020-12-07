using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;


namespace Photon.Pun.KyleDemo
{
    #pragma warning disable 649
    [RequireComponent(typeof(TextMeshPro))]
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        #region private field;

        private TextMeshPro PlayerCountText;
        private GameManager _gm;

        #endregion

        #region public fields

        public int numRooms = -1;
        public Button startButton;

        public

            #endregion

            // Start is called before the first frame update
            void Start()
        {
            PlayerCountText = GetComponent<TextMeshPro>();
            if (startButton == null)
            {
                // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

                Debug.LogError(
                    "<Color=Red><b>Missing</b></Color> startButton Reference. Please set it up in GameObject 'Lobby Manager'",
                    this);
            }
            else
            {
                //check if you are the owner
            }

            _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerCountText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
            //Debug.Log("There are " + PhotonNetwork.CountOfRooms + " rooms available");
        }

        public void StartGame()
        {
            _gm.LoadArena("Level1-1");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            numRooms = roomList.Count;
        }
    }
}
