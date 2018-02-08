using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;

namespace Game.Views
{
    public class ActivitiesView : ViewBase
    {
        public Transform ActivitiesParent;

        [Space(10)]
        public ActivityView ActivityViewPrefab;
        public Sprite ResearchSprite;
        public Sprite DesignSprite;

        private List<ActivityView> _activityViews;

        public void Bind(Company company)
        {
            _activityViews = new List<ActivityView>();
            company.Activities.ObserveAdd().Subscribe(add =>
            {
                var actView = Instantiate(ActivityViewPrefab, ActivitiesParent, false);
                actView.Bind(add.Value);
                _activityViews.Add(actView);
                actView.gameObject.SetActive(true);

                Weapon weapon = null;
                Sprite sprite = null;
                var ract = add.Value as ResearchActivity;
                var dact = add.Value as DesignActivity;
                if (ract != null)
                {
                    
                }
                else if (dact != null)
                {
                    
                }
            });
            company.Activities.ObserveRemove().Subscribe(rem =>
            {
                var ind = _activityViews.FindIndex(x => x.Activity == rem.Value);
                if (ind >= 0)
                {
                    _activityViews[ind].Kill();
                    _activityViews.RemoveAt(ind);
                }
            });
        }
    }
}
