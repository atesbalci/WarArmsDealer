using Game.Models;
using UnityEngine.UI;
using UnityEngine;
using UniRx;

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
                WeaponDesignView.gameObject.SetActive(!WeaponDesignView.gameObject.activeInHierarchy);

                WeaponDesignView.CreateDesignView.gameObject.SetActive(!WeaponDesignView.gameObject.activeInHierarchy);
            });

            ReseachViewButton.onClick.AddListener(() => {
                ResearchView.gameObject.SetActive(!ResearchView.gameObject.activeSelf);
            });

            SalesViewButton.onClick.AddListener(() => {
                SalesView.gameObject.SetActive(!SalesView.gameObject.activeSelf);

                for (int i = SalesView.DesignsPanelParent.childCount - 1; i < _company.CompanyDesigns.CompletedDesigns.Count; i++) {
                    GameObject tempDesign = Instantiate(SalesView.WeaponDesignPrefab, SalesView.DesignsPanelParent, false);
                    tempDesign.transform.Find("Text").GetComponent<Text>().text = _company.CompanyDesigns.CompletedDesigns[i].ToString();
                    tempDesign.SetActive(true);

                    int i1 = i;
                    tempDesign.transform.Find("SellLeft").GetComponent<Button>().onClick.AddListener(() => {
                        _nation1.BuyWeapon(_company.CompanyDesigns.CompletedDesigns[i1],
                            () => { _company.Money.Value += _company.CompanyDesigns.CompletedDesigns[i1].GetCost() * 2; });
                    });

                    tempDesign.transform.Find("SellRight").GetComponent<Button>().onClick.AddListener(() => {
                        _nation0.BuyWeapon(_company.CompanyDesigns.CompletedDesigns[i1],
                            () => { _company.Money.Value += _company.CompanyDesigns.CompletedDesigns[i1].GetCost() * 2; });
                    });
                }
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
