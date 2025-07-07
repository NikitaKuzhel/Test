using UnityEngine;

namespace Game.Enemy
{
    public enum EnemyState
    {
        Idle,
        Move,
        Hit,
        Die,
    }
    
    public class EnemyStateBehaviour : MonoBehaviour
    {
        public EnemyState CurrentState { get; private set; }
        
        public void SwitchToState(EnemyState state)
        {
            CurrentState =  state;
        }
    }
}