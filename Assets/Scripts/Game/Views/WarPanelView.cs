using System.Collections;
using System;
using Game.Models;
using Game.Views;
using UnityEngine;
using UnityEngine.UI;

public class WarPanelView : ViewBase {

    private Company _company;
    private Nation[] _nations;

    private GameObject[] _nationPanels;

    public void Bind(Company p_Company, Nation p_Nation0, Nation p_Nation1) {
        _company = p_Company;

        _nations = new Nation[2] {p_Nation0, p_Nation1};
        _nationPanels = new GameObject[2];

        // For each nation panel
        for (int i = 0; i < 2; i++) {
            // First child is the WarPanelMask, it's child is NationsPanel, then comes our panels for each nation.
            _nationPanels[i] = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
        }

    }

    public void Tick() {
        UpdateUi();
    }

    public void UpdateUi() {
        for (int i = 0; i < 2; i++) {
            _nationPanels[i].transform.Find("NationName").GetComponent<Text>().text = _nations[i].Name;

            // Weapon stats and traits part.
            float[] multipliers = { 15.853f, 0.25f, 0.15f };
            for (int j = 0; j < 3; j++) {
                string statsString = "";
                string traitsString = "-";

                foreach (Stat stat in _nations[i].Weapons[j][_nations[i].Weapons[j].GetStatTypes()]) {
                    statsString += stat.Value + " ";
                }

                foreach (Trait trait in _nations[i].Weapons[j].WeaponTraits) {
                    traitsString += trait.Name + ", ";
                }

                // Weapon Stats
                _nationPanels[i].transform.Find("NationWeaponsPanel").Find("WeaponsStats").GetChild(j)
                    .GetComponent<Text>().text = Enum.GetName(typeof(WeaponType), _nations[i].Weapons[j].Type) + " : " + statsString;

                // Weapon Traits
                // There's a mask gameObject in between them so GetChild(0) is needed as an extra compared to the line above
                _nationPanels[i].transform.Find("NationWeaponsPanel").Find("WeaponsTraits").GetChild(0).GetChild(j)
                        .GetComponent<Text>().text = "Traits : " + traitsString;

                // Casulties part
                _nationPanels[i].transform.Find("NationCasualtyPanel").Find("CasualtyNumbers").GetChild(j).GetComponent<Text>().text = Mathf.RoundToInt((_nations[i].Casualities * multipliers[j] * 2)).ToString();
            }
        }
    }
}
