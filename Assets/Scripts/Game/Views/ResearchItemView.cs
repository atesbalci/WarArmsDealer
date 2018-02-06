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

        private GameObject _statValuesPanel, _statTextsPanel;

        public void Bind(Company p_Company, WeaponType p_WeaponType) {
            _statValuesPanel = transform.Find("StatValuesPanel").gameObject;
            _statTextsPanel = transform.Find("StatTextsPanel").gameObject;

            _company = p_Company;
            _canResearch = true;
            _weaponType = p_WeaponType;

            RefreshUi();

            int lastStatIndex = 0;
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

                // BUG :: There's a bug here ! Somewehere. When any kind of research finishes, every other on going research finishes in logic, but still shown as Activity.
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
            }
        }

        private void ResearchComplete(ResearchCompleteEvent p_ResearchCompleteEvent) {
            if (p_ResearchCompleteEvent.ResearchActivity.Research.Weapon.Type != _weaponType)
                return;

            _canResearch = true;

            _company.Tech.Weapons[(int)p_ResearchCompleteEvent.ResearchActivity.Research.Weapon.Type].AddStat(p_ResearchCompleteEvent.ResearchActivity.Research.StatType, 1);

            _subscription.Dispose();

            RefreshUi();
        }

        private void RefreshUi() {

            transform.Find("WeaponName").GetComponent<Text>().text = Enum.GetName(typeof(WeaponType), _company.Tech.Weapons[(int)_weaponType].Type);

            // Set Stat texts of UI
            int lastStatIndex = 0;
            for (int i = 0; i < 3; i++) {
                for (int k = lastStatIndex; k < _company.Tech.Weapons[(int)_weaponType].Stats.Length; k++) {
                    if (_company.Tech.Weapons[(int)_weaponType].Stats[k].Value != 0) {
                        _statValuesPanel.transform.GetChild(i).GetComponent<Text>().text = _company.Tech.Weapons[(int)_weaponType].Stats[k].Value.ToString();
                        _statTextsPanel.transform.GetChild(i).GetComponent<Text>().text = Enum.GetName(typeof(StatType), _company.Tech.Weapons[(int)_weaponType].Stats[k].Type);
                        lastStatIndex = k + 1;
                        break;
                    }
                }
            }
        }
    }
}
