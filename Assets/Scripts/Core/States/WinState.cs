using Core.UI;
using Game;
using UnityEngine;

namespace Core.States
{
    public class WinState : BaseState
    {
        public override StateType State => StateType.Win;
        
        private readonly IWinPanelController _winPanelController;
        private readonly IStateMachine _stateMachine;
        private readonly IGameController _gameController;

        public WinState(IWinPanelController winPanelController, IStateMachine stateMachine, IGameController gameController)
        {
            _winPanelController = winPanelController;
            _stateMachine = stateMachine;
            _gameController = gameController;
        }

        public override void Enter()
        {
            Debug.Log($"[{GetType().Name}][Enter] OK");
            _winPanelController.InitContinueButtonClickCallback(ContinueButtonClickCallback);
            _winPanelController.Show();
        }

        public override void Exit()
        {
            _gameController.DestroyPlayer();
            _winPanelController.Hide();
            Debug.Log($"[{GetType().Name}][Exit] OK");
        }

        private void ContinueButtonClickCallback()
        {
            _stateMachine.SwitchToState(StateType.Lobby);
        }
    }
}