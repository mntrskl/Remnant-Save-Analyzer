using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somethingDifferent.Models
{
    public class EventModel
    {
        public string zone;
        public string subZone;
        public string location;
        public string name;
        public string AssociatedItem;
        public EventType eventType;

        public EventModel()
        {
            eventType = EventType.Invalid;
        }
    }
}
