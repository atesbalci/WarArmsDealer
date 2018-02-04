using System;

namespace Game.Models {
    public class Stat : ModelBase {
        public int Value { get; set; }
        public StatType Type
        {
            //TODO Ates: Perhaps think of something better than this?
            get { return (StatType) Enum.Parse(typeof(StatType), GetType().Name); }
        }

        public Stat() {
            Value = 1;
        }
    }
}
