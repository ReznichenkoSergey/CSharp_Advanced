using StrategyGame.Warriors.Abstractions;
using StrategyGame.Warriors.Interfaces;
using StrategyGame.Warriors.Models;
using System;
using System.Collections.Generic;

using static ITEA_Collections.Common.Extensions;

namespace StrategyGame.Actions
{
    public class CavalryAttack<T> : Battle<Knight, T>
        where T : CombatUnit, IRangeUnit, new()
    {
        public CavalryAttack(IEnumerable<Knight> army1, IEnumerable<T> army2) : base(army1, army2)
        {

        }

        protected override int CountPoints(IEnumerable<CombatUnit> army)
        {
            //Strength для CombatUnitType.Melee х2.
            int total = 0;
            int count = 0;
            foreach (var item in army)
            {
                count++;
                total += item.Health + (item.UnitType == CombatUnitType.Melee ? 2 * item.Strength : item.Strength);
            }
            ToConsole($"Total army count: {count}", ConsoleColor.DarkYellow);
            ToConsole($"Total army strength: {total}", ConsoleColor.DarkYellow);
            return total;
        }

        public void G()
        {
            base.CountResults();
        }

    }

}
