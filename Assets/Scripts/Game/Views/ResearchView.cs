using Game.Models;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Game.Views {
    public class ResearchView : ViewBase {
        public GameObject NewResearchPanel;

        public List<ResearchActivity> CurrentResearchActivities;

        private Company _company;

        public void Bind(Company p_Company) {
            CurrentResearchActivities = new List<ResearchActivity>();

            _company = p_Company;

            SetUiText();
        }

        private void SetUiText() {

        }
    }
}
