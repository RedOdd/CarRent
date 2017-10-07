using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CarRent
{
    public class CarInBase
    {
        public int Id;
        public Car Car;
        public List<AvailabilityСalendar> AvailabilityСalendar = new List<AvailabilityСalendar>();
        public DateTime LastCheckUpEnded;

        public CarInBase(int id,Car car)
        {
            Id = id;
            Car = car;
            
        }

        public void UpdateOccupationStatus(OccupationStatus occupationStatus)
        {
            if (occupationStatus == OccupationStatus.Free)
            {
                Car.SetFree();
                
            }
            
            if (occupationStatus == OccupationStatus.Busy)
            {
                Car.SetBusy();
                if (Car.IsCheckNeeded())
                {
                    AvailabilityСalendar.Add(new AvailabilityСalendar(AvailabilityСalendar[AvailabilityСalendar.Count - 1].EndOfUse.AddDays(1), AvailabilityСalendar[AvailabilityСalendar.Count - 1].EndOfUse.AddDays(1).AddDays(6)));
                    LastCheckUpEnded = AvailabilityСalendar[AvailabilityСalendar.Count - 1].EndOfUse;
                }
            }
        }
    
    }
}
