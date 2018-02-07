using Game.Models;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Utils.ViewHelpers;

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
        public Button SalesViewButton;

        [Header("Texts")]
        public Text CompanyInfoText;

        [Header("Views")]
        public WeaponDesignView WeaponDesignView;
        //public CreateDesignView CreateDesignView;
        public ResearchView ResearchView;
        public ActivitiesView ActivitiesView;
        public SalesView SalesView;

        public void Bind(Nation p_Nation0, Nation p_Nation1, Company p_Company) {
            _nation0 = p_Nation0;
            _nation1 = p_Nation1;
            _company = p_Company;


            WeaponDesignView.Bind(_company, _nation0, _nation1);
            ResearchView.Bind(_company);
            ActivitiesView.Bind(_company);
            SalesView.Bind(_company, _nation0, _nation1);

            WeaponDesignButton.onClick.AddListener(() => {
                WeaponDesignView.gameObject.GetComponent<PanelGroupElement>().Toggle();
                EventSystem.current.SetSelectedGameObject(null);

                //WeaponDesignView.CreateDesignView.gameObject.SetActive(!WeaponDesignView.gameObject.activeInHierarchy);
            });

            ReseachViewButton.onClick.AddListener(() => {
                ResearchView.GetComponent<PanelGroupElement>().Toggle();
                EventSystem.current.SetSelectedGameObject(null);
            });

            SalesViewButton.onClick.AddListener(() => {
                SalesView.GetComponent<PanelGroupElement>().Toggle();
                SalesView.Show();
                EventSystem.current.SetSelectedGameObject(null);
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
