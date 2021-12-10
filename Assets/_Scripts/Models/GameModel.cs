using System;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Models/GameModel")]
    public class GameModel : ScriptableObject
    {
        public static event Action<GameModel> OnGameFinished;

        [SerializeField] float timeUp = 5f;

        float timeUpTimer;

        public void Init()
        {
            timeUpTimer = timeUp;
        }

        public void Evaluate(float deltaTime)
        {
            EvaluateTimeUp(deltaTime);
        }

        void EvaluateTimeUp(float deltaTime)
        {
            timeUpTimer -= deltaTime;

            if (IsTimeOver())
            {
                OnGameFinished?.Invoke(this);
            }
        }

        bool IsTimeOver() => timeUpTimer <= 0;
    }
}