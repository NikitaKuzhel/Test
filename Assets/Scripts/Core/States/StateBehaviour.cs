using UnityEngine;
using Zenject;

namespace Core.States
{
    public class StateBehaviour : MonoBehaviour
    {
        [Inject] private LobbyState _lobbyState;
        [Inject] private GameplayState _gameplayState;
        [Inject] private WinState _winState;
        [Inject] private LoseState _loseState;
        
        [Inject] private IStateMachine _stateMachine;
        
        private void Start()
        {
            PrepareStates();
            _stateMachine.SwitchToState(StateType.Lobby);
        }

        private void PrepareStates()
        {
            _stateMachine.InitStates(_lobbyState, _gameplayState, _winState, _loseState);
        }
    }
}