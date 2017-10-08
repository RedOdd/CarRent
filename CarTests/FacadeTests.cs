using System;
using CarRent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarTests
{
    [TestClass]
    public class FacadeTests
    {
        [TestMethod]
        public void AdminFacadeWithOneCar_StringShouldBeTrue()
        {
            var testingAdminFacade = new AdminFacade();
            testingAdminFacade.AddNewCar(new Car("Nissan", "Red", OccupationStatus.Free));

            var exeptedList = new List<CarInBase>();
            exeptedList.Add(new CarInBase(0, new Car("Nissan", "Red", OccupationStatus.Free)));
            var exeptedListJSON = JsonConvert.SerializeObject(exeptedList);

            Assert.AreEqual(testingAdminFacade.ShowAllCars(), exeptedListJSON);
        }
    }
}
