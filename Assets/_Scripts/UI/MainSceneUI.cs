using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainSceneUI : MonoBehaviour
    {
        [Header("Button")] public Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(NetworkManager.ConnectToRoom);
        }
    }
}