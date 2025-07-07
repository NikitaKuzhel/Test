using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;

        public void SetMaxHealth(int maxHealth)
        {
            _healthBar.maxValue = maxHealth;
            _healthBar.value = maxHealth;
        }

        public void SetCurrentHealth(int health)
        {
            _healthBar.value = health;
        }
    }
}