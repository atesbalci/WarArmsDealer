using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Models
{
    public class Conditions : ModelBase
    {

        public static bool CanResearch(Company company)
        {
            return company.Activities.Count(x => x is ResearchActivity) < 2;

        }

        public static bool CanDesign(Weapon weapon, Company company)
        {
            return company.Activities.Count(x => x is DesignActivity) < 2 && company.Money.Value >= weapon.GetCost();
        }
    }
}


