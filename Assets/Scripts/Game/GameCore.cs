using Game.Models;
using UnityEngine;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public const float TickFrequency = 1f;

        private float _timer;

        private void Awake() {
            Nation soviets = new Nation("Soviets");
            Nation naizs = new Nation("Nazis");

            WarSim sim = new WarSim(soviets, naizs);

            for (int i = 0; i < 10; i++)
                sim.SimulateBattle();
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= TickFrequency)
            {
                _timer -= TickFrequency;
                Tick();
            }
        }

        private void Tick()
        {

        }
    }
}
