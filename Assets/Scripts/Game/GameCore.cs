using Game.Models;
using UnityEngine;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public const float TickFrequency = 1f;
        public float CombatWidth { get {return 10f + Mathf.Log10(_tickCount+10); } }

        int _tickCount;


        private float _timer;

        private void Awake() {
            Nation soviets = new Nation("Soviets");
            Nation naizs = new Nation("Nazis");

            WarSim sim = new WarSim(soviets, naizs);



            for (int i = 0; i < 10; i++)
                sim.SimulateBattle(CombatWidth);
        
        }
        
        

        private void Update()
        {

            _timer += Time.deltaTime;
            if (_timer >= TickFrequency)
            {
                _timer -= TickFrequency;

                Tick();
                _tickCount++;
            }
        }

        private void Tick()
        {

        }

        
    }
}
