using System.Collections.Generic;
using Game.Models;
using Game.Views;
using UnityEngine;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public const float TickFrequency = 1f;
        public float CombatWidth { get {return 10f + Mathf.Log10(_tickCount+10); } }

        //Progress -100 means Nation0 lost, Progress 100 means Nation1 lost
        public float WarProgress { get { return _nation0.Manpower / (_nation0.Manpower + _nation1.Manpower)*200f-100f; } }

        private int _tickCount;
        private Nation _nation0;
        private Nation _nation1;
        private WarSim _sim;
        private Company _playerCompany = new Company();
        private float _timer;

        public GameView GameView;

        private void Awake() {
            _nation0 = new Nation("Soviets");
            _nation1 = new Nation("Nazis");
            
            _sim = new WarSim(_nation0, _nation1);

            GameView.Bind(_nation0, _nation1, new Company("Bokcuklar Inc."));
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
            _sim.SimulateBattle(CombatWidth);
            GameView.UpdateWarState(WarProgress);

            Debug.Log(WarProgress);
        }
    }
}
