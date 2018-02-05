﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class Conditions : ModelBase
    {

        public static bool CanResearch(Company company)
        {
            return company.ActiveResearches.Count < 2 ? true : false;

        }

        public static bool CanDesign(Weapon weapon, Company company)
        {
            bool result = false;
            return company.ActiveDesigns.Count < 2 ? true : false;

        }
    }
}


