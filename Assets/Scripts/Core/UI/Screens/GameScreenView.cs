using System;
using DG.Tweening;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Screens
{
    public interface IGameScreenView : IView
    {
        InputBehaviour InputBehaviour { get; }
        void InitStartButtonClickCallback(Action click);
        void SetLobbyState();
        void SetGameState();
    }

    public class GameScreenView : BaseScreenView, IGameScreenView
    {
        public override ViewType ViewType => ViewType.Screen;
        public override ScreenType ScreenType =>  ScreenType.Game;
        
        [Header("Core")]
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _startButtonText;
        [field: SerializeField] public InputBehaviour InputBehaviour { get; private set; }
        [Header("Animation")]
        [SerializeField] private float _scaleMultiplier = 1.2f;
        [SerializeField] private float _duration = 0.5f;

        private Tween _scaleTween;
        private Vector3 _originalTextScale;
        
        private Action _click;

        public void InitStartButtonClickCallback(Action click)
        {
            _click =  click;
        }

        public void SetLobbyState()
        {
            _startButton.gameObject.SetActive(true);
            StartButtonTextAnimation();
        }

        public void SetGameState()
        {
            _startButton.gameObject.SetActive(false);
            StopButtonTextAnimation();
        }
        
        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _originalTextScale = _startButtonText.transform.localScale;
        }

        private void OnStartButtonClick()
        {
            _click?.Invoke();
        }

        private void StartButtonTextAnimation()
        {
            _scaleTween?.Kill();

            _scaleTween = _startButtonText.transform
                .DOScale(_originalTextScale * _scaleMultiplier, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopButtonTextAnimation()
        {
            _scaleTween?.SetLoops(0);
            _scaleTween?.Kill();
            _startButtonText.transform.localScale = _originalTextScale;
        }
    }
}