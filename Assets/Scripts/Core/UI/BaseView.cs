using UnityEngine;

namespace Core.UI
{
    public enum ViewType
    {
        Screen,
        Panel,
    }
    
    public enum ScreenType
    {
        Game,
    }

    public enum PanelType
    {
        Win,
        Lose
    }

    public interface IView
    {
        void Show();
        void Hide();
    }
    
    public abstract class BaseView : MonoBehaviour, IView
    {
        public abstract ViewType  ViewType { get; }
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }

    public abstract class BaseScreenView : BaseView
    {
        public abstract ScreenType ScreenType { get; }
    }

    public abstract class BasePanelView : BaseView
    {
        public abstract PanelType PanelType { get; }
    }
}