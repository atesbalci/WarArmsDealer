using Game.Models;
using UnityEngine;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public const float TickFrequency = 1f;
        public float CombatWidth { get {return 10f + Mathf.Log10(_tickCount+10); } }

        //Progress -100 means Nation0 lost, Progress 100 means Nation1 lost
        public float WarProgress { get { return nation0.Manpower / (nation0.Manpower + nation1.Manpower)*200f-100f; } }

        int _tickCount;
        Nation nation0;
        Nation nation1;
        private Company _playerCompany = new Company();
        private float _timer;

        private void Awake() {
            Nation soviets = nation0 = new Nation("Soviets");
            Nation naizs = nation1 = new Nation("Nazis");
            
            WarSim sim = new WarSim(soviets, naizs);


            Debug.Log("Progress:" + WarProgress);
            for (int i = 0; i < 10; i++)
                sim.SimulateBattle(CombatWidth);
            Debug.Log("Progress:" + WarProgress);

            _playerCompany.CompanyDesigns.CreateDesignActivity(new Weapon());
        
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
            _playerCompany.Tick();
        }

        
    }
}
