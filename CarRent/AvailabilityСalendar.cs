using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent
{
    public class AvailabilityСalendar
    {
        
        public AvailabilityСalendar(DateTime start, DateTime end)
        {
            StartOfUse = start;
            EndOfUse = end;
        }

        public DateTime StartOfUse;
        public DateTime EndOfUse;

        
    }
}
