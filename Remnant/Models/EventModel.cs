using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remnant.Models
{
    public class EventModel
    {
        public string zone;
        public string subZone;
        public string location;
        public EventType eventType;
        public string name;
        public bool complete;

        public EventModel()
        {
            eventType = EventType.Invalid;
            complete = false;
        }
    }
}
