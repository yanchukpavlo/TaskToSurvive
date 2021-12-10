using Models;
using UnityEngine;

namespace Views
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] WeaponModel weaponModel;
        [SerializeField] Transform nozzle;

        float timeToFire;
        public bool CanFire(float time) => time >= timeToFire;

        public void Fire(Vector3 destination)
        {
            if (CanFire(Time.time))
            {
                timeToFire = Time.time + weaponModel.FireCooldown;
                ShootProjectile(destination);
            }
        }

        void ShootProjectile(Vector3 destination)
        {
            var nozzlePosition = nozzle.position;
            var go = Instantiate(weaponModel.ProjectilePrefab, nozzlePosition, Quaternion.identity);
            var rigid = go.GetComponent<Rigidbody>();
            rigid.velocity = (destination - nozzlePosition).normalized * weaponModel.ProjectileSpeed;
        }
    }
}