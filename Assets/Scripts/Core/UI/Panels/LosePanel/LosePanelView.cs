using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public interface ILosePanelView : IView
    {
        void InitContinueButtonClickCallback(Action click);
        void StartAnimation();
        void StopAnimation();
    }

    public class LosePanelView : BasePanelView, ILosePanelView
    {
        public override ViewType ViewType => ViewType.Panel;
        public override PanelType PanelType =>  PanelType.Lose;
        
        [Header("Core")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private TextMeshProUGUI _continueButtonText;
        [Header("Animation")]
        [SerializeField] private float _scaleMultiplier = 1.2f;
        [SerializeField] private float _duration = 0.5f;

        private Tween _scaleTween;
        private Vector3 _originalTextScale;
        
        private Action _click;

        public void InitContinueButtonClickCallback(Action click)
        {
            _click =  click;
        }

        public void StartAnimation()
        {
            _continueButton.gameObject.SetActive(true);
            StartButtonTextAnimation();
        }

        public void StopAnimation()
        {
            _continueButton.gameObject.SetActive(false);
            StopButtonTextAnimation();
        }
        
        private void Awake()
        {
            _continueButton.onClick.AddListener(OnStartButtonClick);
            _originalTextScale = _continueButtonText.transform.localScale;
        }

        private void OnStartButtonClick()
        {
            _click?.Invoke();
        }

        private void StartButtonTextAnimation()
        {
            _scaleTween?.Kill();

            _scaleTween = _continueButtonText.transform
                .DOScale(_originalTextScale * _scaleMultiplier, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopButtonTextAnimation()
        {
            _scaleTween?.SetLoops(0);
            _scaleTween?.Kill();
            _continueButtonText.transform.localScale = _originalTextScale;
        }
    }
}