using System;
using Game.Models;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using UniRx;

namespace Game.Views {
    public class ResearchItemView : ViewBase {

        private Company _company;
        private bool _canResearch;
        private WeaponType _weaponType;
        private ResearchActivity _researchActivity;
        private IDisposable _subscription;

        private GameObject _researchLevel, _researchText, _researchCostText,_researchName, _researchText2;

        private ResearchType _researchType;

        public void Bind(Company p_Company, WeaponType p_WeaponType, ResearchType p_ResearchType) {
            _researchLevel = transform.Find("ResearchLevel").gameObject;
            _researchText = transform.Find("ResearchText").Find("Text").gameObject;
            _researchCostText = transform.Find("ResearchText").Find("Cost").gameObject;
            _researchName = transform.Find("ResearchName").GetChild(0).gameObject;
            _researchText2 = transform.Find("ResearchText2").GetChild(0).gameObject;

            _company = p_Company;
            _canResearch = true;
            _weaponType = p_WeaponType;
            _researchType = p_ResearchType;

            RefreshUi();

            if (_researchType == ResearchType.Stat) {
                GetComponent<Button>().onClick.AddListener(
                    () => {
                        Weapon tempWeapon = _company.Tech.Weapons[(int)_weaponType].Copy();
                        Research tempResearch = new Research(tempWeapon);

                        if (_canResearch && _company.Money.Value >= tempResearch.GetCost()){
                            _company.Money.Value -= tempResearch.GetCost();

                            foreach (Stat t in tempWeapon.Stats) {
                                if (t.Value != 0) {
                                    tempWeapon.AddStat(t.Type, 1);
                                }
                            }

                            _researchActivity = new ResearchActivity(tempResearch);
                            _company.AddResearch(_researchActivity);
                            _subscription = MessageManager.Receive<ResearchCompleteEvent>().Subscribe(ResearchComplete);
                            _canResearch = false;
                            RefreshUi();
                        }
                    });
            }
            else {
                GetComponent<Button>().onClick.AddListener(
                    () => {
                        Weapon tempWeapon = _company.Tech.Weapons[(int)_weaponType].Copy();

                        foreach (Stat t in tempWeapon.Stats) {
                            if (t.Value != 0) {
                                tempWeapon.Stats[(int)t.Type].Value = _researchType == ResearchType.Design
                                    ? _company.Tech.DesignLevel - 1
                                    : _company.Tech.TechLevel - 1;
                            }
                        }

                        Research tempResearch = new Research(tempWeapon);

                        if (_canResearch && _company.Money.Value >= tempResearch.GetCost()) {
                            _company.Money.Value -= tempResearch.GetCost();

                            foreach (Stat t in tempWeapon.Stats) {
                                if (t.Value != 0) {
                                    tempWeapon.Stats[(int)t.Type].Value = _researchType == ResearchType.Design
                                        ? _company.Tech.DesignLevel
                                        : _company.Tech.TechLevel;
                                }
                            }

                            _researchActivity = new ResearchActivity(new Research(tempWeapon, _researchType));
                            _company.AddResearch(_researchActivity);
                            _subscription = MessageManager.Receive<ResearchCompleteEvent>().Subscribe(ResearchComplete);
                            _canResearch = false;

                        }
                    });
            }


            /*int lastStatIndex = 0;
            for (int i = 0; i < _statValuesPanel.transform.childCount; i++) {

                StatType p_type = StatType.Attack;

                int tempLastIndex = lastStatIndex;

                // This Loop iterates through Stats array of company
                // lastIndex holds the last used Stat index for the listener
                for (int j = tempLastIndex; j < _company.Tech.Weapons[(int)_weaponType].Stats.Length; j++) {
                    if (_company.Tech.Weapons[(int)_weaponType].Stats[j].Value != 0) {
                        lastStatIndex = j;
                        p_type = _company.Tech.Weapons[(int)_weaponType].Stats[j].Type;
                        break;
                    }
                }

                _statValuesPanel.transform.GetChild(i).Find("Increment").GetComponent<Button>().onClick.AddListener(
                    () => {
                        if (_canResearch) {
                            Weapon tempWeapon = _company.Tech.Weapons[(int)_weaponType].Copy();
                            tempWeapon.AddStat(p_type, 1);
                            _researchActivity = new ResearchActivity(new Research(tempWeapon, p_type));
                            _company.AddResearch(_researchActivity);
                            _subscription = MessageManager.Receive<ResearchCompleteEvent>().Subscribe(ResearchComplete);
                            _canResearch = false;
                        }
                        else {
                            Debug.Log("Az bekle mk");
                        }
                    });

                lastStatIndex++;
            }*/
        }

