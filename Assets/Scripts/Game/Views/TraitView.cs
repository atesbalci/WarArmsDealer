using System.Collections.Generic;
using Game.Models;
using UniRx;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class TraitView : MonoBehaviour
    {

        public GameObject DesignViewTemplate;
        public GameObject DesignList;
        private Company _company;
        private Nation _nation0, _nation1;
        int shown = 0;

        public ReactiveCollection<Trait> CurrentDesignTraits;

        public void Bind(Company company, Nation nation0, Nation nation1)
        {
            _company = company;
            _nation0 = nation0;
            _nation1 = nation1;
            CurrentDesignTraits = new ReactiveCollection<Trait>();
        }

        public void Show(WeaponType type)
        {

            
            for (int j = 0; j < DesignList.transform.Find("GridWithOurElements").childCount; j++ )
            {
                if(DesignList.transform.Find("GridWithOurElements").GetChild(j).gameObject != DesignViewTemplate)
                Destroy(DesignList.transform.Find("GridWithOurElements").GetChild(j).gameObject);
            }

            switch (type)
            {
                case WeaponType.Infantry:
                    for (int i = 0; i < _company.Tech.InfantryTraits.Count; i++)
                    {
                        var d = Instantiate(DesignViewTemplate, DesignList.transform.Find("GridWithOurElements"));
                        d.GetComponentInChildren<Text>().text = _company.Tech.InfantryTraits[i].Name;
                        d.transform.GetChild(1).GetComponent<Text>().text = _company.Tech.InfantryTraits[i].Description;
                        if (CurrentDesignTraits.AsSafeEnumerable().ToList().Exists(x => _company.Tech.InfantryTraits[i] == x))
                            d.GetComponent<Image>().color = Color.blue;
                        else
                            d.GetComponent<Image>().color = Color.white;
                        d.SetActive(true);

                        var b = d.GetComponent<Button>();
                        b.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            var trait = _company.Tech.InfantryTraits.Find(x => x.Name == b.GetComponentInChildren<Text>().text);
                            if (!CurrentDesignTraits.AsSafeEnumerable().ToList().Exists(x => trait == x))
                            {
                                CurrentDesignTraits.Add(trait);
                                d.GetComponent<Image>().color = Color.blue;
                            }
                            else
                            {
                                CurrentDesignTraits.Remove(trait);
                                d.GetComponent<Image>().color = Color.white;
                            }
                            
                        });
                        d.transform.SetAsFirstSibling();
                        //shown++;
                    }
                    break;
                case WeaponType.Tank:
                    for (int i = 0; i < _company.Tech.TankTraits.Count; i++)
                    {
                        var d = Instantiate(DesignViewTemplate, DesignList.transform.Find("GridWithOurElements"));
                        d.GetComponentInChildren<Text>().text = _company.Tech.TankTraits[i].Name;
                        d.transform.GetChild(1).GetComponent<Text>().text = _company.Tech.TankTraits[i].Description;
                        if (CurrentDesignTraits.AsSafeEnumerable().ToList().Exists(x => _company.Tech.TankTraits[i] == x))
                            d.GetComponent<Image>().color = Color.blue;
                        else
                            d.GetComponent<Image>().color = Color.white;
                        d.SetActive(true);
                        d.transform.SetAsFirstSibling();
                        var b = d.GetComponent<Button>();
                        b.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            var trait = _company.Tech.TankTraits.Find(x => x.Name == b.GetComponentInChildren<Text>().text);
                            if (!CurrentDesignTraits.AsSafeEnumerable().ToList().Exists(x => trait == x))
                            {
                                CurrentDesignTraits.Add(trait);
                                d.GetComponent<Image>().color = Color.blue;
                            }
                            else
                            {
                                CurrentDesignTraits.Remove(trait);
                                d.GetComponent<Image>().color = Color.white;
                            }
                        });
                        //shown++;
                    }
                    break;
                case WeaponType.Artillery:
                    for (int i = 0; i < _company.Tech.ArtilleryTraits.Count; i++)
                    {
                        var d = Instantiate(DesignViewTemplate, DesignList.transform.Find("GridWithOurElements"));
                        d.GetComponentInChildren<Text>().text = _company.Tech.ArtilleryTraits[i].Name;
                        d.transform.GetChild(1).GetComponent<Text>().text = _company.Tech.ArtilleryTraits[i].Description;
                        if (CurrentDesignTraits.AsSafeEnumerable().ToList().Exists(x => _company.Tech.ArtilleryTraits[i] == x))
                            d.GetComponent<Image>().color = Color.blue;
                        else
                            d.GetComponent<Image>().color = Color.white;
                        d.SetActive(true);
                        d.transform.SetAsFirstSibling();
                        var b = d.GetComponent<Button>();
                        b.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            var trait = _company.Tech.ArtilleryTraits.Find(x => x.Name == b.GetComponentInChildren<Text>().text);
                            if (!CurrentDesignTraits.AsSafeEnumerable().ToList().Exists(x => trait == x))
                            {
                                CurrentDesignTraits.Add(trait);
                                d.GetComponent<Image>().color = Color.blue;
                            }
                            else
                            {
                                CurrentDesignTraits.Remove(trait);
                                d.GetComponent<Image>().color = Color.white;
                            }
                        });
                        //shown++;
                    }
                    break;
                default:
                    break;
            }

            

        }

        private void OnEnable()
        {
           //Show(WeaponType.Infantry);
        }
    }
}
