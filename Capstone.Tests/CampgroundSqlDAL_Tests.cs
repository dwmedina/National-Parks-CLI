using System;
using System.Collections.Generic;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundSqlDAL_Tests : CapstoneDBTests
    {
        [TestMethod]
        public void GetCampgrounds_Test()
        {
            Campground_DAL testCampgroundDAL = new Campground_DAL();
            Park_DAL testParkDAL = new Park_DAL();
            Park testPark = testParkDAL.GetParks()[1];

            // use the test park's id since it's an int
            IList<Campground> testResults = testCampgroundDAL.GetCampgroundsByPark(testPark.ParkID);

            // make sure we're getting the right campground
            Assert.AreEqual(1, testResults[0].CampID);
        }
    }
}
