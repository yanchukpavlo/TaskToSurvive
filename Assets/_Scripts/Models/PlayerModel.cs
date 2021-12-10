using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Models/PlayerModel")]
    public class PlayerModel : ScriptableObject
    {
        [SerializeField] int healthPoints = 10;
    }
}