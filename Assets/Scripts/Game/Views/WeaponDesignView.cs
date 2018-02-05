using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class WeaponDesignView : MonoBehaviour
    {
        public Transform StatsParent;
        public Text Header;
        public Button SellToLeftButton, CreateDesignButton;
        public Text DesignCostText;
        [Space(10)]
        public GameObject StatsTemplate;

        private Company _company;
        private Nation _nation0, _nation1;
        private List<StatElement> _curStats;

        public void Bind(Company company, Nation nation0, Nation nation1)
        {
            _company = company;
            _nation0 = nation0;
            _nation1 = nation1;

            CreateDesignButton.onClick.AddListener(() => 
            {
                if (_curStats == null)
                    return;
                foreach (var stat in _curStats) {
                    stat.Stat.Value = stat.Value;
                }

                Weapon newDesign =  new InfantryWeapon(new KeyValuePair<StatType, int>[3] {
                    new KeyValuePair<StatType, int>(StatType.Attack, _curStats[0].Value),
                    new KeyValuePair<StatType, int>(StatType.Health, _curStats[1].Value),
                    new KeyValuePair<StatType, int>(StatType.Support, _curStats[2].Value)
                });

                _company.CompanyDesigns.CreateDesignActivity(newDesign);

                gameObject.SetActive(false);
            });
        }

        public void Show(Weapon newProject)
        {
            //Begin reflection wizardry
            var stats = new List<Tuple<Stat, string>>();

            foreach (var stat in newProject.Stats) {
                stats.Add(new Tuple<Stat, string>(stat, System.Enum.GetName(typeof(StatType), stat.Type)));
            }

            foreach (Transform child in StatsParent)
            {
                if (child.gameObject != StatsTemplate)
                {
                    Destroy(child.gameObject);
                }
            }
            _curStats = new List<StatElement>();
            foreach (var stat in stats)
            {
                if (stat.Item1.Value != 0) {
                    var statView = Instantiate(StatsTemplate, StatsParent, false).transform;
                    statView.Find("Text").GetComponent<Text>().text = stat.Item2;
                    var slider = statView.GetComponentInChildren<Slider>();
                    slider.value = 0f;
                    slider.maxValue = _company.Tech.Weapons[0].Stats[(int)stat.Item1.Type].Value;
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

        void showDesign()
        {

            Weapon newDesign = new InfantryWeapon(new KeyValuePair<StatType, int>[3] {
                            new KeyValuePair<StatType, int>(StatType.Attack, _curStats[0].Value),
                            new KeyValuePair<StatType, int>(StatType.Health, _curStats[1].Value),
                            new KeyValuePair<StatType, int>(StatType.Support, _curStats[2].Value)
                        });
            if(!Conditions.CanDesign(newDesign, _company))
            {
                CreateDesignButton.interactable = true;
            }
            else
            {
                CreateDesignButton.interactable = true;
            }
            DesignCostText.text = "Duration: " + newDesign.GetDuration() +
                        "\nCost: " + newDesign.GetCost();
        }

        private class StatElement
        {
            public Stat Stat { get; set; }
            public int Value { get; set; }
        }
    }
}
