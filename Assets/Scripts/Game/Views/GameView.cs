using Game.Models;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Utils.ViewHelpers;
using System.Collections;

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
        public Button WarViewButton;

        [Header("Texts")]
        public Text CompanyInfoText;

        [Header("Views")]
        public WeaponDesignView WeaponDesignView;
        //public CreateDesignView CreateDesignView;
        public ResearchView ResearchView;
        public ActivitiesView ActivitiesView;
        public SalesView SalesView;
        public CameraView CameraView;
        public WarPanelView WarPanelView;

        [Header("PanelGroup")]
        public PanelGroup PanelGroup;

        private bool _redIsNewer;
        private int _greenAnim, _redAnim;
        private float _difference = 0f;
        private string _oldTimeText;

        public void Bind(Nation p_Nation0, Nation p_Nation1, Company p_Company) {
            _nation0 = p_Nation0;
            _nation1 = p_Nation1;
            _company = p_Company;


            WeaponDesignView.Bind(_company, _nation0, _nation1);
            ResearchView.Bind(_company);
            ActivitiesView.Bind(_company);
            SalesView.Bind(_company, _nation0, _nation1);
            WarPanelView.Bind(_company, _nation0, _nation1);

            WeaponDesignButton.onClick.AddListener(() => {
                PanelGroup.HideAll();
                CameraView.Move(CameraSpotType.Design, () =>
                {
                    WeaponDesignView.gameObject.GetComponent<PanelGroupElement>().Toggle();
                });
                EventSystem.current.SetSelectedGameObject(null);
            });

            ReseachViewButton.onClick.AddListener(() => {
                PanelGroup.HideAll();
                CameraView.Move(CameraSpotType.Research, () =>
                {
                    ResearchView.GetComponent<PanelGroupElement>().Toggle();
                });
                EventSystem.current.SetSelectedGameObject(null);
            });

            SalesViewButton.onClick.AddListener(() => {
                PanelGroup.HideAll();
                CameraView.Move(CameraSpotType.Sale, () =>
                {
                    SalesView.GetComponent<PanelGroupElement>().Toggle();
                    SalesView.Show();
                });
                EventSystem.current.SetSelectedGameObject(null);
            });

            WarViewButton.onClick.AddListener(() => {
                PanelGroup.HideAll();
                CameraView.Move(CameraSpotType.War, () => {
                    WarPanelView.GetComponent<PanelGroupElement>().Toggle();
                    WarPanelView.UpdateUi();
                });

                EventSystem.current.SetSelectedGameObject(null);
            });

            CameraView.Init();
        }

        public void UpdateWarState(float p_WarProgress) {
            float leftOrientedWinPercent = (p_WarProgress + 100) / 200;
            WarStateLeftImage.fillAmount = leftOrientedWinPercent;
            WarStateRightImage.fillAmount = 1 - leftOrientedWinPercent;
        }
        IEnumerator MoneyColor(bool isRed)
        {
            yield return new WaitForSeconds(2f);
            if(isRed)
            {
                _redAnim--;
            }
            else
            {
                _greenAnim--;
            }
            UpdateCompanyState();
        }

        public void UpdateCompanyState(int tickCount=-1)
        {
            
            string moneyText = "";
            string timeText = tickCount >= 0 ? CalculateTime(tickCount) : _oldTimeText;
            
            if(_company.OldMoney == _company.Money.Value && (_redAnim == 0 && _greenAnim == 0))
            {
                moneyText = "\nMoney: " + _company.Money;
            }
            if(_company.OldMoney > _company.Money.Value)
            {
                _difference = (_company.Money.Value - _company.OldMoney);
                StartCoroutine(MoneyColor(true));
                _redAnim++;
                Debug.Log("Entered this");
                _redIsNewer = true;
            }
            if (_company.OldMoney < _company.Money.Value)
            {
                _difference = (_company.Money.Value - _company.OldMoney);
                StartCoroutine(MoneyColor(false));
                _greenAnim++;
                _redIsNewer = false;
            }


            if (_redAnim > 0 && _redIsNewer && _difference != 0)
            {
                moneyText = "\nMoney: " + _company.Money + " (<color=red>" + _difference + "</color>)";
            }
            if(_greenAnim > 0 && !_redIsNewer && _difference != 0)
            {
                moneyText = "\nMoney: " + _company.Money + " (<color=green>+" + _difference + "</color>)";
            }
            CompanyInfoText.text = "Company Name: " + _company.Name +
                moneyText +
                "\n" + timeText;
            _oldTimeText = timeText;
            _company.OldMoney = _company.Money.Value;
            
        }

        string CalculateTime(int tickCount)
        {
            string result = "";
            int w = tickCount % 4 + 1;

            int m = (tickCount / 4) % 12 + 1;
            int y = (tickCount / (4 * 12)) + 1;
            result = "Y" + y + " M" + m + " W" + w;
            return result;
        }
    }
}