        private void ResearchComplete(ResearchCompleteEvent p_ResearchCompleteEvent) {
            if (p_ResearchCompleteEvent.ResearchActivity.Research.Weapon.Type != _weaponType && p_ResearchCompleteEvent.ResearchActivity.Research.ResearchType != _researchType)
                return;

            if (_researchType == ResearchType.Design) {
                _company.Tech.DesignLevel++;
            } else if(_researchType == ResearchType.Tech){
                _company.Tech.TechLevel++;
            }
            else {
                _company.Tech.Weapons[(int)p_ResearchCompleteEvent.ResearchActivity.Research.Weapon.Type] =
                    p_ResearchCompleteEvent.ResearchActivity.Research.Weapon.Copy();
                _company.Tech.UnlockTrait(_weaponType, p_ResearchCompleteEvent.ResearchActivity.Research.Weapon.Stats[0].Value);

            }

            _canResearch = true;

            _subscription.Dispose();

            RefreshUi();
        }

        private void RefreshUi() {
            /*
            if (Conditions.CanResearch(_company, _company.Tech.Weapons[(int)_weaponType].Stats[0].Value))
                GetComponent<Button>().interactable = true;
            else
                GetComponent<Button>().interactable = false;
            */
            Research tempResearch = new Research(_company.Tech.Weapons[(int)_weaponType].Copy());

            if (_researchType == ResearchType.Stat) {
                //transform.Find("WeaponName").GetComponent<Text>().text = Enum.GetName(typeof(WeaponType), _company.Tech.Weapons[(int)_weaponType].Type);

                _researchLevel.GetComponent<Text>().text = _company.Tech.Weapons[(int)_weaponType].Stats[0].Value.ToString();
                _researchText.GetComponent<Text>().text = Enum.GetName(typeof(WeaponType), _weaponType); 
                _researchCostText.GetComponent<Text>().text = "Cost: " + tempResearch.GetCost();
                _researchName.GetComponent<Text>().text = _company.Tech.ReturnTechName(_researchType,_weaponType, _company.Tech.Weapons[(int)_weaponType].Stats[0].Value + 1);
                //_researchText2.GetComponent<Text>().text = _company.Tech.ReturnTechDesc(_researchType, _weaponType, _company.Tech.Weapons[(int)_weaponType].Stats[0].Value + 1);
                // Set Stat texts of UI
                /*int lastStatIndex = 0;
                for (int i = 0; i < 3; i++) {
                    for (int k = lastStatIndex; k < _company.Tech.Weapons[(int)_weaponType].Stats.Length; k++) {
                        if (_company.Tech.Weapons[(int)_weaponType].Stats[k].Value != 0) {
                            _statValuesPanel.transform.GetChild(i).GetComponent<Text>().text = _company.Tech.Weapons[(int)_weaponType].Stats[k].Value.ToString();
                            _statTextsPanel.transform.GetChild(i).GetComponent<Text>().text = Enum.GetName(typeof(StatType), _company.Tech.Weapons[(int)_weaponType].Stats[k].Type);
                            lastStatIndex = k + 1;
                            break;
                        }
                    }
                }*/
            }
            else {
                //transform.Find("WeaponName").GetComponent<Text>().text = Enum.GetName(typeof(ResearchType), _researchType);

                _researchLevel.GetComponent<Text>().text = _researchType == ResearchType.Design ? _company.Tech.DesignLevel.ToString() : _company.Tech.TechLevel.ToString();
                _researchText.GetComponent<Text>().text = Enum.GetName(typeof(ResearchType), _researchType);
                _researchCostText.GetComponent<Text>().text = "Cost: " + (_researchType == ResearchType.Design ? _company.Tech.DesignLevel * 250f : _company.Tech.TechLevel * 250f);
                _researchName.GetComponent<Text>().text = _company.Tech.ReturnTechName(_researchType, WeaponType.Infantry, _company.Tech.TechLevel);
                //_researchText2.GetComponent<Text>().text = _company.Tech.ReturnTechDesc(_researchType, WeaponType.Infantry, _company.Tech.TechLevel);

            }
        }
    }
}
