using System;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class HitPlayerDetector : MonoBehaviour
    {
        public event Action<PlayerView> OnHitPlayer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                OnHitPlayer?.Invoke(playerView);
            }
        }
    }
}