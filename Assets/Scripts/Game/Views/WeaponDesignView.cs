using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class WeaponDesignView : MonoBehaviour
    {
        public Transform StatsParent;
        public Text Header;
        public Button ConfirmButton;

        [Space(10)]
        public GameObject StatsTemplate;

        private Company _company;
        private List<StatElement> _curStats;

        public void Bind(Company company)
        {
            _company = company;
            ConfirmButton.onClick.AddListener(() =>
            {
                if(_curStats == null)
                    return;
                foreach (var stat in _curStats)
                {
                    stat.Stat.Value = stat.Value;
                }
            });
        }

        public void Show(Weapon newProject)
        {
            //Begin reflection wizardry
            var type = newProject.GetType();
            var stats = new List<Tuple<Stat, string>>();
            foreach (var property in type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(typeof(StatAttribute), true))
                {
                    stats.Add(new Tuple<Stat, string>((Stat)property.GetValue(newProject, null), ((StatAttribute)attribute).Name));
                }
            }

            foreach (Transform child in StatsParent)
            {
                if (child.gameObject != StatsTemplate)
                {
                    Destroy(child.gameObject);
                }
            }
            _curStats = new List<StatElement>();
            foreach (var stat in stats)
            {
                var statView = Instantiate(StatsTemplate, StatsParent, false).transform;
                statView.Find("Text").GetComponent<Text>().text = stat.Item2;
                var slider = statView.GetComponentInChildren<Slider>();
                slider.value = 0f;
                slider.maxValue = _company.Tech[stat.Item1.Type];
                var valueText = statView.Find("Value").GetComponent<Text>();
                var statEle = new StatElement
                {
                    Stat = stat.Item1,
                    Value = 0
                };
                slider.onValueChanged.AddListener(fl =>
                {
                    var val = Mathf.RoundToInt(fl);
                    valueText.text = val.ToString();
                    statEle.Value = val;
                });
                _curStats.Add(statEle);
                statView.gameObject.SetActive(true);
            }
        }

        private struct StatElement
        {
            public Stat Stat { get; set; }
            public int Value { get; set; }
        }
    }
}
