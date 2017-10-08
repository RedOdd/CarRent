using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent
{
    public class UserFacade
    {
        private bool IsHaveRent = false;
        private CarsBase baseOfCars = new CarsBase();
        private DateTime ExpirationTime = DateTime.MinValue;

        public string ShowFreeCars(DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
           return baseOfCars.ShowFreeCars(plannedTimeStartOfUse, plannedTimeEndOfUse);
        }

        public void RentCar(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            if ((IsHaveRent == false) && (ExpirationTime < plannedTimeStartOfUse))
            {
                baseOfCars.RentСar(carInBase, plannedTimeStartOfUse, plannedTimeEndOfUse);
                if (baseOfCars.GetSuccessful())
                {
                    ExpirationTime = plannedTimeEndOfUse;
                    IsHaveRent = true;
                }
            }
        }
    }
}
