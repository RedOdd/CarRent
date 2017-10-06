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
        static private List<CarInBase> BaseOfCars = new List<CarInBase>();
        static private int BaseOfCarsID = 0;
        static private string BaseOfCarsJSON;
        static private string BaseOfFreeCarsJSON;

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
                CheckAndDeleteOldCheckUp(carInBase);
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
           
                if (DateTime.Today < plannedTimeStartOfUse)
                {
                    if (carInBase.AvailabilityСalendar.Count >= 11)
                    {
                        var countCheckUp = carInBase.AvailabilityСalendar.Count % 11;
                        if (carInBase.AvailabilityСalendar.Count % 11 == 0)
                        {
                            if (plannedTimeStartOfUse > carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1].EndOfUse)
                            {
                                return true;
                            }
                            else
                            {
                            return false;
                            }
                        }
                        else
                        {
                            if (plannedTimeStartOfUse > carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1].EndOfUse)
                            {
                                return true;
                            }
                            else
                            {
                                bool isFindPlace = false;
                                for (var count = countCheckUp * 11; count < carInBase.AvailabilityСalendar.Count; count++)
                                {
                                    if ((carInBase.AvailabilityСalendar[count].EndOfUse < plannedTimeStartOfUse) && (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[count + 1].StartOfUse))
                                    {
                                        isFindPlace = true;
                                        break;
                                    }
                                }
                                return isFindPlace;
                            }
                        }
                    }
                    else
                    {
                        if ((carInBase.AvailabilityСalendar.Count == 0) || (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[0].StartOfUse))
                        {
                            if (carInBase.AvailabilityСalendar.Count == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (plannedTimeStartOfUse > carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1].EndOfUse)
                            {
                                return true;
                            }
                            else
                            {
                                bool isFindPlace = false;
                                for (int count = 0; count < carInBase.AvailabilityСalendar.Count - 1; count++)
                                {
                                    if ((carInBase.AvailabilityСalendar[count].EndOfUse < plannedTimeStartOfUse) && (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[count + 1].StartOfUse))
                                    {
                                        isFindPlace = true;
                                        break;
                                    }
                                }
                                return isFindPlace;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
        }

        
    

        public void CheckAndDeleteOldCheckUp(CarInBase carInBase)
        {
            var countCheckUp = carInBase.AvailabilityСalendar.Count % 11;
            if (carInBase.AvailabilityСalendar.Count >= 11)
            {
                for (var count = 10; count <= countCheckUp * 11; count += 11)
                {
                    if (carInBase.AvailabilityСalendar[count].EndOfUse < DateTime.Today)
                    {
                        for (var innerCount = 10; innerCount >= 0; innerCount--)
                        {
                            carInBase.AvailabilityСalendar.Remove(carInBase.AvailabilityСalendar[count - innerCount]);
                        }
                    }
                }
            }
        }      

        public void RentСar(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            plannedTimeStartOfUse = plannedTimeStartOfUse.Date;
            plannedTimeEndOfUse = plannedTimeEndOfUse.Date;
            CheckFreeCarsForCertanTime(plannedTimeStartOfUse, plannedTimeEndOfUse);
            CheckFreeRentalTimeAndAddOrderIfFree(carInBase, plannedTimeStartOfUse, plannedTimeEndOfUse);
            carInBase.UpdateOccupationStatus(OccupationStatus.Busy);
        }

        private void CheckFreeRentalTimeAndAddOrderIfFree(CarInBase carInBase, DateTime plannedTimeStartOfUse, DateTime plannedTimeEndOfUse)
        {
            if (DateTime.Today < plannedTimeStartOfUse)
            {
                if (carInBase.AvailabilityСalendar.Count >= 11)
                {
                    var countCheckUp = carInBase.AvailabilityСalendar.Count % 11;
                    if (carInBase.AvailabilityСalendar.Count % 11 == 0)
                    {
                        if (plannedTimeStartOfUse > carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1].EndOfUse)
                        {
                            carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                        }
                    }
                    else
                    {
                        if (plannedTimeStartOfUse > carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1].EndOfUse)
                        {
                            carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                        }
                        else
                        {
                            for (var count = countCheckUp * 11; count < carInBase.AvailabilityСalendar.Count; count++)
                            {
                                if ((carInBase.AvailabilityСalendar[count-1].EndOfUse < plannedTimeStartOfUse) && (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[count].StartOfUse))
                                {
                                    carInBase.AvailabilityСalendar.Add(carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1]);
                                    for (int innerCount = count; innerCount < carInBase.AvailabilityСalendar.Count-1; innerCount++)
                                    {
                                        carInBase.AvailabilityСalendar[innerCount+1] = carInBase.AvailabilityСalendar[innerCount];
                                    }
                                    carInBase.AvailabilityСalendar[count] = new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse);
                                }
                            }
                        }
                    }
                }
                else
                {   /* if ((carInBase.AvailabilityСalendar.Count == 0) || (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[0].StartOfUse))
                        {
                            if (carInBase.AvailabilityСalendar.Count == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                        } */
                    if ((carInBase.AvailabilityСalendar.Count == 0 || (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[0].StartOfUse)))
                    {
                        if (carInBase.AvailabilityСalendar.Count == 0)
                        {
                            carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                        }
                        else
                        {
                            if (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[0].StartOfUse)
                            {
                                carInBase.AvailabilityСalendar.Add(carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1]);
                                for (var count = 0; count < carInBase.AvailabilityСalendar.Count; count++)
                                {
                                    carInBase.AvailabilityСalendar[count + 1] = carInBase.AvailabilityСalendar[count];
                                }
                                carInBase.AvailabilityСalendar[0] = new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse);
                            }
                        }
                    }
                    else
                    {
                        if (plannedTimeStartOfUse > carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1].EndOfUse)
                        {
                            carInBase.AvailabilityСalendar.Add(new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse));
                        }
                        else
                        {
                            for (int count = 0; count < carInBase.AvailabilityСalendar.Count - 1; count++)
                            {
                                if ((carInBase.AvailabilityСalendar[count].EndOfUse < plannedTimeStartOfUse) && (plannedTimeEndOfUse < carInBase.AvailabilityСalendar[count + 1].StartOfUse))
                                {
                                    carInBase.AvailabilityСalendar.Add(carInBase.AvailabilityСalendar[carInBase.AvailabilityСalendar.Count - 1]);
                                    for (int innerCount = carInBase.AvailabilityСalendar.Count - 2; innerCount > count + 1; innerCount--)
                                    {
                                        carInBase.AvailabilityСalendar[innerCount] = carInBase.AvailabilityСalendar[innerCount - 1];
                                    }
                                    carInBase.AvailabilityСalendar[count + 1] = new AvailabilityСalendar(plannedTimeStartOfUse, plannedTimeEndOfUse);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
