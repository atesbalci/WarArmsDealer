using System;

namespace Game.Models
{
    public enum StatType : uint { }

    public class Technology : ModelBase
    {
        private const int DefaultTechLevel = 3;

        private readonly int[] _levels;

        public Technology()
        {
            var statCnt = Enum.GetValues(typeof(StatType)).Length;
            _levels = new int[statCnt];
            for (var i = 0; i < statCnt; i++)
            {
                _levels[i] = DefaultTechLevel;
            }
        }

        /// <summary>
        /// Getter/Setter for Tech
        /// </summary>
        /// <param name="stat">Stat type</param>
        /// <returns>Tech level for given stat type</returns>
        public int this[StatType stat]
        {
            get { return _levels[(int)stat]; }
            set { _levels[(int)stat] = value; }
        }
    }
}
