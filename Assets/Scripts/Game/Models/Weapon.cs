using System;

namespace Game.Models {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class StatAttribute : Attribute
    {
        public string Name { get; set; }

        public StatAttribute(string name)
        {
            Name = name;
        }
    }

    public class Weapon : ModelBase {
        [Stat("Health")]
        public HealthStat HealthStat { get; set; }
        [Stat("Attack")]
        public AttackStat AttackStat { get; set; }

        public Weapon() {
            this.HealthStat = new HealthStat();
            this.AttackStat = new AttackStat();
        }
    }
}
