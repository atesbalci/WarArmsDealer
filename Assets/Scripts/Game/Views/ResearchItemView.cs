using System;
using System.Collections.Generic;
using Game.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views {
    public class ResearchItemView : ViewBase {

        private Weapon _weapon;

        // TODO :: A VERY big big todo, Buttons should be given necessary listeners to create ResearchActivities to passed along to Company
        // TODO :: Also you should track if same research is being done or not. GUD LUCK !!
        public void Bind(Weapon p_Weapon) {
            GameObject statValuesPanel = transform.Find("StatValuesPanel").gameObject;
            GameObject statTextsPanel = transform.Find("StatTextsPanel").gameObject;

            _weapon = p_Weapon;

            transform.Find("WeaponName").GetComponent<Text>().text = Enum.GetName(typeof(WeaponType), p_Weapon.Type);

            int lastStatIndex = 0;
            for (int i = 0; i < 3; i++) {
                for (int k = lastStatIndex; k < p_Weapon.Stats.Length; k++) {
                    if (p_Weapon.Stats[k].Value != 0) {
                        statValuesPanel.transform.GetChild(i).GetComponent<Text>().text = p_Weapon.Stats[k].Value.ToString();
                        statTextsPanel.transform.GetChild(i).GetComponent<Text>().text = Enum.GetName(typeof(StatType), p_Weapon.Stats[k].Type);
                        lastStatIndex = k + 1;
                        break;
                    }
                }
            }
        }
    }
}
