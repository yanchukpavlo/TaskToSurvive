using UnityEngine;

namespace Views
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] float autoDestroyAfterSeconds = 4f;

        void Start()
        {
            Destroy(gameObject, autoDestroyAfterSeconds);
        }
    }
}