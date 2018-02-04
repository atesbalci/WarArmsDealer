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
        public GameObject StatsTemplate;

        private Company _company;

        public void Bind(Company company)
        {
            _company = company;
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
            foreach (var stat in stats)
            {
                var statView = Instantiate(StatsTemplate, StatsParent, false).transform;
                statView.Find("Text").GetComponent<Text>().text = stat.Item2;
                var slider = statView.GetComponentInChildren<Slider>();
                slider.value = 0f;
                var valueText = statView.Find("Value").GetComponent<Text>();
                //TODO Ates: Continue
            }
        }
    }
}
