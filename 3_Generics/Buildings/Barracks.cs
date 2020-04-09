using System;
using System.Collections.Generic;
using StrategyGame.Buildings.Intrefaces;
using StrategyGame.Warriors.Abstractions;
using StrategyGame.Warriors.Models.Infantry;

namespace StrategyGame.Buildings
{

    public class Barracks<T> : IBuilding<T> where T : CombatUnit
    {
        public readonly Dictionary<Type, int> warriorsCost = new Dictionary<Type, int>
        {
             {typeof(Pikeman), 11},
             {typeof(Bowman), 8},
             {typeof(Gunner), 15}
        };

        protected Barracks() { }

        public T SomeProp { get; set; }
    }
}
