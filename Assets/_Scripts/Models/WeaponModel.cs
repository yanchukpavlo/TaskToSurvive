using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Models/WeaponModel")]
    public class WeaponModel : ScriptableObject
    {
        [SerializeField] float fireCooldown = 1f;
        [SerializeField] float projectileSpeed = 10;
        [SerializeField] GameObject projectilePrefab;

        public float FireCooldown => fireCooldown;
        public float ProjectileSpeed => projectileSpeed;
        public GameObject ProjectilePrefab => projectilePrefab;
    }
}