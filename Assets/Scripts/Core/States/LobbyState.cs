using Core.UI.Screens;
using Game;
using Game.Enemy;
using Game.Player;
using UnityEngine;

namespace Core.States
{
    public class LobbyState : BaseState
    {
        public override StateType State => StateType.Lobby;
        
        private readonly IPlayerController _playerController;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly MapHelper _mapHelper;
        private readonly IGameScreenController _gameScreenController;
        private readonly IStateMachine _stateMachine;
        private readonly ICommonEnemyController _commonEnemyController;

        public LobbyState(IPlayerController playerController, IPlayerSpawner playerSpawner, MapHelper mapHelper,
            IGameScreenController gameScreenController, IStateMachine stateMachine, ICommonEnemyController commonEnemyController)
        {
            _playerController = playerController;
            _playerSpawner = playerSpawner;
            _mapHelper = mapHelper;
            _gameScreenController = gameScreenController;
            _stateMachine = stateMachine;
            _commonEnemyController = commonEnemyController;
        }

        public override void Enter()
        {
            Debug.Log($"[{GetType().Name}][Enter] OK");
            
            _gameScreenController.Show();
            _gameScreenController.InitStartButtonClickCallback(StartCameraTransition);
            _gameScreenController.SetLobbyState();
            
            _mapHelper.PoolInitializer.InitializePools();
            
            var player = _playerSpawner.SpawnPlayer(_mapHelper.PlayerSpawnPoint);
            _playerController.InitView(player);
            _playerController.InitInputBehaviour(_gameScreenController.InputBehaviour);
            _playerController.InitPoolManager(_mapHelper.PoolInitializer.BulletPool);
            _playerController.SetStartHealth();
            _playerController.SetStartPosition(_mapHelper.PlayerSpawnPoint.position);
            _mapHelper.CameraFollow.InitTarget(player.transform);
            _mapHelper.CameraFollow.SetDefaultPosition();
            
            _commonEnemyController.SpawnEnemies();
        }

        public override void Exit()
        {
            Debug.Log($"[{GetType().Name}][Exit] OK");
        }

        private void StartCameraTransition()
        {
            _mapHelper.CameraFollow.StartAnimation(OnAnimationComplete);
        }

        private void OnAnimationComplete()
        {
            _stateMachine.SwitchToState(StateType.Gameplay);
        }
    }
}

