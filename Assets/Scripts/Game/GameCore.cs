using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using Game.Models;
using Game.Views;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public float TickFrequency = 1f;
        public float CombatWidth { get {return Mathf.Log10(_tickCount+10)*10f; } }

        private Coroutine _menuCoroutine;

        //Progress -100 means Nation0 lost, Progress 100 means Nation1 lost
        float WarProgress = 0f;

        private int _tickCount;
        private Nation _nation0;
        private Nation _nation1;
        private WarSim _sim;
        private Company _playerCompany;
        private float _timer;

        public GameView GameView;

        private void Awake() {
            DOTween.Init();
            _playerCompany = new Company("Bokcular Inc.");
            _nation0 = new Nation("Team Floating Points");
            _nation1 = new Nation("Team Integers");
            
            _sim = new WarSim(_nation0, _nation1);

            GameView.Bind(_nation0, _nation1, _playerCompany);
            GameView.UpdateCompanyState(_tickCount);


            _playerCompany.Money.Subscribe(f => { GameView.UpdateCompanyState(_tickCount); });
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
            WarProgress = _sim.SimulateBattle(CombatWidth, WarProgress);
            GameView.UpdateWarState(WarProgress);
            GameView.UpdateCompanyState(_tickCount);
            GameView.WarPanelView.Tick();
            _playerCompany.Tick();
            //Debug.Log(WarProgress);
        }

        public void QuitButton() {
            if (_menuCoroutine == null)
                _menuCoroutine = StartCoroutine(DelayedMenuAction(Application.Quit));
        }

        public void RestartButton() {
            if (_menuCoroutine == null)
                _menuCoroutine = StartCoroutine(DelayedMenuAction(
                    () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
                ));
        }

        private IEnumerator DelayedMenuAction(Action p_Action) {
            yield return new WaitForSeconds(0.8f);

            p_Action.Invoke();
        }
    }
}
