using Game.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Game.Views {
    public class ResearchView : ViewBase {

        public GameObject NewResearchPanel;
        public GameObject ResearchItemView;

        private Company _company;

        public void Bind(Company p_Company) {
            _company = p_Company;

            foreach (var weapon in _company.Tech.Weapons) {
                GameObject temp = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
                temp.GetComponent<ResearchItemView>().Bind(_company, weapon.Type);
                temp.SetActive(true);
            }
        }
    }
}
