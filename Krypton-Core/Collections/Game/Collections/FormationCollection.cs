using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Collections
{
    public class FormationCollection
    {
        public const string DEFAULT_FORMATION = "drone_formation_default";
        public const string TURTLE_FORMATION = "drone_formation_f-01-tu";
        public const string ARROW_FORMATION = "drone_formation_f-02-ar";
        public const string LANCE_FORMATION = "drone_formation_f-03-la";
        public const string STAR_FORMATION = "drone_formation_f-04-st";
        public const string PINCER_FORMATION = "drone_formation_f-05-pi";
        public const string DOUBLE_ARROW_FORMATION = "drone_formation_f-06-da";
        public const string DIAMOND_FORMATION = "drone_formation_f-07-di";
        public const string CHEVRON_FORMATION = "drone_formation_f-08-ch";
        public const string MOTH_FORMATION = "drone_formation_f-09-mo";
        public const string CRAB_FORMATION = "drone_formation_f-10-cr";
        public const string HEART_FORMATION = "drone_formation_f-11-he";
        public const string BARRAGE_FORMATION = "drone_formation_f-12-ba";
        public const string BAT_FORMATION = "drone_formation_f-13-bt";
        public const string DOME_FORMATION = "drone_formation_f-3d-dm";
        public const string DRILL_FORMATION = "drone_formation_f-3d-dr";
        public const string RING_FORMATION = "drone_formation_f-3d-rg";
        public const string VETERAN_FORMATION = "drone_formation_f-3d-vt";
        public const string WHEEL_FORMATION = "drone_formation_f-3d-wl";
        public const string WAVE_FORMATION = "drone_formation_f-3d-wv";
        public const string X_FORMATION = "drone_formation_f-3d-x";

        
        public static List<Tuple<string, string>> Formations = new()
        {
            new Tuple<string, string>(DEFAULT_FORMATION, "Default"),
            new Tuple<string, string>(TURTLE_FORMATION, "Turtle"),
            new Tuple<string, string>(ARROW_FORMATION, "Arrow"),
            new Tuple<string, string>(LANCE_FORMATION, "Lance"),
            new Tuple<string, string>(STAR_FORMATION, "Star"),
            new Tuple<string, string>(PINCER_FORMATION, "Pincer"),
            new Tuple<string, string>(DOUBLE_ARROW_FORMATION, "Double Arrow"),
            new Tuple<string, string>(DIAMOND_FORMATION, "Diamond"),
            new Tuple<string, string>(CHEVRON_FORMATION, "Chevron"),
            new Tuple<string, string>(MOTH_FORMATION, "Moth"),
            new Tuple<string, string>(CRAB_FORMATION, "Crab"),
            new Tuple<string, string>(HEART_FORMATION, "Heart"),
            new Tuple<string, string>(BARRAGE_FORMATION, "Barrage"),
            new Tuple<string, string>(BAT_FORMATION, "Bat"),
            new Tuple<string, string>(DOME_FORMATION, "Dome"),
            new Tuple<string, string>(DRILL_FORMATION, "Drill"),
            new Tuple<string, string>(RING_FORMATION, "Ring"),
            new Tuple<string, string>(VETERAN_FORMATION, "Veteran"),
            new Tuple<string, string>(WHEEL_FORMATION, "Wheel"),
            new Tuple<string, string>(WAVE_FORMATION, "Wave"),
            new Tuple<string, string>(X_FORMATION, "X"),
        };
    }
}
