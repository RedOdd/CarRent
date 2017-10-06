using CarRent; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CarRentTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void UseSendToRent_IsFreeToRentShouldBeFalse()
        {
            var testedCar = new Car("Nissan", "Red", OccupationStatus.Free);
            testedCar.SetBusy();

            var realValue = testedCar.IsFreeToRent();
            var exeptedValue = false;

            Assert.AreEqual(realValue, exeptedValue);
        }

        [TestMethod]
        public void UseReturnBack_IsFreeToRentShouldBeTrue()
        {
            var testedCar = new Car("Nissan", "Red", OccupationStatus.Busy);
            testedCar.SetFree();

            var realValue = testedCar.IsFreeToRent();
            var exeptedValue = true;

            Assert.AreEqual(realValue, exeptedValue);

        }
       
    }
}

