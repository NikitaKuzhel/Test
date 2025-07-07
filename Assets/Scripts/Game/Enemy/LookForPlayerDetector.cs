using System;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class LookForPlayerDetector : MonoBehaviour
    {
        public event Action<PlayerView> OnFindPlayer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                OnFindPlayer?.Invoke(playerView);
            }
        }
    }
}