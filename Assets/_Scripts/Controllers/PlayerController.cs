using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class PlayerController
    {
        App app;
        public PlayerController(App app)
        {
            this.app = app;
            GameModel.OnGameFinished += GameModelOnOnGameFinished;
        }

        private void GameModelOnOnGameFinished(GameModel obj)
        {
            app.Views.PlayerView.Die();
        }
    }
}