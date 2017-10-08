using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarRent
{
    public class AdminFacade
    {
        private CarsBase baseOfCars = new CarsBase();

        public string ShowAllCars()
        {
           return baseOfCars.ShowAllCars();
        }

        public void AddNewCar(Car car)
        {
            baseOfCars.AddNewCar(car);
        }
    }
}
