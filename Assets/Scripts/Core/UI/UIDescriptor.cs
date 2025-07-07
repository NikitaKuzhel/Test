using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IUIElementPathGetter
    {
        string GetScreen(ScreenType screenType);
        string GetPanel(PanelType panelType);
    }

    [CreateAssetMenu(fileName = "UIDescriptor", menuName ="Create/UI descriptor")]
    public class UIDescriptor : ScriptableObject, IUIElementPathGetter
    {
        [SerializeField] private List<PanelResourceDescriptor> _panelResourceDescriptors = new();
        [SerializeField] private List<ScreenResourceDescriptor> _screenResourceDescriptors = new();
        
        public string GetPanel(PanelType panelType)
        {
            var result = _panelResourceDescriptors.Find(descriptor => descriptor.PanelType == panelType);
            if (result == null)
            {
                Debug.LogError($"[{GetType().Name}][GetPanel] Cannot find panel type: {panelType}");
                return null;
            }
            return result.Path;
        }

        public string GetScreen(ScreenType screenType)
        {
            var result = _screenResourceDescriptors.Find(descriptor => descriptor.ScreenType == screenType);
            if (result == null)
            {
                Debug.LogError($"[{GetType().Name}][GetScreen] Cannot find screen type: {screenType}");
                return null;
            }

            return result.Path;
        }
    }
    
    [System.Serializable]
    public class ViewResourceDescriptor
    {
        [field: SerializeField] public ViewType Type { get; protected set; }
        [field: SerializeField] public string Path { get; protected set; }
    }


    [System.Serializable]
    public class ScreenResourceDescriptor : ViewResourceDescriptor
    {
        [field: SerializeField] public ScreenType ScreenType { get; private set; }
    }


    [System.Serializable]
    public class PanelResourceDescriptor : ViewResourceDescriptor
    {
        [field: SerializeField] public PanelType PanelType { get; private set; }
    }
}
