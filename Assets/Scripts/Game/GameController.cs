using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI.Screens;
using Game.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public interface IGameController
    {
        void StartGame();
        void Subscribe();
        void Unsubscribe();
        void DestroyPlayer();
        void InitWinCallback(Action callback);
        void InitLoseCallback(Action callback);
    }

    public class GameController : IGameController
    {
        private readonly IPlayerController _playerController;
        private readonly MapHelper _mapHelper;
        
        private Action _winCallback;
        private Action _loseCallback;

        public GameController(IPlayerController playerController, MapHelper mapHelper)
        {
            _playerController = playerController;
            _mapHelper = mapHelper;
        }

        public void StartGame()
        {
            _playerController.StartMovement();
        }

        public void Subscribe()
        {
            _playerController.Subscribe();
            _playerController.OnDeath += OnDeathListener;
            _mapHelper.WinTrigger.OnWin += OnWinListener;
        }

        public void Unsubscribe()
        {
            _playerController.UnSubscribe();
            _playerController.OnDeath -= OnDeathListener;
            _mapHelper.WinTrigger.OnWin -= OnWinListener;
        }

        public void DestroyPlayer()
        {
            _playerController.Destroy();
        }

        public void InitWinCallback(Action callback)
        {
            _winCallback =  callback;
        }

        public void InitLoseCallback(Action callback)
        {
            _loseCallback = callback;
        }

        private void OnDeathListener()
        {
            _playerController.StopMovement();
            _loseCallback?.Invoke();
        }

        private void OnWinListener()
        {
            _playerController.StopMovement();
            _winCallback?.Invoke();
        }
    }
}