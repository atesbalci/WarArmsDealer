using Utils;
using UnityEngine;

namespace Game.Models
{
    public class DesignCompleteEvent : WEvent
    {
        public DesignActivity DesignActivity { get; set; }
    }

    public class DesignActivity : Activity
    {
        public Weapon CreatedWeapon { get; private set; }

        public DesignActivity(Weapon design, int designTime) : base(designTime)
        {
            CreatedWeapon = design;
        }

        protected override void ActivityComplete()
        {
            MessageManager.Send(new DesignCompleteEvent { DesignActivity = this });
        }
    }
}
