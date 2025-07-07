using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IUIManager
    {
        T GetScreen<T>(ScreenType screenType);
        T GetPanel<T>(PanelType panelType);
    }
    
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIDescriptor _uiDescriptor;
        [SerializeField] private Transform _screensParent;
        [SerializeField] private Transform _panelsParent;
        
        private static UIManager _instance;

        private Dictionary<ScreenType, GameObject> _screens = new();
        private Dictionary<PanelType, GameObject> _panels = new();
        
        public T GetScreen<T>(ScreenType screenType)
        {
            GameObject go = GetScreen(screenType);
            return go.GetComponent<T>();
        }
        public T GetPanel<T>(PanelType panelType)
        {
            GameObject go = GetPanel(panelType);
            return go.GetComponent<T>();
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private GameObject GetScreen(ScreenType screenType)
        {
            if (_screens.TryGetValue(screenType, out var screen) == true)  return screen;

            var prefab = Resources.Load<GameObject>(_uiDescriptor.GetScreen(screenType));
            var instance = Instantiate(prefab, _screensParent);
            _screens[screenType] = instance;
            return instance;
        }
        private GameObject GetPanel(PanelType panelType)
        {
            if (_panels.TryGetValue(panelType, out var panel) == true)  return panel;

            var prefab = Resources.Load<GameObject>(_uiDescriptor.GetPanel(panelType));
            var instance = Instantiate(prefab, _panelsParent);
            instance.gameObject.SetActive(false);
            _panels[panelType] = instance;
            return instance;
        }
    }
}