using System;
using Models;
using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        public static event Action<PlayerView, Vector3> OnPlayerFired;

        [SerializeField] PlayerModel playerModel;
        [SerializeField] Camera cam;
        [SerializeField] Animator anim;
        private static readonly int DIE_TRIGGER = Animator.StringToHash("Die");

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var destination = GetFireDestination();
                OnPlayerFired?.Invoke(this, destination);
            }
        }

        Vector3 GetFireDestination()
        {
            var ray = cam.ViewportPointToRay(
                new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y,
                    Input.mousePosition.z));
            return Physics.Raycast(ray, out var hit) ? hit.point : ray.GetPoint(1000);
        }

        public void Die()
        {
            anim.SetTrigger(DIE_TRIGGER);
        }
    }
}