using Utils;
using System;

namespace Game.Models
{
    public class ResearchCompleteEvent : WEvent
    {
        public ResearchActivity ResearchActivity { get; set; }
    }

    public class ResearchActivity : Activity
    {
        public Research Research { get; private set; }

        public ResearchActivity(Research research) : base(research.CalculateDuration())
        {
            Research = research;
        }

        protected override void ActivityComplete()
        {
            MessageManager.Send(new ResearchCompleteEvent {ResearchActivity = this});
        }

        public override string ToString()
        {
            return Research.ResearchType == ResearchType.Stat ? Research.Weapon.ToString() : Enum.GetName(typeof(ResearchType), Research.ResearchType) + Research.Weapon.Stats[0].Value;
        }
    }
}
