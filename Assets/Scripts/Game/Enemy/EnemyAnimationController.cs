using UnityEngine;

namespace Game.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayMovementAnimation()
        {
            _animator.SetTrigger("Move");
        }
    }
}