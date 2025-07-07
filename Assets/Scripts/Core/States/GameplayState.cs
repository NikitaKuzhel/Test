using Core.UI.Screens;
using Game;
using Game.Enemy;
using UnityEngine;

namespace Core.States
{
    public class GameplayState : BaseState
    {
        public override StateType State => StateType.Gameplay;
        
        private readonly IGameScreenController _gameScreenController;
        private readonly IGameController _gameController;
        private readonly IStateMachine _stateMachine;
        private readonly ICommonEnemyController _commonEnemyController;
        
        public GameplayState(IGameScreenController gameScreenController, IGameController gameController,
            IStateMachine stateMachine, ICommonEnemyController commonEnemyController)
        {
            _gameScreenController = gameScreenController;
            _gameController = gameController;
            _stateMachine = stateMachine;
            _commonEnemyController = commonEnemyController;
        }

        public override void Enter()
        {
            Debug.Log($"[{GetType().Name}][Enter] OK");
            _gameController.InitWinCallback(OnWinCallback);
            _gameController.InitLoseCallback(OnLoseCallback);
            _gameController.Subscribe();
            _gameController.StartGame();
            _gameScreenController.SetGameState();
        }

        public override void Exit()
        {
            _commonEnemyController.DestroyEnemies();
            _gameController.Unsubscribe();
            Debug.Log($"[{GetType().Name}][Exit] OK");
        }

        private void OnWinCallback()
        {
            _stateMachine.SwitchToState(StateType.Win);
        }

        private void OnLoseCallback()
        {
            _stateMachine.SwitchToState(StateType.Lose);
        }
    }
}