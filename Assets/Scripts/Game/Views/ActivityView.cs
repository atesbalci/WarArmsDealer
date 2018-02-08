using DG.Tweening;
using Game.Models;
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

        private Graphic[] _graphics;

        public void Bind(Activity activity)
        {
            Activity = activity;
            Progress.fillAmount = 0f;
            Name.text = Activity.ToString();
            var col = Color.yellow;
            col.a = Progress.color.a;
            Progress.color = col;
            _graphics = GetComponentsInChildren<Graphic>(true);

            //Animation
            transform.localScale = Vector3.one * 2f;
            transform.DOScale(Vector3.one, 0.5f);
            foreach (var graphic in _graphics)
            {
                var color = graphic.color;
                var alpha = color.a;
                color.a = 0f;
                graphic.color = color;
                color.a = alpha;
                graphic.DOColor(color, 0.5f);
            }
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
            var col = Mathf.Approximately(Progress.fillAmount, 1f) ? Color.green : new Color(1f, 0.5f, 0f);
            col.a = Progress.color.a;
            Progress.color = col;

            //Animation
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.one * 1.5f, 0.5f));
            sequence.Append(transform.DOScale(Vector3.one, 1f));
            foreach (var graphic in _graphics)
            {
                var clear = graphic.color;
                clear.a = 0f;
                sequence.Join(graphic.DOColor(clear, 1f));
            }
        }
    }
}