using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;


using System.Collections;
using TMPro;


namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    //[RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants


        // Store the PlayerPref Key to avoid typos
        const string playerNamePrefKey = "PlayerName";


        #endregion


        #region MonoBehaviour CallBacks

        TMP_InputField _inputField;
        
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start ()
        {
            _inputField = GetComponent<TMP_InputField>();
            string defaultName = string.Empty;
            
            if (_inputField!=null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    Debug.Log("found key "+defaultName);
                    _inputField.text = defaultName;
                }
            }


            PhotonNetwork.NickName =  defaultName;
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName()
        {
            string value = _inputField.text;
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;

            Debug.Log("set the stored player name");
            PlayerPrefs.SetString(playerNamePrefKey,value);
        }


        #endregion
    }
}