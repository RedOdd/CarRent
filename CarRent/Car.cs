
using System;

namespace CarRent
{
    public class Car
    {
        public Car(string model, string color, OccupationStatus occupationStatus)
        {
            Model = model;
            Color = color;
            _occupationStatus = occupationStatus;
        }

        public void SetBusy()
        {
            _rentsCount++;
            _occupationStatus = OccupationStatus.Busy;
        }

        public bool IsCheckNeeded()
        {
            if (_rentsCount == 10)
            {
                return true;
            } else
            {
                return false;
            }

        }

        public void SetFree()
        {
            _occupationStatus = OccupationStatus.Free;
        }

        public bool IsFreeToRent()
        {
            return (_occupationStatus == OccupationStatus.Free);        
        }


        public string Model { get; }

        public string Color { get; }

        private int _rentsCount;
        private OccupationStatus _occupationStatus;

        
    }
}
