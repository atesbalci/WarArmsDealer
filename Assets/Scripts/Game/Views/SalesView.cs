﻿using Game.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views {
    public class SalesView : ViewBase {

        public GameObject WeaponDesignPrefab;
        public Transform DesignsPanelParent;

        private Company _company;
        private Nation _nation0, _nation1;

        public void Bind(Company p_Company, Nation p_Nation0, Nation p_Nation1) {
            _company = p_Company;
            _nation0 = p_Nation0;
            _nation1 = p_Nation1;

            for (int i = 0; i < _company.CompanyDesigns.CompletedDesigns.Count; i++) {
                GameObject tempDesign = Instantiate(WeaponDesignPrefab, DesignsPanelParent, false);
                tempDesign.transform.Find("Text").GetComponent<Text>().text = _company.DesignedWeapons[i].ToString();
                tempDesign.SetActive(true);

                tempDesign.transform.Find("SellLeft").GetComponent<Button>().onClick.AddListener(() => {
                    _nation0.BuyWeapon(_company.DesignedWeapons[i], null);
                });

                tempDesign.transform.Find("SellRight").GetComponent<Button>().onClick.AddListener(() => 
                {
                    _nation0.BuyWeapon(_company.DesignedWeapons[1], null);
                });
            }
        }

        public void OnEnable() {
            for (int i = DesignsPanelParent.childCount - 1; i < _company.CompanyDesigns.CompletedDesigns.Count; i++) {
                GameObject tempDesign = Instantiate(WeaponDesignPrefab, DesignsPanelParent, false);
                tempDesign.transform.Find("Text").GetComponent<Text>().text = _company.CompanyDesigns.CompletedDesigns[i].ToString();
                tempDesign.SetActive(true);

                int i1 = i;
                tempDesign.transform.Find("SellLeft").GetComponent<Button>().onClick.AddListener(() => {
                    _nation1.BuyWeapon(_company.CompanyDesigns.CompletedDesigns[i1], null);
                });

                tempDesign.transform.Find("SellRight").GetComponent<Button>().onClick.AddListener(() => {
                    _nation0.BuyWeapon(_company.CompanyDesigns.CompletedDesigns[i1], null);
                });
            }
        }
    }
}