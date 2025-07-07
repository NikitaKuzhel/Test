using System.Collections;
using System.Collections.Generic;
using Core.States;
using Core.UI;
using Game;
using UnityEngine;

namespace Core.States
{
    public class LoseState : BaseState
    {
        public override StateType State =>  StateType.Lose;
        
        private readonly ILosePanelController _losePanelController;
        private readonly IStateMachine _stateMachine;
        private readonly IGameController _gameController;

        public LoseState(ILosePanelController losePanelController, IStateMachine stateMachine, IGameController gameController)
        {
            _losePanelController = losePanelController;
            _stateMachine = stateMachine;
            _gameController = gameController;
        }


        public override void Enter()
        {
            Debug.Log($"[{GetType().Name}][Enter] OK");
            _losePanelController.InitContinueButtonClickCallback(ContinueButtonClickCallback);
            _losePanelController.Show();
        }

        public override void Exit()
        {
            _gameController.DestroyPlayer();
            _losePanelController.Hide();
            Debug.Log($"[{GetType().Name}][Exit] OK");
        }
        
        private void ContinueButtonClickCallback()
        {
            _stateMachine.SwitchToState(StateType.Lobby);
        }
    }
}