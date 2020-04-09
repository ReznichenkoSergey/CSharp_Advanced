using StrategyGame.Base.Interfaces;
using StrategyGame.Warriors.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Warriors.Models
{
    public class Knight : CombatUnit
    {
        public Knight(): base(18, 10, CombatUnitType.Melee) { }

        public override void MeleeAttack(IUnit unit)
        {
            throw new NotImplementedException();
        }
    }
}
