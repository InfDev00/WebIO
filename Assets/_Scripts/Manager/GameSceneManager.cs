using System;
using _Scripts;
using _Scripts.UI;
using Cinemachine;
using Managers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace Manager
{
    public class GameSceneManager : MonoBehaviour
    {
        private PlayerController _player;
        
        [Header("UI")]
        public ExpSlider expSlider;
        public GameObject gameEndPanel;
        public Button gameEndButton;
        public JoyStick joyStick;

        [Header("Camera")] public CinemachineVirtualCamera virtualCamera;
        
        private void Awake()
        {
            var playerObj = NetworkManager.InstantiateObj("Player");
            _player = playerObj.GetComponent<PlayerController>();
            _player.joyStick = joyStick;

            virtualCamera.Follow = _player.transform;

            if (PhotonNetwork.IsMasterClient)
            {
                NetworkManager.InstantiateObj("Enemy", new Vector3(7, 0, 7));
                NetworkManager.InstantiateObj("Enemy", new Vector3(-7, 0, -7));
            }
        }

        private void Start()
        {
            _player.OnObjUpdate += expSlider.UpdateSlider;
            _player.OnDamaged += GameEnd;

            gameEndButton.onClick.AddListener((() =>
            {
                gameEndButton.interactable = false;
                NetworkManager.LeaveRoom();
            }));
            
            gameEndPanel.SetActive(false);
        }
        
        private void GameEnd() => gameEndPanel.SetActive(true);
    }
}