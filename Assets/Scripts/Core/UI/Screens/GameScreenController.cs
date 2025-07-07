using System;
using Core.States;
using Game.Player;

namespace Core.UI.Screens
{
    public interface IGameScreenController : IBaseController<IGameScreenView>
    {
        InputBehaviour InputBehaviour { get; }
        
        void InitStartButtonClickCallback(Action click);
        void SetLobbyState();
        void SetGameState();
    }
    
    public class GameScreenController : BaseController<GameScreenView>, IGameScreenController
    {
        public IGameScreenView View { get; }
        public InputBehaviour InputBehaviour { get; private set; }
        private Action _startButtonClickCallback;
        
        public GameScreenController(IUIManager uiManager) : base(uiManager)
        {
            View = UIManager.GetScreen<IGameScreenView>(ScreenType.Game);
            
            View.InitStartButtonClickCallback(StartButtonClick);
            InputBehaviour = View.InputBehaviour;
        }

        public override void Show()
        {
            View.Show();
        }

        public override void Hide()
        {
            View.Hide();
        }

        public void InitStartButtonClickCallback(Action click)
        {
            _startButtonClickCallback =  click;
        }

        public void SetLobbyState()
        {
            View.SetLobbyState();
            InputBehaviour.StopInputCoroutine();
        }

        public void SetGameState()
        {
            View.SetGameState();
            InputBehaviour.LaunchInputCoroutine();
        }

        private void StartButtonClick()
        {
            _startButtonClickCallback?.Invoke();
        }
    }
}