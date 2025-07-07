using System;

namespace Core.UI
{
    public interface IWinPanelController : IBaseController<IWinPanelView>
    {
        void InitContinueButtonClickCallback(Action click);
    }
    public class WinPanelController : BaseController<WinPanelView>, IWinPanelController
    {
        public IWinPanelView View { get; }
        
        private Action _continueButtonClickCallback;
        
        public WinPanelController(IUIManager uiManager) : base(uiManager)
        {
            View = UIManager.GetPanel<IWinPanelView>(PanelType.Win);
            
            View.InitContinueButtonClickCallback(ContinueButtonClick);
        }

        public override void Show()
        {
            View.Show();
            View.StartAnimation();
        }

        public override void Hide()
        {
            View.StopAnimation();
            View.Hide();
        }

        public void InitContinueButtonClickCallback(Action click)
        {
            _continueButtonClickCallback = click;
        }

        private void ContinueButtonClick()
        {
            _continueButtonClickCallback?.Invoke();
        }
    }
}