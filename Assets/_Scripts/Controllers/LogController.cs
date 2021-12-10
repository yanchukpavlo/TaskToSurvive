using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class LogController
    {
        public LogController()
        {
            PlayerView.OnPlayerFired += PlayerViewOnOnPlayerFired;
            GameModel.OnGameFinished += GameModelOnOnGameFinished;
        }

        private void GameModelOnOnGameFinished(GameModel _)
        {
            Debug.Log("The time is over! You have lost.");
        }

        private void PlayerViewOnOnPlayerFired(PlayerView _, Vector3 destination)
        {
            Debug.Log("Player Fired!");
            Debug.Log($"There may be a problem with the projectile {destination}.");
        }
    }
}