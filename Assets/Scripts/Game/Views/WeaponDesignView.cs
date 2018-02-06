using System.Collections.Generic;
using Game.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class WeaponDesignView : MonoBehaviour
    {
        public Text Header;
        public Button AllDesignButton;
        [Space(10)]
        //public GameObject StatsTemplate;

        private Company _company;
        private Nation _nation0, _nation1;

        public CreateDesignView CreateDesignView;
        public AllDesignView AllDesignView;
        //private List<StatElement> _curStats;

        private void OnEnable()
        {
            AllDesignView.gameObject.SetActive(true);
        }

        public void Bind(Company company, Nation nation0, Nation nation1)
        {
            _company = company;
            _nation0 = nation0;
            _nation1 = nation1;

            CreateDesignView.Bind(company, nation0, nation1);
            AllDesignView.Bind(company, nation0, nation1);
            AllDesignView.gameObject.SetActive(true);

            /*
            AllDesignButton.onClick.AddListener(() =>
            {
                AllDesignView.gameObject.SetActive(true);
            });
            */

        }

        private void OnDisable() {
            CreateDesignView.gameObject.SetActive(false);
        }
    }
}
