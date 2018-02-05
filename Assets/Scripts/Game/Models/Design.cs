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

        public List<Weapon> CompletedDesigns;

        public int BaseDesignTime = 3;


        public Design(Company playerCompany)
        {
            CompletedDesigns = new List<Weapon>();
            _playerCompany = playerCompany;
            Debug.Log("Subscribed");
            MessageManager.Receive<DesignCompleteEvent>().Subscribe(ev => {designComplete(ev.DesignActivity); });
        }



        public void CreateDesignActivity(Weapon weapon)
        {
            //maybe we will modify the design time
            _playerCompany.Money -= weapon.GetCost();

            _playerCompany.Activities.Add(new DesignActivity(weapon, BaseDesignTime));
        }

        void designComplete(DesignActivity da)
        {
            CompletedDesigns.Add(da.CreatedWeapon);
            Debug.Log("Design complete!");
        }
    }
}

