using Core;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerSpawner
    {
        PlayerView SpawnPlayer(Transform parent);
    }

    public class PlayerSpawner : IPlayerSpawner
    {
        private readonly ResourceProvider _resourceProvider;

        public PlayerSpawner(ResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
        }

        public PlayerView SpawnPlayer(Transform parent)
        {
            var playerPrefab =
                _resourceProvider.GetPrefab("Player"); // in a real project the key would be part of some descriptor
            
            var playerObject = Object.Instantiate(playerPrefab, parent);
            
            if (playerObject.TryGetComponent(out PlayerView playerView))
            {
                return playerView;
            }

            throw new System.Exception("Could not find Player");
        }
    }
}