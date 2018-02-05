using System.Collections.Generic;

namespace Game.Models
{
    public class Company : ModelBase
    {
        public List<Activity> Activities { get; set; }
        public List<Weapon> DesignedWeapons;
        public Technology Tech { get; set; }
        public Design CompanyDesigns;
        public string Name;
        public float Money;


        public Company(string name = "")
        {
            CompanyDesigns = new Design(this);
            Activities = new List<Activity>();
            DesignedWeapons = new List<Weapon>();
            Tech = new Technology();
            Money = 1000f;
<<<<<<< HEAD
        }

        public Company(string p_Name) {
            CompanyDesigns = new Design(this);
            ActiveResearches = new List<ResearchActivity>();
            ActiveDesigns = new List<DesignActivity>();
            DesignedWeapons = new List<Weapon>();
            Tech = new Technology();
            Money = 1000f;


            Name = p_Name;
=======
            Name = name;
>>>>>>> 997096acdbee376af7a6b4d8c2d08b0c1a30d51e
        }

        public override void Tick()
        {
            base.Tick();
            for (var i = 0; i < Activities.Count; i++)
            {
                var activity = Activities[i];
                activity.Tick();
                if (activity.RemainingDuration <= 0)
                {
                    Activities.RemoveAt(i);
                    i--;
                }
            }
        }


        public void AddResearch(Research research)
        {
            Activities.Add(new ResearchActivity(research));
        }
        [System.Obsolete("This is an obsolete method")]
        public void AddDesign(Weapon design)
        {
            Activities.Add(new DesignActivity(design, CompanyDesigns.BaseDesignTime));
        }
    }
}
