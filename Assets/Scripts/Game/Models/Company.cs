using System.Collections.Generic;
using UniRx;

namespace Game.Models
{
    public class Company : ModelBase
    {
        public ReactiveCollection<Activity> Activities { get; set; }
        public List<Weapon> DesignedWeapons;
        public Technology Tech { get; set; }
        public Design CompanyDesigns;
        public string Name;
        public float Money;


        public Company(string name = "")
        {
            CompanyDesigns = new Design(this);
            Activities = new ReactiveCollection<Activity>();
            DesignedWeapons = new List<Weapon>();
            Tech = new Technology();
            Money = 1000f;
            Name = name;
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

        public void AddResearch(ResearchActivity p_ResearchActivity) {
            Activities.Add(p_ResearchActivity);
        }

        [System.Obsolete("This is an obsolete method")]
        public void AddDesign(Weapon design)
        {
            Activities.Add(new DesignActivity(design, CompanyDesigns.BaseDesignTime));
        }
    }
}
