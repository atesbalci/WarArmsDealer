using UnityEngine;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public const float TickFrequency = 1f;

        private float _timer;
        
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
