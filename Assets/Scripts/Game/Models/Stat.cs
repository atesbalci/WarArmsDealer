using System;

namespace Game.Models {
    [AttributeUsage(AttributeTargets.Class)]
    public class StatAttribute : Attribute {
        public string Name { get; set; }

        public StatAttribute(string name) {
            Name = name;
        }
    }

    /// <summary>
    /// Attack is 1, Health is 2, Armor is 3, Piercing is 4, Support is 5
    /// </summary>
    public enum StatType : uint {
        Attack,
        Health,
        Armor,
        Piercing,
        Support
    }

    [Stat("Stat")]
    public class Stat : ModelBase {
        public int Value { get; set; }
        public StatType Type { get; set; }

        public Stat() {
            Value = 1;
            Type = StatType.Attack;
        }
    }
}
