using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarRentTests
{
    [TestClass]
    public class CarBaseTest
    {
        [TestMethod]
        public void CheckVoidShowFreeCars_AddEqualCarsWithDifferentOccupationStatus_FreeFree_RentedFree_IsBaseOfCarsShouldBeEqual()
        {
            
            var exeptedBaseOfCars = new List<CarInBase>();
            exeptedBaseOfCars.Add(new CarInBase(0,new Car( "Nissan", "Red", OccupationStatus.Free)));
            exeptedBaseOfCars.Add(new CarInBase(1, new Car("Nissan", "Red", OccupationStatus.Free)));
            var exeptedBaseOfCarsJSON = JsonConvert.SerializeObject(exeptedBaseOfCars);


            var testedCarsBase = new CarsBase();
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Busy));
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            

            Assert.AreEqual(exeptedBaseOfCarsJSON, testedCarsBase.ShowFreeCars(DateTime.Parse("10.10.2017"),DateTime.Parse("15.10.2017")));
        }

        [TestMethod]
        public void CheckSomething()
        {

            var exeptedBaseOfCars = new List<CarInBase>();
            exeptedBaseOfCars.Add(new CarInBase(0, new Car("Nissan", "Red", OccupationStatus.Free)));
            exeptedBaseOfCars.Add(new CarInBase(1, new Car("Nissan", "Red", OccupationStatus.Free)));
            var exeptedBaseOfCarsJSON = JsonConvert.SerializeObject(exeptedBaseOfCars);


            var testedCarsBase = new CarsBase();
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Busy));
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            var testedCarsBaseForJSON = JsonConvert.DeserializeObject<List<CarInBase>>(testedCarsBase.ShowFreeCars(DateTime.Parse("10.10.2017"), DateTime.Parse("15.10.2017")));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Parse("10.10.2017"), DateTime.Parse("15.10.2017"));


            Assert.AreEqual(exeptedBaseOfCarsJSON, testedCarsBase.ShowFreeCars(DateTime.Parse("10.10.2017"), DateTime.Parse("15.10.2017")));
        }
    }
}
