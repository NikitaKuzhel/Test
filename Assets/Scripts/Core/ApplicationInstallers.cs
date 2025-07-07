using Core.States;
using Core.UI;
using Core.UI.Screens;
using Game;
using Game.Enemy;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationInstallers : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ResourceProvider _resourceProvider;
        [SerializeField] private MapHelper _mapHelper;
        
        public override void InstallBindings()
        {
            PrepareCore();
            PrepareUI();
            PrepareGameControllers();
        }

        private void PrepareCore()
        {
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
            
            Container.Bind<LobbyState>().ToSelf().AsSingle();
            Container.Bind<GameplayState>().ToSelf().AsSingle();
            Container.Bind<WinState>().ToSelf().AsSingle();
            Container.Bind<LoseState>().ToSelf().AsSingle();

            Container.Bind<ResourceProvider>().FromInstance(_resourceProvider);
            Container.Bind<MapHelper>().FromInstance(_mapHelper);
        }
        
        private void PrepareUI()
        {
            Container.Bind<IUIManager>().FromInstance(_uiManager);
            
            Container.Bind<IGameScreenController>().To<GameScreenController>().AsSingle();
            Container.Bind<IWinPanelController>().To<WinPanelController>().AsSingle();
            Container.Bind<ILosePanelController>().To<LosePanelController>().AsSingle();
        }

        private void PrepareGameControllers()
        {
            Container.Bind<IGameController>().To<GameController>().AsSingle();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().AsSingle();
            Container.Bind<ICommonEnemyController>().To<CommonEnemyController>().AsSingle();
            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();
            
            var playerHealthController = new PlayerHealthController();
            Container.Bind<IPlayerController>().To<PlayerController>().AsSingle().WithArguments(playerHealthController);
        }
    }
}