using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager instance { get; private set; }

        private const int MAX_PLAYER_PER_ROOM = 16;
        
        #region Event
        
        public Action OnPlayerKillOther = null;
        public Action<string> OnGameEnd = null;
        public Action OnEnemyLeftRoom = null;

        #endregion
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(this.gameObject);
            
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            
            ConnectToMaster();
        }
        
        #region Server

        private static void ConnectToMaster() => PhotonNetwork.ConnectUsingSettings();
        public override void OnConnectedToMaster() => SceneManager.LoadScene("MainScene");

        #endregion

        #region Room

        public static void ConnectToRoom() =>
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: new RoomOptions { MaxPlayers = MAX_PLAYER_PER_ROOM });

        public override void OnJoinedRoom() => PhotonNetwork.LoadLevel("GameScene");

        public static void LeaveRoom() => PhotonNetwork.LeaveRoom();
        public override void OnLeftRoom() => SceneManager.LoadScene("MainScene");

        #endregion

        #region Game

        public static GameObject InstantiateObj(string name) => InstantiateObj(name, Vector3.zero);

        public static GameObject InstantiateObj(string name, Vector3 position) =>
            PhotonNetwork.Instantiate(name, position, Quaternion.identity);
        #endregion
    }
}