// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsteroidsGameManager.cs" company="Exit Games GmbH">
//   Part of: Asteroid demo
// </copyright>
// <summary>
//  Game Manager for the Asteroid Demo
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEditor.Animations;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using UnityEngine.SceneManagement;

namespace Photon.Pun.Demo.Asteroids
{
    public class KidsGameManager : MonoBehaviourPunCallbacks
    {
        public static KidsGameManager Instance = null;

        public Text InfoText;

        [SerializeField]
        private int currentLevelIndex = 0;
        private string[] levels =
        {
            "KidsWaitingRoom",
            "level1-1",
            "level2-1",
            "level3-1",
            "level4-1",
            "level5-1",
        };

        public string[] kidPrefabs;
        
        #region UNITY

        public void Awake()
        {
            Instance = this;
            //this should stick around for future levels
            DontDestroyOnLoad(this);
        }

        public override void OnEnable()
        {
            base.OnEnable();

            CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
        }

        public void Start()
        {
            Hashtable props = new Hashtable
            {
                {AsteroidsGame.PLAYER_LOADED_LEVEL, true}
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
        }

        #endregion

        #region COROUTINES

        #endregion

        #region PUN CALLBACKS

        public override void OnDisconnected(DisconnectCause cause)
        {
            if (!Application.isEditor)
                UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            //do you really think I have enough time to avoid this problem? bah
            /*if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
            {
                StartCoroutine(SpawnAsteroid());
            }*/
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.ContainsKey(AsteroidsGame.PLAYER_LOADED_LEVEL))
            {
                if (CheckAllPlayerLoadedLevel())
                {
                    StartGame();
                }
                else
                {
                    // not all players loaded yet. wait:
                    Debug.Log("setting text waiting for players! ",this.InfoText);
                    InfoText.text = "Waiting for other players...";
                }
            }
        
        }

        #endregion

        
        // called by OnCountdownTimerIsExpired() when the timer ended
        private void StartGame()
        {
            Debug.Log("StartGame!");

            // on rejoin, we have to figure out if the spaceship exists or not
            // if this is a rejoin (the ship is already network instantiated and will be setup via event) we don't need to call PN.Instantiate

            //spawn players in a row next to each other
            Vector3 position = new Vector3((PhotonNetwork.LocalPlayer.GetPlayerNumber()-PhotonNetwork.CurrentRoom.PlayerCount/2)*2,0,0);
            string playerPrefabName = kidPrefabs[PhotonNetwork.LocalPlayer.GetPlayerNumber()];
            PhotonNetwork.Instantiate(playerPrefabName, position, Quaternion.identity, 0).GetComponent<Animator>();
        }
        //Set finished attribute for all players to false
        //position all players at the start
        //load new level
        public void LoadNextLevel()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable() {{"finished", false}};
                p.SetCustomProperties(props);
            }
            //uses a random player's photonview? Idk might not be the best idea
            GameObject.FindObjectOfType<PhotonView>().RPC("MoveToStartPosition",RpcTarget.All);
            currentLevelIndex++;
            PhotonNetwork.LoadLevel(levels[currentLevelIndex]);
            //wow this is a big ol line of code
            //PhotonNetwork.LoadLevel(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
        }

        private bool CheckAllPlayerLoadedLevel()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                object playerLoadedLevel;

                if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
                {
                    if ((bool) playerLoadedLevel)
                    {
                        continue;
                    }
                }

                return false;
            }

            return true;
        }

        private void OnCountdownTimerIsExpired()
        {
            Debug.Log("starting the game");
            StartGame();
        }
    }
}