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

            // Add ResearchItemView for 
            foreach (var weapon in _company.Tech.Weapons) {
                GameObject temp = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
                temp.GetComponent<ResearchItemView>().Bind(_company, weapon.Type, ResearchType.Stat);
                temp.SetActive(true);
            }

            GameObject tempResearch = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
            tempResearch.GetComponent<ResearchItemView>().Bind(_company, WeaponType.Infantry, ResearchType.Tech);
            tempResearch.SetActive(true);

            GameObject tempDesign = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
            tempDesign.GetComponent<ResearchItemView>().Bind(_company, WeaponType.Infantry, ResearchType.Design);
            tempDesign.SetActive(true);
        }
    }
}
