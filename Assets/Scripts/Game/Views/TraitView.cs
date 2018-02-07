using System.Collections.Generic;
using Game.Models;
using UniRx;
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



        public void Bind(Company company, Nation nation0, Nation nation1)
        {
            _company = company;
            _nation0 = nation0;
            _nation1 = nation1;
        }

        public void Show(WeaponType type)
        {

            for(int j = 0; j < DesignList.transform.Find("GridWithOurElements").childCount; j++ )
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
                        d.SetActive(true);
                        d.transform.SetAsFirstSibling();
                        //shown++;
                    }
                    break;
                case WeaponType.Tank:
                    for (int i = 0; i < _company.Tech.TankTraits.Count; i++)
                    {
                        var d = Instantiate(DesignViewTemplate, DesignList.transform.Find("GridWithOurElements"));
                        d.GetComponentInChildren<Text>().text = _company.Tech.TankTraits[i].Name;
                        d.SetActive(true);
                        d.transform.SetAsFirstSibling();
                        //shown++;
                    }
                    break;
                case WeaponType.Artillery:
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
