using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.KyleDemo;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VictoryScreen : MonoBehaviour
{
    public Text victoryText;
    
    // Start is called before the first frame update
    void Start()
    {
        StopPlayers();   
        MurderPlayers();
        Destroy(GameObject.Find("KidsGameManager"));
        int highscore = -1;
        string highName = "Danny Devito";
        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            if (p.GetScore() > highscore)
            {
                highscore = p.GetScore();
                highName = p.NickName;
            }
        }

        victoryText.text = highName;
    }

    void StopPlayers()
    {
        PhotonView[] players = FindObjectsOfType<PhotonView>();
        foreach (PhotonView p in players)
        {
            p.RPC("StopMoving",RpcTarget.All);
        }
    }

    public void MurderPlayers()
    {
        PhotonView[] players = FindObjectsOfType<PhotonView>();
        foreach (PhotonView p in players)
        {
            Destroy(p.gameObject);
        }
    }

    public void DisconnectMe()
    {
        StartCoroutine(LeaveGame());
    }

    IEnumerator LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene("LobbyScene");
    }

}
