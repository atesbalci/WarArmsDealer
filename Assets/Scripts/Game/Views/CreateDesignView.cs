﻿using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class CreateDesignView : MonoBehaviour
    {

        public Transform StatsParent;
        public Text Header;
        public Button SellToLeftButton, CreateDesignButton;
        public Text DesignCostText;
        [Space(10)]
        public GameObject StatsTemplate;
        public GameObject TypeTemplate;
        public GameObject TraitTemplate;
        public Button AddTraitButton;
        private TraitView _traitView;

        private Company _company;
        private Nation _nation0, _nation1;
        private List<StatElement> _curStats;
        private List<Tuple<Stat, string>> stats; 
        private Weapon _newProject;
        private WeaponType _type;
        private GameObject _sliderPanel;

        public void Bind(Company company, Nation nation0, Nation nation1)
        {


            _traitView = GetComponent<TraitView>();
            _company = company;
            _nation0 = nation0;
            _nation1 = nation1;
            _newProject = new Weapon();
            stats = new List<Tuple<Stat, string>>();

            bool traits = false;
            AddTraitButton.onClick.AddListener(() =>
            {
                traits = !traits;

                TraitTemplate.SetActive(!TraitTemplate.activeInHierarchy);
                _sliderPanel.SetActive(!_sliderPanel.activeInHierarchy);
                _traitView.Show(_type);
                //TypeTemplate.SetActive(!TypeTemplate.activeInHierarchy);
                foreach (Toggle t in TypeTemplate.GetComponentsInChildren<Toggle>())
                {
                    t.interactable = !t.interactable;
                }
                if(_traitView.CurrentDesignTraits.Count > 0)
                {
                    foreach (Toggle t in TypeTemplate.GetComponentsInChildren<Toggle>())
                    {
                        t.interactable = false;
                    }
                }

            });
            CreateDesignButton.onClick.AddListener(() => 
            {
                if (_curStats == null)
                    return;
                foreach (var stat in _curStats) {
                    stat.Stat.Value = stat.Value;
                }

                foreach(var trait in _traitView.CurrentDesignTraits)
                {
                    _newProject.WeaponTraits.Add(trait);
                }
                _traitView.CurrentDesignTraits = new List<Trait>();
                _company.CompanyDesigns.CreateDesignActivity(_newProject);

                
                transform.parent.Find("AllDesignPanel").gameObject.SetActive(true);
                gameObject.SetActive(false);
                //transform.SetAsFirstSibling();
                //transform.parent.gameObject.SetActive(false);
            });
            TypeTemplate.SetActive(true);
            foreach (Toggle t in TypeTemplate.GetComponentsInChildren<Toggle>())
            {
                t.onValueChanged.AddListener(toggle =>
                {
                    if (toggle && _traitView.CurrentDesignTraits.Count == 0)
                        switch (t.name)
                        {
                            case "Infantry":
                                Debug.Log(toggle + " Infantry");
                                _type = WeaponType.Infantry;
                                break;
                            case "Tank":
                                _type = WeaponType.Tank;
                                break;
                            case "Artillery":
                                _type = WeaponType.Artillery;
                                break;

                        }


                    if(!TraitTemplate.activeInHierarchy)
                    {
                        stats = new List<Tuple<Stat, string>>();
                        Show();
                    }
                    else
                    {
                        _traitView.Show(_type);
                    }
                    

                });
            }
        }

        public void Show()
        {
            //Begin reflection wizardry
            
            
            

            foreach (Transform child in StatsParent)
            {
                if (child.gameObject != StatsTemplate && child.gameObject != TypeTemplate && child.gameObject != TraitTemplate)
                {
                    Destroy(child.gameObject);
                }
            }

            //var typeSelect = Instantiate(TypeTemplate, StatsParent, false);
            

            _newProject = Weapon.CreateWeapon(_type, 1, 1, 1);

            foreach (var stat in _newProject.Stats)
            {
                stats.Add(new Tuple<Stat, string>(stat, System.Enum.GetName(typeof(StatType), stat.Type)));
            }




            _curStats = new List<StatElement>();


            _sliderPanel = Instantiate(new GameObject("Sliders"), StatsParent);
            _sliderPanel.AddComponent<VerticalLayoutGroup>();
            _sliderPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 300f);
            foreach (var stat in stats)
            {
                if (stat.Item1.Value != 0) {
                    var statView = Instantiate(StatsTemplate, _sliderPanel.transform, false).transform;
                    statView.Find("Text").GetComponent<Text>().text = stat.Item2;
                    var slider = statView.GetComponentInChildren<Slider>();
                    slider.value = 0f;
                    slider.maxValue = _company.Tech.Weapons[(int)_type].Stats[(int)stat.Item1.Type].Value;
                    var valueText = statView.Find("Value").GetComponent<Text>();
                    valueText.text = "0";
                    var statEle = new StatElement {
                        Stat = stat.Item1,
                        Value = 0
                    };
                    slider.onValueChanged.AddListener(fl =>
                    {
                        var val = Mathf.RoundToInt(fl);
                        valueText.text = val.ToString();
                        statEle.Value = val;

                        showDesign();


                    });
                    _curStats.Add(statEle);


                    statView.gameObject.SetActive(true);
                }
            }
            

            StatsParent.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            Show();
        }

        void showDesign()
        {

            Weapon newDesign = new InfantryWeapon(new KeyValuePair<StatType, int>[3] {
                            new KeyValuePair<StatType, int>(StatType.Attack, _curStats[0].Value),
                            new KeyValuePair<StatType, int>(StatType.Health, _curStats[1].Value),
                            new KeyValuePair<StatType, int>(StatType.Support, _curStats[2].Value)
                        });
            if(!Conditions.CanDesign(newDesign, _company))
            {
                CreateDesignButton.interactable = false;
            }
            else
            {
                CreateDesignButton.interactable = true;
            }
            DesignCostText.text = "Duration: " + Mathf.CeilToInt(newDesign.GetDuration()) +
                        "\nCost: " + newDesign.GetCost();
        }

        private class StatElement
        {
            public Stat Stat { get; set; }
            public int Value { get; set; }
        }
    }
}
