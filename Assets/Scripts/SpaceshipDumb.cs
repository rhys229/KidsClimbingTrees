// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Spaceship.cs" company="Exit Games GmbH">
//   Part of: Asteroid Demo,
// </copyright>
// <summary>
//  Spaceship
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;

using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

//namespace Photon.Pun.Demo.Asteroids
//{
    public class SpaceshipDumb : MonoBehaviour
    {

        public float h;
        public float v;
        public bool controllable = true;

        private PhotonView photonView;
        private TextMeshPro nameText;

        #region UNITY

        public void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        public void Update()
        {
            if (!photonView.IsMine || !controllable)
            {
                return;
            }

            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            
            transform.position += new Vector3(h,v,0);
        }

        public void FixedUpdate()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!controllable)
            {
                return;
            }
        }

        #endregion

        /*#region COROUTINES

        private IEnumerator WaitForRespawn()
        {
            yield return new WaitForSeconds(AsteroidsGame.PLAYER_RESPAWN_TIME);

            photonView.RPC("RespawnSpaceship", RpcTarget.AllViaServer);
        }

        #endregion*/

        #region PUN CALLBACKS

        #endregion
    }
//}