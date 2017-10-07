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
        public void AddEqualCarsWithDifferentOccupationStatus_IsBaseOfCarsShouldBeEqual()
        {
            
            var exeptedBaseOfCars = new List<CarInBase>();
            exeptedBaseOfCars.Add(new CarInBase(0,new Car( "Nissan", "Red", OccupationStatus.Free)));
            exeptedBaseOfCars.Add(new CarInBase(1, new Car("Nissan", "Red", OccupationStatus.Free)));
            var exeptedBaseOfCarsJSON = JsonConvert.SerializeObject(exeptedBaseOfCars);

            var testedCarsBase = new CarsBase();
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Busy));
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            
            Assert.AreEqual(exeptedBaseOfCarsJSON, testedCarsBase.ShowFreeCars(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
        }

        [TestMethod]
        public void AddEqualCars_IsEndOfTimeUsingShouldBeEqual()
        {

            var exeptedBaseOfCars = new List<CarInBase>();
            exeptedBaseOfCars.Add(new CarInBase(0, new Car("Nissan", "Red", OccupationStatus.Free)));
            exeptedBaseOfCars.Add(new CarInBase(1, new Car("Nissan", "Red", OccupationStatus.Free)));
            var exeptedBaseOfCarsJSON = JsonConvert.SerializeObject(exeptedBaseOfCars);
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            var testedCarsBase = new CarsBase();
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            var testedCarsBaseForJSON = JsonConvert.DeserializeObject<List<CarInBase>>(testedCarsBase.ShowFreeCars(DateTime.Parse("10.10.2017"), DateTime.Parse("15.10.2017")));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(1), DateTime.Today.AddDays(2));

            Assert.AreEqual(exeptedBaseOfCars[0].AvailabilityСalendar[0].EndOfUse, testedCarsBaseForJSON[0].AvailabilityСalendar[0].EndOfUse);
        }

        [TestMethod]
        public void AddMuchRentWithAutomaticCheckUpWithWrongRent_IsEndOfTimeUsingShouldBeEqual()
        {

            var exeptedBaseOfCars = new List<CarInBase>();
            exeptedBaseOfCars.Add(new CarInBase(0, new Car("Nissan", "Red", OccupationStatus.Free)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(3), DateTime.Today.AddDays(4)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(5), DateTime.Today.AddDays(6)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(7), DateTime.Today.AddDays(8)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(9), DateTime.Today.AddDays(10)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(11), DateTime.Today.AddDays(12)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(13), DateTime.Today.AddDays(14)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(15), DateTime.Today.AddDays(16)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(17), DateTime.Today.AddDays(18)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(19), DateTime.Today.AddDays(20)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(21), DateTime.Today.AddDays(27)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(29), DateTime.Today.AddDays(30)));
            exeptedBaseOfCars[0].LastCheckUpEnded = DateTime.Parse("03.11.2017 0:00:00");

            var testedCarsBase = new CarsBase();
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            var testedCarsBaseForJSON = JsonConvert.DeserializeObject<List<CarInBase>>(testedCarsBase.ShowFreeCars(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(1), DateTime.Today.AddDays(2));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(3), DateTime.Today.AddDays(4));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(5), DateTime.Today.AddDays(6));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(7), DateTime.Today.AddDays(8));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(9), DateTime.Today.AddDays(10));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(11), DateTime.Today.AddDays(12));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(13), DateTime.Today.AddDays(14));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(15), DateTime.Today.AddDays(16));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(17), DateTime.Today.AddDays(18));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(19), DateTime.Today.AddDays(20));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(21), DateTime.Today.AddDays(22));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(23), DateTime.Today.AddDays(24));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(29), DateTime.Today.AddDays(30));

            Assert.AreEqual(exeptedBaseOfCars[0].AvailabilityСalendar[11].EndOfUse, testedCarsBaseForJSON[0].AvailabilityСalendar[11].EndOfUse);
        }

        [TestMethod]
        public void AddMuchRentWithAutomaticCheckUp_IsEndOfTimeUsingShouldBeEqual()
        {

            var exeptedBaseOfCars = new List<CarInBase>();
            exeptedBaseOfCars.Add(new CarInBase(0, new Car("Nissan", "Red", OccupationStatus.Free)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(3), DateTime.Today.AddDays(4)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(5), DateTime.Today.AddDays(6)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(7), DateTime.Today.AddDays(8)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(9), DateTime.Today.AddDays(10)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(11), DateTime.Today.AddDays(12)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(13), DateTime.Today.AddDays(14)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(15), DateTime.Today.AddDays(16)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(17), DateTime.Today.AddDays(18)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(19), DateTime.Today.AddDays(20)));
            exeptedBaseOfCars[0].AvailabilityСalendar.Add(new AvailabilityСalendar(DateTime.Today.AddDays(21), DateTime.Today.AddDays(27)));

            var testedCarsBase = new CarsBase();
            testedCarsBase.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));
            var testedCarsBaseForJSON = JsonConvert.DeserializeObject<List<CarInBase>>(testedCarsBase.ShowFreeCars(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(1), DateTime.Today.AddDays(2));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(3), DateTime.Today.AddDays(4));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(5), DateTime.Today.AddDays(6));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(7), DateTime.Today.AddDays(8));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(9), DateTime.Today.AddDays(10));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(11), DateTime.Today.AddDays(12));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(13), DateTime.Today.AddDays(14));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(15), DateTime.Today.AddDays(16));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(17), DateTime.Today.AddDays(18));
            testedCarsBase.RentСar(testedCarsBaseForJSON[0], DateTime.Today.AddDays(19), DateTime.Today.AddDays(20));
            
            Assert.AreEqual(exeptedBaseOfCars[0].AvailabilityСalendar[10].EndOfUse, testedCarsBaseForJSON[0].AvailabilityСalendar[10].EndOfUse);
        }

    }
}
