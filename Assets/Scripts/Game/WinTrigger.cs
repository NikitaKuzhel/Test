using System;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class WinTrigger : MonoBehaviour
    {
        public event Action OnWin;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                OnWin?.Invoke();
            }
        }
    }
}