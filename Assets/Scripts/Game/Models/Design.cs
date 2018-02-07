using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UniRx;


namespace Game.Models
{
    //This class holds the designs of a company
    public class Design : ModelBase
    {
        private Company _playerCompany;

        public ReactiveCollection<Weapon> CompletedDesigns;

        public int BaseDesignTime = 3;


        public Design(Company playerCompany)
        {
            CompletedDesigns = new ReactiveCollection<Weapon>();
            _playerCompany = playerCompany;
            Debug.Log("Subscribed");
            MessageManager.Receive<DesignCompleteEvent>().Subscribe(ev => {designComplete(ev.DesignActivity); });
            
        }



        public void CreateDesignActivity(Weapon weapon)
        {
            //maybe we will modify the design time
            _playerCompany.Money.Value -= weapon.GetCost();

            _playerCompany.Activities.Add(new DesignActivity(weapon, Mathf.CeilToInt(weapon.GetDuration())));
        }

        void designComplete(DesignActivity da)
        {
            CompletedDesigns.Add(da.CreatedWeapon);
            Debug.Log("Design complete!");
        }
    }
}

