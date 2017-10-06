using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarRent
{
    class AdminFacade
    {
        CarsBase baseOfCars = new CarsBase();

        public void ShowAllCars()
        {
            baseOfCars.ShowAllCars();
        }

        public void AddNewCar(Car car)
        {
            baseOfCars.AddNewCar(car);
        }
    }
}
