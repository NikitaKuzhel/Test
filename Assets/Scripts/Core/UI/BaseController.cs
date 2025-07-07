namespace Core.UI
{
    public interface IBaseController<T> where T : IView
    {
        void Show();
        void Hide();
    }
    
    public abstract class BaseController<T> : IBaseController<T> where T : BaseView
    {
        protected T View;
        
        protected readonly IUIManager UIManager;

        protected BaseController(IUIManager uiManager)
        {
            UIManager = uiManager;
        }
        
        public abstract void Show();

        public abstract void Hide();
    }
}