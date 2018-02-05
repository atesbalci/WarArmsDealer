using System;
using Game.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class ActivityView : ViewBase
    {
        public Image Logo;
        public Text Name;
        public Image Progress;

        public Activity Activity { get; set; }

        private IDisposable _death;

        public void Bind(Activity activity)
        {
            Activity = activity;
            Progress.fillAmount = 0f;
            Name.text = Activity.ToString();
            var col = Color.yellow;
            col.a = Progress.color.a;
            Progress.color = col;
            _death = null;
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            var progress = 1f - (float)Activity.RemainingDuration / Activity.TotalDuration;
            Progress.fillAmount = progress;
        }

        public void Kill()
        {
            Refresh();
            _death = Observable.Timer(TimeSpan.FromSeconds(2f)).Subscribe(l => Destroy(gameObject));
            var col = Mathf.Approximately(Progress.fillAmount, 1f) ? Color.green : new Color(1f, 0.5f, 0f);
            col.a = Progress.color.a;
            Progress.color = col;
        }
    }
}