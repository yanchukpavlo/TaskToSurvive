using System;
using Models;
using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] GameModel gameModel;

        void Start()
        {
            gameModel.Init();
        }

        void Update()
        {
            gameModel.Evaluate(Time.deltaTime);
        }
    }
}