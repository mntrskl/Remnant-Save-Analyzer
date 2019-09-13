using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remnant.Models
{
    public enum EventType
    {
        Invalid,
        SideDungeon,
        WorldBoss,
        Siege,
        MiniBoss,
        ItemDrop
    }

    public static class EventTypeExtensions
    {
        public static string ToReadableString(this EventType value)
        {
            switch(value)
            {
                case EventType.SideDungeon:
                    return "Side Dungeon";
                case EventType.WorldBoss:
                    return "World Boss";
                case EventType.Siege:
                    return "Siege";
                case EventType.MiniBoss:
                    return "Mini-Boss";
                case EventType.ItemDrop:
                    return "Item Drop";
                default:
                    return "??";
            }
        }
    }
}
