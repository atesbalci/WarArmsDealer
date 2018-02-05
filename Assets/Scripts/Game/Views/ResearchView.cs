using Game.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Game.Views {
    public class ResearchView : ViewBase {

        public GameObject NewResearchPanel;
        public ResearchItemView ResearchItemView;

        private Company _company;

        public void Bind(Company p_Company) {
            _company = p_Company;

            ResearchItemView temp = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
            temp.Bind(new InfantryWeapon());
            temp.gameObject.SetActive(true);

            ResearchItemView temp1 = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
            temp1.Bind(new TankWeapon());
            temp1.gameObject.SetActive(true);

            ResearchItemView temp2 = Instantiate(ResearchItemView, NewResearchPanel.transform, false);
            temp2.Bind(new ArtilleryWeapon());
            temp2.gameObject.SetActive(true);
        }
    }
}
