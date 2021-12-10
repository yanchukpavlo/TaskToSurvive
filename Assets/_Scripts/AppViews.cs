using UnityEngine;
using Views;

public class AppViews : MonoBehaviour
{
    public PlayerView PlayerView => playerView;
    public WeaponView WeaponView => weaponView;

    [SerializeField] PlayerView playerView;
    [SerializeField] GameView gameView;
    [SerializeField] WeaponView weaponView;
}