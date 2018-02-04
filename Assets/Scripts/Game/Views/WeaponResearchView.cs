using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;

namespace Game.Views
{
    public class WeaponResearchView : MonoBehaviour
    {
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
        }
    }
}
