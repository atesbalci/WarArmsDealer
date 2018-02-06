using Game.Models;
using UnityEngine.UI;
using UnityEngine;

namespace Game.Views {
    public class GameView : ViewBase {
        private Nation _nation0, _nation1;
        private Company _company;

        [Header("War State")]
        public Image WarStateLeftImage;
        public Image WarStateRightImage;

        [Header("Buttons")]
        public Button WeaponDesignButton;
        public Button ReseachViewButton;

        [Header("Texts")]
        public Text CompanyInfoText;

        [Header("Views")]
        public WeaponDesignView WeaponDesignView;
        //public CreateDesignView CreateDesignView;
        public ResearchView ResearchView;

        public void Bind(Nation p_Nation0, Nation p_Nation1, Company p_Company) {
            _nation0 = p_Nation0;
            _nation1 = p_Nation1;
            _company = p_Company;


            WeaponDesignView.Bind(_company, _nation0, _nation1);
            ResearchView.Bind(_company);

            WeaponDesignButton.onClick.AddListener(() => {
                if(WeaponDesignView.gameObject.activeInHierarchy)
                    WeaponDesignView.gameObject.SetActive(false);
                else
                    WeaponDesignView.gameObject.SetActive(true);
                //CreateDesignView.Show(new InfantryWeapon());
            });

            ReseachViewButton.onClick.AddListener(() => {
                ResearchView.gameObject.SetActive(!ResearchView.gameObject.activeSelf);
            });
        }

        public void UpdateWarState(float p_WarProgress) {
            float leftOrientedWinPercent = (p_WarProgress + 100) / 200;
            WarStateLeftImage.fillAmount = leftOrientedWinPercent;
            WarStateRightImage.fillAmount = 1 - leftOrientedWinPercent;
        }
        public void UpdateCompanyState(int tickCount)
        {
            CompanyInfoText.text = "Company Name: " + _company.Name +
                "\nMoney: " + _company.Money +
                "\nTime: " + tickCount;
        }
    }
}
