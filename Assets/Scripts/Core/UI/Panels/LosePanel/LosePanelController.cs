using System;
namespace Core.UI
{
    public interface ILosePanelController :  IBaseController<ILosePanelView>
    {
        void InitContinueButtonClickCallback(Action click);
    }

    public class LosePanelController : BaseController<LosePanelView>, ILosePanelController
    {
        public ILosePanelView View { get; }
        
        private Action _continueButtonClickCallback;
        
        public LosePanelController(IUIManager uiManager) : base(uiManager)
        {
            View = UIManager.GetPanel<ILosePanelView>(PanelType.Lose);
            
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