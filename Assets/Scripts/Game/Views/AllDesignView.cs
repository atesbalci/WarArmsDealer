using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class AllDesignView : MonoBehaviour
    {
        public Button SellToLeftButton, CreateDesignButton;
        public Text DesignCostText;
        [Space(10)]
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

            _company.CompanyDesigns.CompletedDesigns.ObserveAdd().Subscribe(x => { Show(); });

            var create = Instantiate(DesignViewTemplate, DesignList.transform.Find("GridWithOurElements"));
            create.GetComponentInChildren<Text>().text = "Create New Design";
            create.GetComponent<Button>().onClick.AddListener(() =>
            {
                transform.parent.Find("CreateDesignPanel").gameObject.SetActive(true);
                gameObject.SetActive(false);
            });
            create.SetActive(true);
        }

        public void Show()
        {

            for (int i = shown; i < _company.CompanyDesigns.CompletedDesigns.Count; i++)
            {
                Debug.Log("Creating a design in the list: " + _company.CompanyDesigns.CompletedDesigns.Count);
                var d = Instantiate(DesignViewTemplate, DesignList.transform.Find("GridWithOurElements"));
                d.GetComponentInChildren<Text>().text = _company.CompanyDesigns.CompletedDesigns[i].ToString();
                d.SetActive(true);
                d.transform.SetAsFirstSibling();
                shown++;
            }
            
        }

        private void OnEnable()
        {
            Show();
        }
    }
}
