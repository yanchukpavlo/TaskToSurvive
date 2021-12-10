using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class WeaponController
    {
        App app;
        public WeaponController(App app)
        {
            this.app = app;
            PlayerView.OnPlayerFired += PlayerViewOnOnPlayerFired;
            
        }
        
        private void PlayerViewOnOnPlayerFired(PlayerView playerView, Vector3 destination)
        {
            app.Views.WeaponView.Fire(destination);
        }
    }
}