﻿using System;
using Activator.Spells;
using LeagueSharp.Common;

namespace Activator.Spells.Shields
{
    class defensiveballcurl : spell
    {
        internal override string Name
        {
            get { return "defensiveballcurl"; }
        }

        internal override string DisplayName
        {
            get { return "Defensive Ball Curl | W"; }
        }

        internal override float Range
        {
            get { return float.MaxValue; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.SelfLowHP, MenuType.SelfMuchHP, MenuType.SelfMinMP }; }
        }

        internal override int DefaultHP
        {
            get { return 95; }
        }

        internal override int DefaultMP
        {
            get { return 55; }
        }

        public override void OnTick()
        {
            if (!Menu.Item("use" + Name).GetValue<bool>())
                return;

            if (Player.Mana / Player.MaxMana * 100 <
                Menu.Item("SelfMinMP" + Name + "Pct").GetValue<Slider>().Value)
                return;

            foreach (var hero in champion.Heroes)
            {
                if (hero.Player.NetworkId == Player.NetworkId)
                {
                    if (hero.IncomeDamage / hero.Player.MaxHealth * 100 >=
                        Menu.Item("SelfMuchHP" + Name + "Pct").GetValue<Slider>().Value)
                        UseSpellOn(hero.Player);

                    if (hero.Player.Health / hero.Player.MaxHealth * 100 <=
                        Menu.Item("SelfLowHP" + Name + "Pct").GetValue<Slider>().Value && hero.IncomeDamage > 0)
                        UseSpellOn(hero.Player);
                }
            }
        }
    }
}