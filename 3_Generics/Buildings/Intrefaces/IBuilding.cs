using System;
using System.Collections.Generic;
using System.Text;
using StrategyGame.Warriors.Abstractions;

namespace StrategyGame.Buildings.Intrefaces
{
    public interface IBuilding<T> where T : CombatUnit
    {
        T SomeProp { get; set; }
    }
}
