using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRent
{
    public class CarsBase
    {
        private List<CarInBase> BaseOfCars = new List<CarInBase>();
        static private int BaseOfCarsID = 0;
        private string BaseOfCarsJSON;
        private string BaseOfFreeCarsJSON;
        private bool IsSuccessful = false;

        public void AddNewCar(Car car)
        {
            BaseOfCars.Add(new CarInBase(BaseOfCarsID, car));
            BaseOfCarsID++;
        }

        public string ShowAllCars()
        {    
            BaseOfCarsJSON = JsonConvert.SerializeObject(BaseOfCars);
            return BaseOfCarsJSON;
        }

        public string ShowFreeCars(DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            plannedTimeStartOfUse = plannedTimeStartOfUse.Date;
            plannedTimeEndOfUse = plannedTimeEndOfUse.Date;
            CheckFreeCarsForCertanTime(plannedTimeStartOfUse, plannedTimeEndOfUse);
            BaseOfFreeCarsJSON = JsonConvert.SerializeObject(BaseOfCars.FindAll(FindFreeCar));
            return BaseOfFreeCarsJSON;
        }

        private static bool FindFreeCar(CarInBase carInBase)
        {

            if (carInBase.Car.IsFreeToRent())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void CheckFreeCarsForCertanTime(DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            foreach (var carInBase in BaseOfCars)
            {
                if (CarBecameFree(carInBase, plannedTimeStartOfUse, plannedTimeEndOfUse))
                {
                    carInBase.UpdateOccupationStatus(OccupationStatus.Free);
                }
                else
                {
                    carInBase.UpdateOccupationStatus(OccupationStatus.Busy);
                } 
            }
        }

        private static bool CarBecameFree(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            if ((DateTime.Today < plannedTimeStartOfUse)&&(CheckFreeRentalTime(carInBase, plannedTimeStartOfUse, plannedTimeEndOfUse)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckFreeRentalTime(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            var countCheckUp = (carInBase.AvailabilityСalendar.Count-1) / 11;
            if ((plannedTimeStartOfUse > carInBase.LastCheckUpEnded) && (DateTime.Today < plannedTimeStartOfUse))
            {
                if (carInBase.AvailabilityСalendar.Count % 11 == 0)
                {
                    return true;
                }
                else
                {
                    if (carInBase.AvailabilityСalendar.Count % 11 == 9)
                    {
                        return true;
                    }
                    else
                    {
                        var isRightPlace = true;
                        for (int count = countCheckUp * 11; count < carInBase.AvailabilityСalendar.Count; count++)
                        {
                            if (((carInBase.AvailabilityСalendar[count].StartOfUse <= plannedTimeStartOfUse) || (plannedTimeStartOfUse <= carInBase.AvailabilityСalendar[count].EndOfUse)) ||
                                ((carInBase.AvailabilityСalendar[count].StartOfUse <= plannedTimeEndOfUse) || (plannedTimeEndOfUse <= carInBase.AvailabilityСalendar[count].EndOfUse)) ||
                                ((plannedTimeStartOfUse <= carInBase.AvailabilityСalendar[count].StartOfUse) || (carInBase.AvailabilityСalendar[count].StartOfUse <= plannedTimeEndOfUse)) ||
                                ((plannedTimeStartOfUse <= carInBase.AvailabilityСalendar[count].EndOfUse)||(carInBase.AvailabilityСalendar[count].EndOfUse <= plannedTimeEndOfUse)))
                            {
                                isRightPlace = false;
                                break;
                            }
                        }
                        return isRightPlace;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public void RentСar(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            IsSuccessful = false;
            plannedTimeStartOfUse = plannedTimeStartOfUse.Date;
            plannedTimeEndOfUse = plannedTimeEndOfUse.Date;
            CheckFreeCarsForCertanTime(plannedTimeStartOfUse, plannedTimeEndOfUse);
            CheckFreeRentalTimeAndAddOrderIfFree(carInBase, plannedTimeStartOfUse, plannedTimeEndOfUse);
            carInBase.UpdateOccupationStatus(OccupationStatus.Busy);
        }

        public bool GetSuccessful()
        {
            return IsSuccessful;
        }

        private void CheckFreeRentalTimeAndAddOrderIfFree(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            var countCheckUp = (carInBase.AvailabilityСalendar.Count - 1) / 11;
            if ((plannedTimeStartOfUse > carInBase.LastCheckUpEnded) && (DateTime.Today < plannedTimeStartOfUse))
            {
                if (carInBase.AvailabilityСalendar.Count % 11 == 0)
                {
                    carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                    IsSuccessful = true;
                }
                else
                {
                    if (carInBase.AvailabilityСalendar.Count % 11 == 9)
                    {
                        carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                        IsSuccessful = true;
                    }
                    else
                    {
                        var isRightPlace = true;
                        for (int count = countCheckUp * 11; count < carInBase.AvailabilityСalendar.Count; count++)
                        {
                            if (((carInBase.AvailabilityСalendar[count].StartOfUse <= plannedTimeStartOfUse) && (plannedTimeStartOfUse <= carInBase.AvailabilityСalendar[count].EndOfUse)) ||
                                ((carInBase.AvailabilityСalendar[count].StartOfUse <= plannedTimeEndOfUse) && (plannedTimeEndOfUse <= carInBase.AvailabilityСalendar[count].EndOfUse)) ||
                                ((plannedTimeStartOfUse <= carInBase.AvailabilityСalendar[count].StartOfUse) && (carInBase.AvailabilityСalendar[count].StartOfUse <= plannedTimeEndOfUse)) ||
                                ((plannedTimeStartOfUse <= carInBase.AvailabilityСalendar[count].EndOfUse) && (carInBase.AvailabilityСalendar[count].EndOfUse <= plannedTimeEndOfUse)))
                            {
                                isRightPlace = false;
                                break;
                            }
                        }
                       if (isRightPlace)
                        {
                            carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                            IsSuccessful = true;
                        }
                    }
                }
            }
        }
    }
}
