using System;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerHealthController
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        void SetStartHealth();
        void TakeDamage(int damage);
        event Action<int> OnHealthChanged;
        event Action OnDeath;
    }

    public class PlayerHealthController : IPlayerHealthController
    {
        public int MaxHealth { get; private set; } = 100; // in normal project I will have some player descriptor
        public int CurrentHealth { get; private set; }
        public event Action<int> OnHealthChanged;
        public event Action OnDeath;

        public void SetStartHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            OnHealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }
        }

    }
}